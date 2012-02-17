using System;

namespace NasuTek.M3 {
    public partial class FormatterEngine {     
        [Obsolete("You should use the Mono Addin API's to expose your plugin. See the StreamDesk Extensibility Documentation for more details.", true)] public Tuple<bool, Exception> LoadFormatterDll(string path) {
            throw new NotImplementedException("This code is obsolete. You should use the Mono Addin API's to expose your plugin. See the StreamDesk Extensibility Documentation for more details.");
        }
    }
}

