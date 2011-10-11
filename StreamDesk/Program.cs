namespace StreamDesk
{
    using System;
    using System.Windows.Forms;
    using System.Reflection;
    using System.ServiceProcess;

    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                ServiceController sc = new ServiceController("StreamDeskService");
                if (sc.Status != ServiceControllerStatus.Stopped)
                {
                    Blah(args);
                }
                else
                {
                    if (MessageBox.Show("StreamDesk requires the StreamDesk HTTP Data Service to be running. Do you want to start it now? (Note: You requre administrative priviliges to start the service.)", "StreamDesk", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        sc.Start();
                        sc.WaitForStatus(ServiceControllerStatus.Running);
                        Blah(args);
                    }
                }
            }
            catch(InvalidOperationException)
            {
                MessageBox.Show("StreamDesk requires the StreamDesk HTTP Data Service to be installed. Please reinstall StreamDesk", "StreamDesk", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void Blah(string[] args)
        {
            Assembly streamdeskdll = Assembly.LoadFile(Application.StartupPath + "\\StreamDesk.Core.exe");
            Assembly checkforxulruntime = Assembly.LoadWithPartialName("NasuTek.XUL.Runtime, Version=1.0.0.0");
            if (checkforxulruntime != null)
            {
                Type module = streamdeskdll.GetType("StreamDesk.Program");
                MethodInfo mi = module.GetMethod("Main");
                mi.Invoke(null, new object[] { args, true });
            }
            else
            {
                MessageBox.Show("StreamDesk requires the NasuTek XUL Runtime to be installed on your computer. You can get more information at http://nasutek.com/products/xulruntime.", "StreamDesk", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}