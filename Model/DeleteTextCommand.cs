
namespace Compiler.Model
{
    /* Класс для представление команды "Вставка"
     Нужен для Undo/Redo.
    Противоположная команда удалению*/
    class DeleteTextCommand : Command
    {
        readonly int _position; // Позиция где было команда
        readonly string _text;  // Сохраненный текст, который был удален

        public DeleteTextCommand(int position, string text)
        {
            _position = position;
            _text = text;
        }

        // Выполнение команды
        public override void Execute(RichTextBox textBox)
        {
            textBox.SelectionStart = _position;
            textBox.SelectionLength = _text.Length;
            textBox.SelectedText = "";
        }

        // Обратная команды для помещения в Redo
        public override void Undo(RichTextBox textBox)
        {
            textBox.SelectionStart = _position;
            textBox.SelectionLength = 0;
            textBox.SelectedText = _text;
        }
    }
}
