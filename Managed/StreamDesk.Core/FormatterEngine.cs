using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreamDesk.Managed.DatabaseFormats;

namespace StreamDesk.Managed
{
    public class FormatterEngine
    {
        public List<IDatabaseFormatter> Formatters { get; private set; }

        public FormatterEngine()
        {
            Formatters = new List<IDatabaseFormatter> {new SDBinaryFormatter(), new SDXMLFormatter(), new GoogleProtocolBuffers()};
        }

        public IDatabaseFormatter GetFormatterByExtension(string extension)
        {
            return Formatters.FirstOrDefault(i => i.FileExtension == extension);
        }

        public string ReturnFilter
        {
            get
            {
                return
                    Formatters.Aggregate("",
                                         (current, databaseFormatter) =>
                                         current +
                                         String.Format("|{0} (*{1})|*{1}", databaseFormatter.FormatName,
                                                       databaseFormatter.FileExtension)).Substring(1);
            }
        }
    }
}
