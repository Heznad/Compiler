
namespace Compiler.Model
{
    /* Класс для представление команды "Вставка"
     Нужен для Undo/Redo.
    Противоположная команда удалению*/
    class InsertTextCommand : Command
    {
        private readonly int _position; // Позиция где было команда
        private readonly string _text;  // Сохранённый текст

        public InsertTextCommand(int position, string text)
        {
            _position = position;
            _text = text;
        }

        // Выполнение команды
        public override void Execute(RichTextBox textBox)
        {
            textBox.SelectionStart = _position;
            textBox.SelectionLength = 0;
            textBox.SelectedText = _text;
        }

        // Обратная команды для помещения в Undo
        public override void Undo(RichTextBox textBox)
        {
            textBox.SelectionStart = _position;
            textBox.SelectionLength = _text.Length;
            textBox.SelectedText = "";
        }
    }
}
