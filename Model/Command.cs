
namespace Compiler.Model
{
    // Паттерн команда
    abstract class Command 
    {
        public abstract void Execute(RichTextBox textBox);
        public abstract void Undo(RichTextBox textBox);
    }
}
