namespace Compiler
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelTime.Text = DateTime.Now.ToLongDateString();
            toolStripStatusLabelDate.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
