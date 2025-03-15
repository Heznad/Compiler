using System.Linq;
using System.Windows.Forms;
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
            presenter = new(tabControl, btn_Undo, btn_Redo);
            timer1.Start();
            presenter.AddTabPage("NewFile");
            presenter.UpdateUndoRedoButtonStates();
        }

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
            presenter.CLoseCompilyator();
        }
        private void TabControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tabControl.TabPages.Count; i++)
                {
                    if (tabControl.GetTabRect(i).Contains(e.Location))
                    {
                        ContextMenuStrip contextMenu = new ContextMenuStrip();
                        ToolStripMenuItem closeTabItem = new ToolStripMenuItem("Закрыть вкладку");
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            datelabel.Text = DateTime.Now.ToLongDateString();
            timelabel.Text = DateTime.Now.ToLongTimeString();
        }

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

        private void tsmi_Font_Click(object sender, EventArgs e)
        {
            presenter.SettingsFont();
        }

        private void tsmi_ColorFont_Click(object sender, EventArgs e)
        {
            presenter.SettingsColorFont();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            // Проверяем, что перетаскиваемые данные - файлы
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Разрешаем перетаскивание (курсор изменится)
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                // Запрещаем перетаскивание (курсор останется обычным)
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            // Получаем список перетаскиваемых файлов
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            // Обрабатываем каждый файл
            foreach (string file in files)
            {
                presenter.IncludeTextFromFile(file);
            }

        }

        private void btn_Undo_Click(object sender, EventArgs e)
        {
            presenter.UndoButton_Click();
        }

        private void btn_Redo_Click(object sender, EventArgs e)
        {
            presenter.RedoButton_Click();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count > 0) presenter.UpdateUndoRedoButtonStates();
            else
            {
                btn_Undo.Enabled = false;
                btn_Redo.Enabled = false;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            presenter.CLoseCompilyator();
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
    }
}