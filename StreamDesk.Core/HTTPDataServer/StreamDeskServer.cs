using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using StreamDesk.Properties;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Xml;
using StreamDesk.AppCore;

namespace StreamDesk.HTTPDataServer
{
    public class StateObject
    {
        public bool connected = false;
        public Socket workSocket = null;
        public Socket partnerSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
        public string id = String.Empty;
        public DateTime TimeStamp;
    }

    public class Server
    {
        protected int maxSockets;
        protected int sockCount = 0;
        private int convID = 0;
        private int portNumber;
        private System.Threading.Timer lostTimer;
        private const int numThreads = 1;
        private const int timerTimeout = 300000;
        private const int timeoutMinutes = 3;
        private bool ShuttingDown = false;
        protected string title;
        protected Hashtable connectedHT = new Hashtable();
        protected ArrayList connectedSocks;

        //Thread signal.
        private ManualResetEvent allDone = new ManualResetEvent(false);
        private Thread[] serverThread = new Thread[numThreads];
        private AutoResetEvent[] threadEnd = new AutoResetEvent[numThreads];


        public Server()
        {
            this.portNumber = 9898;
            this.title = "Meh";
            this.maxSockets = 10000;
            connectedSocks = new ArrayList(this.maxSockets);
        }


        /// 
        /// Description: Start the threads to listen to the port and process
        /// messages.
        /// 

        public void Start()
        {
            // Clear the thread end events
            for (int lcv = 0; lcv < numThreads; lcv++)
                threadEnd[lcv] = new AutoResetEvent(false);

            ThreadStart threadStart1 = new ThreadStart(StartListening);
            serverThread[0] = new Thread(threadStart1);
            serverThread[0].IsBackground = true;
            serverThread[0].Start();

            // Create the delegate that invokes methods for the timer.
            TimerCallback timerDelegate = new TimerCallback(this.CheckSockets);
            // Create a timer that waits one minute, then invokes every 5 minutes.
            lostTimer = new System.Threading.Timer(timerDelegate, null, Server.timerTimeout, Server.timerTimeout);

        }

        /// 
        /// Description: Check for dormant sockets and close them.
        /// 
        /// <param name="eventState">Required parameter for a timer call back
        /// method.</param>
        private void CheckSockets(object eventState)
        {
            lostTimer.Change(System.Threading.Timeout.Infinite,
                 System.Threading.Timeout.Infinite);
            try
            {
                foreach (StateObject state in connectedSocks)
                {
                    if (state.workSocket == null)
                    {	// Remove invalid state object
                        Monitor.Enter(connectedSocks);
                        if (connectedSocks.Contains(state))
                        {
                            connectedSocks.Remove(state);
                            Interlocked.Decrement(ref sockCount);
                        }
                        Monitor.Exit(connectedSocks);
                    }
                    else
                    {
                        if (DateTime.Now.AddTicks(-state.TimeStamp.Ticks).Minute > timeoutMinutes)
                        {
                            RemoveSocket(state);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                lostTimer.Change(Server.timerTimeout, Server.timerTimeout);
            }
        }
        /// 
        /// Decription: Stop the threads for the port listener.
        /// 
        public void Stop()
        {
            int lcv;
            lostTimer.Dispose();
            lostTimer = null;

            for (lcv = 0; lcv < numThreads; lcv++)
            {
                if (!serverThread[lcv].IsAlive)
                    threadEnd[lcv].Set();	// Set event if thread is already dead
            }
            ShuttingDown = true;
            // Create a connection to the port to unblock the listener thread
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, this.portNumber);
            sock.Connect(endPoint);
            //sock.Close();
            sock = null;

            // Check thread end events and wait for up to 5 seconds.
            for (lcv = 0; lcv < numThreads; lcv++)
                threadEnd[lcv].WaitOne(5000, false);
        }

        /// 
        /// Decription: Open a listener socket and wait for a connection.
        /// 
        private void StartListening()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, this.portNumber);
            Socket listener = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(1000);

                while (!ShuttingDown)
                {
                    allDone.Reset();
                    listener.BeginAccept(new AsyncCallback(this.AcceptCallback), listener);
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                threadEnd[0].Set();
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            StateObject state = new StateObject();
            state.workSocket = handler;
            state.TimeStamp = DateTime.Now;
            try
            {
                Interlocked.Increment(ref sockCount);
                Monitor.Enter(connectedSocks);
                connectedSocks.Add(state);
                Monitor.Exit(connectedSocks);
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(this.ReadCallback), state);
                if (sockCount > this.maxSockets)
                {
                    RemoveSocket(state);
                    handler = null;
                    state = null;
                }
            }
            catch (SocketException es)
            {
                RemoveSocket(state);
            }
            catch (Exception e)
            {
                RemoveSocket(state);
            }
        }

        protected void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            try
            {
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {

#if ENABLE_DEBUGGABLE_LOGGING
                    if (!EventLog.SourceExists("StreamDesk HTTP Service"))
                        EventLog.CreateEventSource(
                           "StreamDesk HTTP Service", "Application");
                    EventLog EventLog1 = new EventLog();
                    EventLog1.Source = "StreamDesk HTTP Service";
                    EventLog1.WriteEntry(Encoding.ASCII.GetString(state.buffer));
#endif

                    string full = Encoding.ASCII.GetString(state.buffer);
                    string getstring = Encoding.ASCII.GetString(state.buffer).Replace("GET /", "").Split(new string[] { " HTTP/1.1" }, StringSplitOptions.RemoveEmptyEntries)[0];

#if ENABLE_DEBUGGABLE_LOGGING
                    EventLog1.WriteEntry(getstring);
#endif

                    string[] getdata = getstring.Split('/');

#if ENABLE_DEBUGGABLE_LOGGING
                    EventLog1.WriteEntry(getdata[0]);
#endif

                    if (getdata[0] == "+gettree")
                    {
                        string xml = StreamDeskDBControl._GetStreamList();
                        Send(handler, String.Format("{0}{1}", String.Format(Resources.HTTPHeaderXML, xml.Length), xml));
                    }
                    else if (getdata[0] == "+chat")
                    {
                        string html = "<b>There is no Chat embed. Actually you're not even spoce to see this O_O.</b>";
                        if (StreamDeskDBControl.IsChatEmbed(getdata[1]))
                        {
                            html = String.Format(StreamDeskDBControl.ChatEmbed[getdata[1]]["EmbedStringFormat"], getdata[2]);
                        }
                        Send(handler, String.Format("{0}{1}", String.Format(Resources.HTTPHeader, html.Length), html));
                    }
                    else if (getdata[0] == "+is_stream_type")
                    {
                        string html = StreamDeskDBControl.IsStreamEmbed(getdata[1]).ToString();
                        Send(handler, String.Format("{0}{1}", String.Format(Resources.HTTPHeader, html.Length), html));
                    }
                    else if (getdata[0] == "+stream")
                    {
                        string html = String.Format(StreamDeskDBControl.StreamEmbed[getdata[1]], getdata[2]);
                        Send(handler, String.Format("{0}{1}", String.Format(Resources.HTTPHeader, html.Length), html));
                    }
                    RemoveSocket(state);
                }
                else
                {
                    RemoveSocket(state);
                }
            }
            catch (System.Net.Sockets.SocketException es)
            {
                RemoveSocket(state);
                if (es.ErrorCode != 64)
                {
                    Console.WriteLine(string.Format("ReadCallback Socket Exception: {0}, {1}.", es.ErrorCode, es.ToString()));
                }
            }
            catch (Exception e)
            {
                RemoveSocket(state);
                if (e.GetType().FullName != "System.ObjectDisposedException")
                {
                    Console.WriteLine(string.Format("ReadCallback Exception: {0}.", e.ToString()));
                }
            }
        }

        protected void Send(Socket sock, string data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            if (byteData.Length > 0)
                sock.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(this.SendCallback), sock);
        }

        protected void Send(Socket sock, byte[] data)
        {
            if (data.Length > 0)
                sock.BeginSend(data, 0, data.Length, 0,
                new AsyncCallback(this.SendCallback), sock);
        }

        protected void SendCallback(IAsyncResult ar)
        {
            Socket handler = (Socket)ar.AsyncState;
            try
            {
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception e)
            {
            }
        }

        virtual protected void RemoveSocket(StateObject state)
        {
            Socket sock = state.workSocket;
            Monitor.Enter(connectedSocks);
            if (connectedSocks.Contains(state))
            {
                connectedSocks.Remove(state);
                Interlocked.Decrement(ref sockCount);
            }
            Monitor.Exit(connectedSocks);
            Monitor.Enter(connectedHT);

            if ((sock != null) && (connectedHT.ContainsKey(sock)))
            {
                object sockTemp = connectedHT[sock];
                if (connectedHT.ContainsKey(sockTemp))
                {
                    if (connectedHT.ContainsKey(connectedHT[sockTemp]))
                    {
                        connectedHT.Remove(sock);
                        if (sock.Equals(connectedHT[sockTemp]))
                        {
                            connectedHT.Remove(sockTemp);
                        }
                        else
                        {
                            object val, key = sockTemp;
                            while (true)
                            {
                                val = connectedHT[key];
                                if (sock.Equals(val))
                                {
                                    connectedHT[key] = sockTemp;
                                    break;
                                }
                                else if (connectedHT.ContainsKey(val))
                                    key = val;
                                else	// The chain is broken
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(string.Format("Socket is not in the {0} connected hash table!", this.title));
                    }
                }
            }
            Monitor.Exit(connectedHT);

            if (sock != null)
            {
                sock.Close();
                sock = null;
                state.workSocket = null;
                state = null;
            }
        }
    }
}