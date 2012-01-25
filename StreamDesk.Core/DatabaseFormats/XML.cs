using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StreamDesk.Core.DatabaseFormats
{
    public class SDXMLFormatter : IDatabaseFormatter
    {
        public string FormatName
        {
            get { return "StreamDesk Database 1.3 (XML)"; }
        }

        public string FileExtension
        {
            get { return ".sdx"; }
        }

        public StreamDeskDatabase Read(System.IO.FileStream file)
        {
            var formatter = new XmlSerializer(typeof(StreamDeskDatabase));
            return (StreamDeskDatabase)formatter.Deserialize(file);
        }

        public void Write(System.IO.FileStream file, StreamDeskDatabase streamDeskDatabase)
        {
            var formatter = new XmlSerializer(typeof(StreamDeskDatabase));
            formatter.Serialize(file, streamDeskDatabase);
        }

        public Tuple<bool, bool, bool, bool, bool, bool> GetSupportedDatabaseFeatures()
        {
            return Tuple.Create(true, true, true, true, true, true);
        }
    }
}
