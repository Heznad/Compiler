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
            GetText("Compiler.TextFiles.Help.txt");
        }
        private void InfoForm()
        {
            GetText("Compiler.TextFiles.Info.txt");
        }
        private void GetText(string path)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = path;
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        MessageBox.Show($"Ресурс '{resourceName}' не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        richTextBox.Text = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке ресурса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}