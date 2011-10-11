namespace StreamDesk
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class frmChat : Form
    {
        public frmChat(string chatServer, string chatChannel)
        {
            InitializeComponent();
            if (chatServer == "geekshed")
            {
                this.chatHTML = "<html><body style=\"padding: 0px; margin: 0px;\"><iframe scrolling=\"no\" frameborder=\"0\" height=\"100%\" width=\"100%\" name=\"flashchat\" src=\"http://flashirc.geekshed.net/getchat.php?channel=" + chatChannel + "\"></iframe></body></html>";
            }
            else if (chatServer == "justintv")
            {
                this.chatHTML = "<html><body style=\"padding: 0px; margin: 0px;\"><object type=\"application/x-shockwave-flash\" height=\"100%\" width=\"100%\" id=\"jtv_chat_flash\" data=\"http://www.justin.tv/widgets/jtv_chat.swf?channel=" + chatChannel + "\" bgcolor=\"#000000\"><param name=\"allowFullScreen\" value=\"true\" /><param name=\"movie\" value=\"http://www.justin.tv/widgets/jtv_chat.swf\" /><param name=\"flashvars\" value=\"channel=" + chatChannel + "\" /></object></body></html>";
            }
            else
            {
                this.chatHTML = "<html><body style=\"padding: 0px; margin: 0px;\"><embed width=\"100%\" height=\"100%\" type=\"application/x-shockwave-flash\" flashvars=\"channel=#" + chatChannel + "&server=" + chatServer + "\" pluginspage=\"http://www.adobe.com/go/getflashplayer\" src=\"http://www.ustream.tv/IrcClient.swf\" allowfullscreen=\"true\" /></body></html>";
            }
        }

        private void frmChat_Load(object sender, EventArgs e)
        {
            this.wbWebIRC.DocumentText = this.chatHTML;
        }
    }
}

