using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using StreamDesk.Managed.DatabaseFormats;

namespace StreamDesk.Managed
{
    public class FormatterEngine
    {
        public List<IDatabaseFormatter> Formatters { get; private set; }

        public FormatterEngine()
        {
            Formatters = new List<IDatabaseFormatter> {new SDXMLFormatter()};
        }

        public IDatabaseFormatter GetFormatterByExtension(string extension)
        {
            return Formatters.FirstOrDefault(i => i.FileExtension == extension);
        }

        public Tuple<bool, Exception> LoadFormatterDll(string path) {
            try {
                var assembly = Assembly.LoadFile(path);
                foreach (var type in assembly.GetTypes().Where(p => typeof(IDatabaseFormatter).IsAssignableFrom(p))) {
                    Formatters.Add((IDatabaseFormatter)type.GetConstructor(Type.EmptyTypes).Invoke(null));
                }
                return Tuple.Create(true, (Exception)null);
            } catch(Exception e) {
                return Tuple.Create(false, e);
            }
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
