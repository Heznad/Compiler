using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model
{
    class InsertTextCommand : Command
    {
        private readonly int _position;
        private readonly string _text;

        public InsertTextCommand(int position, string text)
        {
            _position = position;
            _text = text;
        }

        public override void Execute(RichTextBox textBox)
        {
            textBox.SelectionStart = _position;
            textBox.SelectionLength = 0;
            textBox.SelectedText = _text;
        }

        public override void Undo(RichTextBox textBox)
        {
            textBox.SelectionStart = _position;
            textBox.SelectionLength = _text.Length;
            textBox.SelectedText = "";
        }
    }
}
