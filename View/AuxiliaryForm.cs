using System.Globalization;
using System.Reflection;

namespace Compiler.View
{
    public partial class AuxiliaryForm : Form
    {
        /*Вспомогательная форма.
        Для вывода содержимого больших текстовых файлов*/
        public AuxiliaryForm(string format)
        {
            InitializeComponent();
            switch (format)
            {
                case "tsmi_Problem":
                    this.Text = MyString.ProblemStatement;
                    break;
                case "tsmi_Grammar":
                    this.Text = MyString.Grammar;
                    break;
                case "tsmi_GrammarClass":
                    this.Text = MyString.GrammarClassification;
                    break;
                case "tsmi_Analysis":
                    this.Text = MyString.AnalysisMethod;
                    break;
                case "tsmi_Diagnostics":
                    this.Text = MyString.DiagnosticsAndNeutralizationOfErrors;
                    break;
                case "tsmi_TextExample":
                    this.Text = MyString.TextExample;
                    break;
                case "tsmi_ListLiterature":
                    this.Text = MyString.ListOfLiterature;
                    break;
                case "tsmi_ProgramSource":
                    this.Text = MyString.ProgramSourceCode;
                    break;
                case "btn_Help":
                case "tsmi_Help":
                    this.Text = MyString.Help;
                    HelpForm();
                    break;
                case "btn_Info":
                case "tsmi_Info":
                    this.Text = MyString.AboutProgram;
                    InfoForm();
                    break;
                default:
                    break;
            }
        }

        // Файлы для формы "Справка"
        private void HelpForm()
        {
            if (CultureInfo.CurrentCulture.Name == "ru")
                GetText("Compiler.TextFiles.Help.txt");
            else
                GetText("Compiler.TextFiles.HelpEN.txt");
        }

        // Файлы для формы "О программе"
        private void InfoForm()
        {
            if (CultureInfo.CurrentCulture.Name == "ru")
                GetText("Compiler.TextFiles.Info.txt");
            else
                GetText("Compiler.TextFiles.InfoEN.txt");
        }

       // Получаем текст из встроенных ресурсов
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
                        MessageBox.Show(MyString.ResourceNotFound + resourceName, MyString.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(MyString.ErrorDownloadResource + ex.Message, MyString.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}