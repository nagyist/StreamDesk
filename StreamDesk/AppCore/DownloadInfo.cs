namespace StreamDesk.AppCore
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;

    public class DownloadInfo
    {
        public byte[] BufferRead = new byte[0x400];
        private const int BufferSize = 0x400;
        public int bytesProcessed = 0;
        public byte[] dataBufferFast;
        public ArrayList dataBufferSlow;
        public int dataLength = -1;
        public DownloadProgressHandler ProgressCallback;
        public WebRequest Request = null;
        public Stream ResponseStream;
        public bool useFastBuffers = true;
    }
}

