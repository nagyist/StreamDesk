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

        /// <summary>
        /// 
        /// <returns>
        /// A <see cref="Tuple"/> which contains booleans telling you what features are supported by that
        /// database format.
        /// 
        /// Item1: Does the Database format support embeds (Non-hardcoded Application Embeds)
        /// Item2: Does the Database format support chat data
        /// Item3: Does the Database format support multiple Stream/Chat Embed Data
        /// Item4: Does the Database format support sub-providers
        /// Item5: Does the Database format support Stream Identifiers (UUID)
        /// Item6: Does the Database format support Provider Information
        /// </returns>
        /// </summary>
        Tuple<bool, bool, bool, bool, bool, bool> GetSupportedDatabaseFeatures();
    }
}
