using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            richTextBox.Text = "\tСправка";
        }
        private void InfoForm()
        {
            richTextBox.Text = "\tТема: Разработка пользовательского интерфейса (GUI) для языкового процессора.\r\n" +
                "\r\nДанная лабораторная работа является практической частью курсовой работы по дисциплине \"Теория формальных языков и компиляторов\"." +
                "\r\n\r\nЦель работы: Разработать приложение – текстовый редактор. ";
        }
    }
}
