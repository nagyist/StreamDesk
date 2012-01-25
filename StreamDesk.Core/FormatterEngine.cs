using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreamDesk.Core.DatabaseFormats;

namespace StreamDesk.Core
{
    public class FormatterEngine
    {
        public List<IDatabaseFormatter> Formatters { get; private set; }

        public FormatterEngine()
        {
            Formatters = new List<IDatabaseFormatter> {new SDBinaryFormatter(), new SDXMLFormatter(), new PListMacFormatter()};
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
