using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamDesk.Managed
{
    public interface IDatabaseFormatter
    {
        string FormatName { get; }
        string FileExtension { get; }
        StreamDeskDatabase Read(System.IO.FileStream file);
        void Write(System.IO.FileStream file, StreamDeskDatabase streamDeskDatabase);
    }
}
