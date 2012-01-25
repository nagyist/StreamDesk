using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StreamDesk.Managed;

namespace Editor
{
    class MessageHelper
    {
        public static bool ShowCompatabilityMessage(IDatabaseFormatter formatter)
        {
            var compatabilityTurple = formatter.GetSupportedDatabaseFeatures();

            if (compatabilityTurple.Item1 && compatabilityTurple.Item2 && compatabilityTurple.Item3
                && compatabilityTurple.Item4 && compatabilityTurple.Item5 && compatabilityTurple.Item6)
                return true;          

            string helpMessage =
                @"Note this format does not support the following features of the StreamDesk 1.2 Database format:

";
            if (!compatabilityTurple.Item1)
                helpMessage += "- Does not support embeds (Non-hardcoded Application Embeds)\n";
            if (!compatabilityTurple.Item2)
                helpMessage += "- Does not support chat data\n";
            if (!compatabilityTurple.Item3)
                helpMessage += "- Does not support multiple Stream/Chat Embed Data\n";
            if (!compatabilityTurple.Item4)
                helpMessage += "- Does not support sub-providers\n";
            if (!compatabilityTurple.Item5)
                helpMessage += "- Does not support Stream Identifiers (UUID)\n";
            if (!compatabilityTurple.Item6)
                helpMessage += "- Does not support Provider Information\n";

            helpMessage +=
                @"
Are you sure you still want to save in the ""{0}"" Database Format?";

            if (MessageBox.Show(String.Format(helpMessage, formatter.FormatName), "StreamDesk Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                return true;

            return false;
        }
    }
}
