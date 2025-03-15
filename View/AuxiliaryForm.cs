using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Compiler.View
{
    public partial class AuxiliaryForm : Form
    {
        public AuxiliaryForm(string format)
        {
            InitializeComponent();
            switch (format)
            {
                case "btn_Help":
                case "tsmi_Help":
                    HelpForm();
                    break;
                case "btn_Info":
                case "tsmi_Info":
                    InfoForm();
                    break;
                default:
                    break;
            }
        }
        private void HelpForm()
        {
            richTextBox.Text = GetTextFromResource("Compiler.TXT.Help.txt");
        }
        private void InfoForm()
        {
            richTextBox.Text = GetTextFromResource("Compiler.TXT.Info.txt");
        }
        private static string GetTextFromResource(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
