using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Mono.Addins;
using StreamDesk.Managed.Database;

namespace StreamDesk.Managed.DatabaseFormats
{
    [Extension("/StreamDesk/DatabaseFormatters")]
    class SDXMLFormatter : IDatabaseFormatter
    {
        public string FormatName
        {
            get { return "StreamDesk Database 2.0 (XML)"; }
        }

        public string FileExtension
        {
            get { return ".sdnx"; }
        }

        public StreamDeskDatabase Read(System.IO.Stream file)
        {
            var formatter = new XmlSerializer(typeof(StreamDeskDatabase));
            return (StreamDeskDatabase)formatter.Deserialize(file);
        }

        public void Write(System.IO.FileStream file, StreamDeskDatabase streamDeskDatabase)
        {
            var formatter = new XmlSerializer(typeof(StreamDeskDatabase));
            formatter.Serialize(file, streamDeskDatabase);
        }
    }
}
