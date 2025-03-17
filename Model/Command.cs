using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model
{
    abstract class Command
    {
        public abstract void Execute(RichTextBox textBox);
        public abstract void Undo(RichTextBox textBox);
    }
}
