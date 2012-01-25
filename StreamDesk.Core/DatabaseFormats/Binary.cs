using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace StreamDesk.Core.DatabaseFormats
{
    public class SDBinaryFormatter : IDatabaseFormatter
    {
        public StreamDeskDatabase Read(System.IO.FileStream file)
        {
            var formatter = new BinaryFormatter();
            return (StreamDeskDatabase) formatter.Deserialize(file);
        }

        public void Write(System.IO.FileStream file, StreamDeskDatabase streamDeskDatabase)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(file, streamDeskDatabase);
        }

        public string FormatName
        {
            get { return "StreamDesk Database 1.3 (Binary)"; }
        }

        public string FileExtension
        {
            get { return ".sdb"; }
        }

        public Tuple<bool, bool, bool, bool, bool, bool> GetSupportedDatabaseFeatures()
        {
            return Tuple.Create(true, true, true, true, true, true);
        }
    }
}
