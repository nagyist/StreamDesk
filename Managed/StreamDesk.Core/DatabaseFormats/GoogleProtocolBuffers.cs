using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamDesk.Managed.DatabaseFormats
{
    class GoogleProtocolBuffers : IDatabaseFormatter
    {
        public string FormatName
        {
            get { return "StreamDesk Database 2.0 (Google Protocol Buffers)"; }
        }

        public string FileExtension
        {
            get { return ".sdnp"; }
        }

        public StreamDeskDatabase Read(System.IO.FileStream file)
        {
            throw new NotImplementedException();
        }

        public void Write(System.IO.FileStream file, StreamDeskDatabase streamDeskDatabase)
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, bool, bool, bool, bool, bool> GetSupportedDatabaseFeatures()
        {
            throw new NotImplementedException();
        }
    }
}
