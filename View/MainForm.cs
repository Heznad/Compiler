using System.Linq;
using System.Windows.Forms;
using Compiler.Presenter;

namespace Compiler
{
    public partial class MainForm : Form
    {
        PresenterForm presenter;
        public MainForm()
        {
            InitializeComponent();
            presenter = new(this, tabControl);
            timer1.Start();
        }

        private void btn_File_Click(object sender, EventArgs e)
        {
            presenter.AddTabPage();
        }

        private void btn_Folder_Click(object sender, EventArgs e)
        {
            presenter.OpenFile();
        }

        private void btn_Save_Click(object sender,EventArgs e)
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

    }
}



