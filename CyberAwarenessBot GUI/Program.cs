using System;
using System.Windows.Forms;

namespace CyberAwarenessBotGUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new HomeForm());   // splash first
        }
    }
}
