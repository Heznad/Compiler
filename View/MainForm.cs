using System.Globalization;
using Compiler.Model;
using Compiler.Presenter;
using Compiler.View;

namespace Compiler
{
    public partial class MainForm : Form
    {
        PresenterForm presenter;
        public MainForm()
        {
            InitializeComponent();
            this.KeyPreview = true; // Для горячих клавищ
            presenter = new(tabControl, btn_Undo, btn_Redo);
            timer_1.Start();
            timer_2.Start();
            presenter.AddTabPage(MyString.NewFile);
            presenter.UpdateUndoRedoButtonStates();
        }


        #region [ Файл ]
        private void btn_File_Click(object sender, EventArgs e)
        {
            presenter.AddTabPage();
        }
        private void btn_Folder_Click(object sender, EventArgs e)
        {
            presenter.OpenFile();
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            presenter.SaveFile();
        }
        private void btn_SaveAs_Click(object sender, EventArgs e)
        {
            presenter.SaveFileAs();
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
             this.Close();
        }
        #endregion

        #region [ Правка ]
        private void btn_Undo_Click(object sender, EventArgs e)
        {
            presenter.UndoButton_Click();
        }
        private void btn_Redo_Click(object sender, EventArgs e)
        {
            presenter.RedoButton_Click();
        }
        private void btn_Cut_Click(object sender, EventArgs e)
        {
            presenter.RichTextBox_Cut();
        }
        private void btn_Put_Click(object sender, EventArgs e)
        {
            presenter.RichTextBox_Paste();
        }
        private void btn_Copy_Click(object sender, EventArgs e)
        {
            presenter.RichTextBox_Copy();
        }
        private void tsmi_Delete_Click(object sender, EventArgs e)
        {
            presenter.RichTextBox_Delete();
        }
        private void tsmi_SelectAll_Click(object sender, EventArgs e)
        {
            presenter.RichTextBox_SelectAll();
        }
        private void btn_Light_Click(object sender, EventArgs e)
        {
            presenter.HighlightKeywords();
        }
        #endregion

        #region [ Справка ]
        private void btn_Info_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button b = (Button)sender;
                string name = b.Name;
                AuxiliaryForm af = new(name);
                af.ShowDialog();
            }
            else
            {

                ToolStripMenuItem t = (ToolStripMenuItem)sender;
                string name = t.Name;
                AuxiliaryForm af = new(name);
                af.ShowDialog();
            }
        }

        #endregion

        #region [ Локализация ]
        private void tsmi_Russian_Click(object sender, EventArgs e)
        {

            if (CultureInfo.CurrentCulture.Name == "en-US")
            {
                var changeLanguage = new ChangeLanguage();
                changeLanguage.UpdateConfig("language", "ru");
                DialogResult result = MessageBox.Show(
        MyString.MessageRestart,
        MyString.Message,
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1,
        MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes) presenter.RestartCompilyator();
                tsmi_Localization.Enabled = false;
            }
        }
        private void tsmi_English_Click(object sender, EventArgs e)
        {
            if (CultureInfo.CurrentCulture.Name == "ru-RU" || CultureInfo.CurrentCulture.Name == "ru")
            {
                var changeLanguage = new ChangeLanguage();
                changeLanguage.UpdateConfig("language", "en-US");
                DialogResult result = MessageBox.Show(
        MyString.MessageRestart,
        MyString.Message,
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1,
        MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes) presenter.RestartCompilyator();
                tsmi_Localization.Enabled = false;
            }
        }
        #endregion

        #region [ Вид ]
        private void tsmi_Font_Click(object sender, EventArgs e)
        {
            presenter.SetFont();
        }
        private void tsmi_FontOutput_Click(object sender, EventArgs e)
        {
            presenter.SetFontOutput();
        }
        private void tsmi_ColorFont_Click(object sender, EventArgs e)
        {
            presenter.SettingsColorFont();
        }
        private void tsmi_ColorKeywords_Click(object sender, EventArgs e)
        {
            presenter.SettingsColorAllKeywords();
        }
        #endregion

        #region [ Таймеры ]
        private void timer1_Tick(object sender, EventArgs e)
        {
            // if (presenter.IsHighLight == true) presenter.IsHighLight = false;
            datelabel.Text = DateTime.Now.ToLongDateString();
            timelabel.Text = DateTime.Now.ToLongTimeString();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (presenter.IsHighLight) presenter.IsHighLight = false;
        }
        #endregion

        #region [ Свойства Form ]

        // Состояние курсоры при перетаскивание файла
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // Перетаскивание файла в зону формы
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                presenter.IncludeTextFromFile(file);
            }

        }
        
        // Спрашиваем пользователя разрешения, сохраняем файлы и выходим  
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
        MyString.MessageExit,
        MyString.Message,
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1,
        MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else presenter.CLoseCompilyator();
        }

        #endregion

        #region [ Нажатие правой кнопкой мыши на вкладку ]
        private void TabControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tabControl.TabPages.Count; i++)
                {
                    if (tabControl.GetTabRect(i).Contains(e.Location))
                    {
                        ContextMenuStrip contextMenu = new ContextMenuStrip();
                        ToolStripMenuItem closeTabItem = new ToolStripMenuItem(MyString.CloseTab);
                        closeTabItem.Click += (s, ea) =>
                        {
                            presenter.CloseSelectedTab();
                        };
                        contextMenu.Items.Add(closeTabItem);
                        contextMenu.Show(tabControl, e.Location);

                        break;
                    }
                }
            }
        }
        #endregion

        #region [ Выбор вкладки ]
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count > 0) presenter.UpdateUndoRedoButtonStates();
            else
            {
                btn_Undo.Enabled = false;
                btn_Redo.Enabled = false;
            }
        }
        #endregion

        // Горячие клавиши
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    presenter.SaveFile();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Z)
                {
                    presenter.UndoButton_Click();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.X)
                {
                    presenter.RichTextBox_Cut();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.C)
                {
                    presenter.RichTextBox_Copy();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.V)
                {
                    presenter.RichTextBox_Paste();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.N)
                {
                    presenter.AddTabPage();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Y)
                {
                    presenter.RedoButton_Click();
                    e.Handled = true;
                }
            }
        }

        
    }
}