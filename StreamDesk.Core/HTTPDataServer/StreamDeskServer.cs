#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using StreamDesk.AppCore;
using StreamDesk.Properties;
using System.Web;
using System.Windows.Forms;

#endregion

namespace StreamDesk.HTTPDataServer {
    public class StateObject {
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public bool connected = false;
        public string id = String.Empty;
        public Socket partnerSocket = null;
        public StringBuilder sb = new StringBuilder ();
        public DateTime TimeStamp;
        public Socket workSocket = null;
    }

    public class Server {
        private const int numThreads = 1;
        private const int timeoutMinutes = 3;
        private const int timerTimeout = 300000;
        private ManualResetEvent allDone = new ManualResetEvent (false);
        protected Hashtable connectedHT = new Hashtable ();
        protected ArrayList connectedSocks;
        private int convID = 0;
        private System.Threading.Timer lostTimer;
        protected int maxSockets;
        private int portNumber;

        //Thread signal.
        private Thread[] serverThread = new Thread[numThreads];
        private bool ShuttingDown = false;
        protected int sockCount = 0;
        private AutoResetEvent[] threadEnd = new AutoResetEvent[numThreads];
        protected string title;

        public Server () {
            portNumber = 9898;
            title = "Meh";
            maxSockets = 10000;
            connectedSocks = new ArrayList (maxSockets);
        }

        /// 
        /// Description: Start the threads to listen to the port and process
        /// messages.
        /// 
        public void Start () {
            // Clear the thread end events
            for (int lcv = 0 ; lcv < numThreads ; lcv++)
                threadEnd[lcv] = new AutoResetEvent (false);

            var threadStart1 = new ThreadStart(delegate
                {
                    // Create the delegate that invokes methods for the timer.
                    var timerDelegate = new TimerCallback(CheckSockets);
                    // Create a timer that waits one minute, then invokes every 5 minutes.
                    lostTimer = new System.Threading.Timer(timerDelegate, null, timerTimeout, timerTimeout);
                });
            serverThread[0] = new Thread (threadStart1);
            serverThread[0].IsBackground = true;
            serverThread[0].Start ();

            StartListening();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            EventLog eventLog = new EventLog ();
            if (EventLog.SourceExists ("SDS")) EventLog.CreateEventSource ("SDS", "Application");
            eventLog.Source = "SDS";
            eventLog.WriteEntry (((Exception) e.ExceptionObject).ToString (), EventLogEntryType.Error);
        }

        /// 
        /// Description: Check for dormant sockets and close them.
        /// 
        /// <param name="eventState">Required parameter for a timer call back
        /// method.</param>
        private void CheckSockets (object eventState) {
            lostTimer.Change (Timeout.Infinite,
                              Timeout.Infinite);
            try {
                foreach (StateObject state in connectedSocks) {
                    if (state.workSocket == null) {
                        // Remove invalid state object
                        Monitor.Enter (connectedSocks);
                        if (connectedSocks.Contains (state)) {
                            connectedSocks.Remove (state);
                            Interlocked.Decrement (ref sockCount);
                        }
                        Monitor.Exit (connectedSocks);
                    } else {
                        if (DateTime.Now.AddTicks (-state.TimeStamp.Ticks).Minute > timeoutMinutes) {
                            RemoveSocket (state);
                        }
                    }
                }
            } catch (Exception) {} finally {
                lostTimer.Change (timerTimeout, timerTimeout);
            }
        }

        /// 
        /// Decription: Stop the threads for the port listener.
        /// 
        public void Stop () {
            int lcv;
            lostTimer.Dispose ();
            lostTimer = null;

            for (lcv = 0 ; lcv < numThreads ; lcv++) {
                if (!serverThread[lcv].IsAlive)
                    threadEnd[lcv].Set (); // Set event if thread is already dead
            }
            ShuttingDown = true;
            // Create a connection to the port to unblock the listener thread
            var sock = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var endPoint = new IPEndPoint (IPAddress.Loopback, portNumber);
            sock.Connect (endPoint);
            //sock.Close();
            sock = null;

            // Check thread end events and wait for up to 5 seconds.
            for (lcv = 0 ; lcv < numThreads ; lcv++)
                threadEnd[lcv].WaitOne (5000, false);
        }

        /// 
        /// Decription: Open a listener socket and wait for a connection.
        /// 
        private void StartListening () {
            var localEndPoint = new IPEndPoint (IPAddress.Any, portNumber);
            var listener = new Socket (AddressFamily.InterNetwork,
                                       SocketType.Stream, ProtocolType.Tcp);

            try {
                listener.Bind (localEndPoint);
                listener.Listen (1000);

                while (!ShuttingDown) {
                    allDone.Reset ();
                    listener.BeginAccept (new AsyncCallback (AcceptCallback), listener);
                    allDone.WaitOne ();
                }
            } catch (Exception e) {
                threadEnd[0].Set ();
            }
        }

        private void AcceptCallback (IAsyncResult ar) {
            allDone.Set ();
            var listener = (Socket) ar.AsyncState;
            Socket handler = listener.EndAccept (ar);
            var state = new StateObject ();
            state.workSocket = handler;
            state.TimeStamp = DateTime.Now;
            try {
                Interlocked.Increment (ref sockCount);
                Monitor.Enter (connectedSocks);
                connectedSocks.Add (state);
                Monitor.Exit (connectedSocks);
                handler.BeginReceive (state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback (ReadCallback),
                                      state);
                if (sockCount > maxSockets) {
                    RemoveSocket (state);
                    handler = null;
                    state = null;
                }
            } catch (SocketException es) {
                RemoveSocket (state);
            } catch (Exception e) {
                RemoveSocket (state);
            }
        }

        protected void ReadCallback (IAsyncResult ar) {
            String content = String.Empty;
            var state = (StateObject) ar.AsyncState;
            Socket handler = state.workSocket;
            try {
                int bytesRead = handler.EndReceive (ar);
                if (bytesRead > 0) {
                    string full = Encoding.ASCII.GetString (state.buffer);
                    string getstring =
                        Encoding.ASCII.GetString (state.buffer).Replace ("GET /", "").Split (new string[] {
                                                                                                              " HTTP/1.1"
                                                                                                          },
                                                                                             StringSplitOptions.
                                                                                                 RemoveEmptyEntries)[0];
                    string[] getdata = getstring.Split ('/');

                    if (getdata[0] == "+gettree") {
                        string xml = StreamDeskDBControl._GetStreamList ();
                        Send (handler,
                              String.Format ("{0}{1}", String.Format (Resources.HTTPHeaderXML, xml.Length), xml));
                    } else if (getdata[0] == "+chat") {
                        string html = "<b>There is no Chat embed. Actually you're not even spoce to see this O_O.</b>";
                        if (StreamDeskDBControl.IsChatEmbed (getdata[1])) {
                            html = String.Format (StreamDeskDBControl.ChatEmbed[getdata[1]]["EmbedStringFormat"],
                                                  HttpUtility.UrlDecode (getdata[2]));
                        }
                        Send (handler, String.Format ("{0}{1}", String.Format (Resources.HTTPHeader, html.Length), html));
                    } else if (getdata[0] == "+is_stream_type") {
                        string html = StreamDeskDBControl.IsStreamEmbed (getdata[1]).ToString ();
                        Send (handler, String.Format ("{0}{1}", String.Format (Resources.HTTPHeader, html.Length), html));
                    } else if (getdata[0] == "+stream") {
                        string html = String.Format (StreamDeskDBControl.StreamEmbed[getdata[1]],
                                                     HttpUtility.UrlDecode (getdata[2]));
                        Send (handler, String.Format ("{0}{1}", String.Format (Resources.HTTPHeader, html.Length), html));
                    } else if (getdata[0] == "+update") {
                        string html;
                        if (StreamDeskDBControl.Update () == true) {
                            html = "Update Complete";
                        } else {
                            html = "Update Failed";
                        }
                        Send (handler, String.Format ("{0}{1}", String.Format (Resources.HTTPHeader, html.Length), html));
                    } else if (getdata[0] == "+exception") {
                        throw new Exception("Testing");
                    }
                    RemoveSocket (state);
                } else {
                    RemoveSocket (state);
                }
            } catch (SocketException es) {
#if ENABLE_DEBUGGABLE_LOGGING
                RemoveSocket (state);
                if (es.ErrorCode != 64) {
                    Console.WriteLine (string.Format ("ReadCallback Socket Exception: {0}, {1}.", es.ErrorCode,
                                                      es.ToString ()));
                }
                EventLog eventLog = new EventLog();
                if (EventLog.SourceExists("SDS")) EventLog.CreateEventSource("SDS", "Application");
                eventLog.Source = "SDS";
                eventLog.WriteEntry(es.ToString(), EventLogEntryType.Error);
#endif
            } catch (Exception e) {
#if ENABLE_DEBUGGABLE_LOGGING
                RemoveSocket (state);
                if (e.GetType ().FullName != "System.ObjectDisposedException") {
                    Console.WriteLine (string.Format ("ReadCallback Exception: {0}.", e.ToString ()));
                }
                EventLog eventLog = new EventLog();
                if (EventLog.SourceExists("SDS")) EventLog.CreateEventSource("SDS", "Application");
                eventLog.Source = "SDS";
                eventLog.WriteEntry(e.ToString(), EventLogEntryType.Error);
#endif
            }
        }

        protected void Send (Socket sock, string data) {
            byte[] byteData = Encoding.ASCII.GetBytes (data);
            if (byteData.Length > 0)
                sock.BeginSend (byteData, 0, byteData.Length, 0,
                                new AsyncCallback (SendCallback), sock);
        }

        protected void Send (Socket sock, byte[] data) {
            if (data.Length > 0)
                sock.BeginSend (data, 0, data.Length, 0,
                                new AsyncCallback (SendCallback), sock);
        }

        protected void SendCallback (IAsyncResult ar) {
            var handler = (Socket) ar.AsyncState;
            try {
                int bytesSent = handler.EndSend (ar);
            } catch (Exception e) {}
        }

        protected virtual void RemoveSocket (StateObject state) {
            Socket sock = state.workSocket;
            Monitor.Enter (connectedSocks);
            if (connectedSocks.Contains (state)) {
                connectedSocks.Remove (state);
                Interlocked.Decrement (ref sockCount);
            }
            Monitor.Exit (connectedSocks);
            Monitor.Enter (connectedHT);

            if ((sock != null) && (connectedHT.ContainsKey (sock))) {
                object sockTemp = connectedHT[sock];
                if (connectedHT.ContainsKey (sockTemp)) {
                    if (connectedHT.ContainsKey (connectedHT[sockTemp])) {
                        connectedHT.Remove (sock);
                        if (sock.Equals (connectedHT[sockTemp])) {
                            connectedHT.Remove (sockTemp);
                        } else {
                            object val, key = sockTemp;
                            while (true) {
                                val = connectedHT[key];
                                if (sock.Equals (val)) {
                                    connectedHT[key] = sockTemp;
                                    break;
                                } else if (connectedHT.ContainsKey (val))
                                    key = val;
                                else // The chain is broken
                                    break;
                            }
                        }
                    } else {
                        Console.WriteLine (string.Format ("Socket is not in the {0} connected hash table!", title));
                    }
                }
            }
            Monitor.Exit (connectedHT);

            if (sock != null) {
                sock.Close ();
                sock = null;
                state.workSocket = null;
                state = null;
            }
        }
    }
}