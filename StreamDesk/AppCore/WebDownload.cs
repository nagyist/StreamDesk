namespace StreamDesk.AppCore
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Threading;

    public class WebDownload
    {
        public ManualResetEvent allDone = new ManualResetEvent(false);
        private const int BUFFER_SIZE = 0x400;

        public byte[] Download(string url, DownloadProgressHandler progressCB)
        {
            this.allDone.Reset();
            Uri requestUri = new Uri(url);
            WebRequest request = WebRequest.Create(requestUri);
            DownloadInfo state = new DownloadInfo();
            state.Request = request;
            state.ProgressCallback = (DownloadProgressHandler) Delegate.Combine(state.ProgressCallback, progressCB);
            request.BeginGetResponse(new AsyncCallback(this.ResponseCallback), state);
            this.allDone.WaitOne();
            if (state.useFastBuffers)
            {
                return state.dataBufferFast;
            }
            byte[] buffer = new byte[state.dataBufferSlow.Count];
            for (int i = 0; i < state.dataBufferSlow.Count; i++)
            {
                buffer[i] = (byte) state.dataBufferSlow[i];
            }
            return buffer;
        }

        private void ReadCallBack(IAsyncResult asyncResult)
        {
            DownloadInfo asyncState = (DownloadInfo) asyncResult.AsyncState;
            Stream responseStream = asyncState.ResponseStream;
            int length = responseStream.EndRead(asyncResult);
            if (length > 0)
            {
                if (asyncState.useFastBuffers)
                {
                    Array.Copy(asyncState.BufferRead, 0, asyncState.dataBufferFast, asyncState.bytesProcessed, length);
                }
                else
                {
                    for (int i = 0; i < length; i++)
                    {
                        asyncState.dataBufferSlow.Add(asyncState.BufferRead[i]);
                    }
                }
                asyncState.bytesProcessed += length;
                if (asyncState.ProgressCallback != null)
                {
                    asyncState.ProgressCallback(asyncState.bytesProcessed, asyncState.dataLength);
                }
                responseStream.BeginRead(asyncState.BufferRead, 0, 0x400, new AsyncCallback(this.ReadCallBack), asyncState);
            }
            else
            {
                responseStream.Close();
                this.allDone.Set();
            }
        }

        private void ResponseCallback(IAsyncResult ar)
        {
            DownloadInfo asyncState = (DownloadInfo) ar.AsyncState;
            WebResponse response = asyncState.Request.EndGetResponse(ar);
            string str = response.Headers["Content-Length"];
            if (str != null)
            {
                asyncState.dataLength = Convert.ToInt32(str);
                asyncState.dataBufferFast = new byte[asyncState.dataLength];
            }
            else
            {
                asyncState.useFastBuffers = false;
                asyncState.dataBufferSlow = new ArrayList(0x400);
            }
            Stream responseStream = response.GetResponseStream();
            asyncState.ResponseStream = responseStream;
            responseStream.BeginRead(asyncState.BufferRead, 0, 0x400, new AsyncCallback(this.ReadCallBack), asyncState);
        }
    }
}

