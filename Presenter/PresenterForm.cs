using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Compiler.Presenter
{
    public class PresenterForm
    {
        private Dictionary<TabPage, string> tabPageFilePaths = new Dictionary<TabPage, string>();
        MainForm view;
        TabControl tabControl;
        public PresenterForm(MainForm view, TabControl tabControl)
        {
            this.view = view;
            this.tabControl = tabControl;
        }

        public void AddTabPage(string name = "")
        {
            if (name == "") name = NameNewFile();
            if (name != "")
            {
                TabPage tabPage = new(name);
                SplitContainer splitContainer = new SplitContainer
                {
                    Orientation = Orientation.Horizontal,
                    Dock = DockStyle.Fill,
                    SplitterDistance = tabPage.Height / 2,
                    SplitterWidth = 10,
                    Margin = new Padding(3)
                };
                RichTextBox richTextBox = new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    AcceptsTab = true,
                    
                };
                richTextBox.TextChanged += RichTextBox_TextChanged;
                DataGridView dataGridView = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    BackgroundColor = Color.White,
                    ReadOnly = true
                };
                splitContainer.Panel1.Controls.Add(richTextBox);
                splitContainer.Panel2.Controls.Add(dataGridView);
                tabPage.Controls.Add(splitContainer);
                tabControl.TabPages.Add(tabPage);
                tabControl.SelectedIndex = tabControl.TabCount - 1;
            }
        }
        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите текстовый файл";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);
                    string fileContent = File.ReadAllText(filePath);
                    AddTabPage(fileName);
                    TabPage tp = tabControl.TabPages[tabControl.TabCount - 1];
                    SplitContainer splitContainer = (SplitContainer)tp.Controls[0];
                    RichTextBox richTextBox = (RichTextBox)splitContainer.Panel1.Controls[0];
                    richTextBox.Text = fileContent;
                    tabPageFilePaths.Add(tp, filePath);
                    tabControl.SelectedIndex = tabControl.TabCount - 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void SaveFile()
        {
            if (tabControl.TabPages.Count != 0)
            {
                TabPage currentTabPage = tabControl.SelectedTab;
                if (!tabPageFilePaths.ContainsKey(currentTabPage) || string.IsNullOrEmpty(tabPageFilePaths[currentTabPage]))
                {
                    SaveFileAs(); // Если файл еще не был сохранен, показываем "Сохранить как..."
                }
                else
                {
                    string filePath = tabPageFilePaths[currentTabPage];
                    SaveTabPageContentToFile(currentTabPage, filePath);
                }
            }
            else return;
        }
        public void SaveFileAs()
        {
            if (tabControl.TabPages.Count != 0)
            {
                TabPage currentTabPage = tabControl.SelectedTab;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Сохранить как";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    SaveTabPageContentToFile(currentTabPage, filePath);
                    tabPageFilePaths[currentTabPage] = filePath; // Сохраняем путь к файлу
                    currentTabPage.Text = Path.GetFileName(filePath); // Обновляем заголовок вкладки
                }
            }
            else return;
        }
        public void CloseSelectedTab()
        {
            TabPage tabPageToClose = tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPageToClose.Controls[0];
            RichTextBox richTextBox = (RichTextBox)splitContainer.Panel1.Controls[0];

            if (richTextBox.Modified)
            {
                DialogResult result = MessageBox.Show($"Сохранить изменения в файле \"{tabPageToClose.Text}\" перед закрытием?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    tabControl.SelectedTab = tabPageToClose; // Активируем вкладку, чтобы она была текущей для SaveFile()
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    return; // Отмена закрытия
                }
            }
            // Удаляем путь к файлу из словаря
            tabPageFilePaths.Remove(tabPageToClose);
            // Закрываем вкладку
            tabControl.TabPages.Remove(tabPageToClose);
        }

        public void CLoseCompilyator()
        {
            for(int i = 0;i<tabControl.Controls.Count;i++)
            {
                tabControl.SelectedIndex = i;

                TabPage tabPageToClose = tabControl.TabPages[i];
                SplitContainer splitContainer = (SplitContainer)tabPageToClose.Controls[0];
                RichTextBox richTextBox = (RichTextBox)splitContainer.Panel1.Controls[0];

                if (richTextBox.Modified)
                {
                    DialogResult result = MessageBox.Show($"Сохранить изменения в файле \"{tabPageToClose.Text}\" перед выходом?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        SaveFile();
                    }
                    else if (result == DialogResult.No)
                    {
                        if (i != tabControl.Controls.Count) continue;
                    }
                }
                tabPageFilePaths.Remove(tabPageToClose);
                tabControl.TabPages.Remove(tabPageToClose);
            }
            Application.Exit();
        }
        private void SaveTabPageContentToFile(TabPage tabPage, string filePath)
        {
            try
            {
                SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0]; 
                RichTextBox richTextBox = (RichTextBox)splitContainer.Panel1.Controls[0];
                File.WriteAllText(filePath, richTextBox.Text);
                MessageBox.Show("Файл успешно сохранен.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTextBox.Modified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string NameNewFile()
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Введите название файла",
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 50, Top = 20, Text = "Название файла:" };
            TextBox textBox = new TextBox() { Text = "NewFile", Left = 50, Top = 50, Width = 200 };
            Button confirmation = new Button() { Text = "OK", Left = 50, Width = 100, Height = 30, Top = 90, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Отмена", Left = 150, Width = 100, Height = 30, Top = 90, DialogResult = DialogResult.Cancel };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);

            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            RichTextBox richTextBox = (RichTextBox)sender;
            richTextBox.Modified = true;  // Устанавливаем флаг Modified при изменении текста
        }
    }
}
