using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StreamDesk.Core.DatabaseFormats
{
    public class PListMacFormatter : IDatabaseFormatter
    {
        public string FormatName
        {
            get { return "StreamDesk Database 1.0m (Macintosh v1.61 Plist)"; }
        }

        public string FileExtension
        {
            get { return ".plist"; }
        }

        public StreamDeskDatabase Read(System.IO.FileStream file)
        {
            throw new NotImplementedException();
        }

        public void Write(System.IO.FileStream file, StreamDeskDatabase streamDeskDatabase)
        {
            
        }

        public Tuple<bool, bool, bool, bool, bool, bool> GetSupportedDatabaseFeatures()
        {
            return Tuple.Create(false, false, false, false, false, false);
        }
    }
}
