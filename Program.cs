using System.Configuration;

namespace Compiler
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var language = ConfigurationManager.AppSettings["language"];
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(language);
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}