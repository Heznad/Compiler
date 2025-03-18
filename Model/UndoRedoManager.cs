using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model
{
    // Словарь для управления стэками Undo/Redo
    class UndoRedoManager
    {
        // Словарь для хранения стеков undo/redo для каждого RichTextBox
        Dictionary<RichTextBox, Stack<Command>> _undoStacks = new Dictionary<RichTextBox, Stack<Command>>();
        Dictionary<RichTextBox, Stack<Command>> _redoStacks = new Dictionary<RichTextBox, Stack<Command>>();
        int _maxVolumeStack = 200; // Размер стэков Undo/Redo
        string _lastText = ""; // Сохраняем предыдущий текст
        int _lastTextLength = 0; // размер прошлого текста
        bool _isPerformingUndoRedo = false;
        RichTextBox _currentRichTextBox;
        Button _btnUndo;
        Button _btnRedo;
        
        public UndoRedoManager(Button BtnUndo, Button BtnRedo)
        {
            _btnUndo = BtnUndo;
            _btnRedo = BtnRedo;
        }

        public Dictionary<RichTextBox, Stack<Command>> UndoStacks { get => _undoStacks; set => _undoStacks = value; }
        public Dictionary<RichTextBox, Stack<Command>> RedoStacks { get => _redoStacks; set => _redoStacks = value; }
        public string LastText { get => _lastText; set => _lastText = value; }
        public bool IsPerformingUndoRedo { get => _isPerformingUndoRedo; set => _isPerformingUndoRedo = value; }
        public RichTextBox CurrentRichTextBox { get => _currentRichTextBox; set => _currentRichTextBox = value; }

        // Выполнение команды
        private void ExecuteCommand(Command command)
        {
            Stack<Command> undoStack = _undoStacks[_currentRichTextBox];
            Stack<Command> redoStack = _redoStacks[_currentRichTextBox];
            undoStack.Push(command);
            if (undoStack.Count > _maxVolumeStack)
            {
                Stack<Command> tempStack = new Stack<Command>();
                while (undoStack.Count > 1)
                {
                    tempStack.Push(undoStack.Pop());
                }
                undoStack.Pop();
                while (tempStack.Count > 0)
                {
                    undoStack.Push(tempStack.Pop());
                }
            }
            redoStack.Clear();
            UpdateUndoRedoButtonStates();
        }

        // Нажатие на кнопку "Отменить" 
        public void UndoButton_Click()
        {
            Stack<Command> undoStack = _undoStacks[_currentRichTextBox];
            Stack<Command> redoStack = _redoStacks[_currentRichTextBox];
            if (undoStack.Count > 0)
            {
                _isPerformingUndoRedo = true;  // Предотвращаем запись операций во время Undo
                Command command = undoStack.Pop();
                command.Undo(_currentRichTextBox);
                redoStack.Push(command);  // Перемещаем в Redo
                if (redoStack.Count > _maxVolumeStack)
                {
                    Stack<Command> tempStack = new Stack<Command>();
                    while (redoStack.Count > 1)
                    {
                        tempStack.Push(redoStack.Pop());
                    }
                    redoStack.Pop();
                    while (tempStack.Count > 0)
                    {
                        redoStack.Push(tempStack.Pop());
                    }
                }
                UpdateUndoRedoButtonStates();
                _isPerformingUndoRedo = false;
            }
            _lastTextLength = _currentRichTextBox.TextLength;
            _lastText = _currentRichTextBox.Text;

        }

        // Нажатие на кнопку "Повторить" 
        public void RedoButton_Click()
        {
            Stack<Command> undoStack = _undoStacks[_currentRichTextBox];
            Stack<Command> redoStack = _redoStacks[_currentRichTextBox];
            if (redoStack.Count > 0)
            {
                _isPerformingUndoRedo = true;
                Command command = redoStack.Pop();
                command.Execute(_currentRichTextBox);
                undoStack.Push(command);
                if (undoStack.Count > _maxVolumeStack)
                {
                    Stack<Command> tempStack = new Stack<Command>();
                    while (undoStack.Count > 1)
                    {
                        tempStack.Push(undoStack.Pop());
                    }
                    undoStack.Pop();
                    while (tempStack.Count > 0)
                    {
                        undoStack.Push(tempStack.Pop());
                    }
                }
                UpdateUndoRedoButtonStates();
                _isPerformingUndoRedo = false;
            }

            _lastTextLength = _currentRichTextBox.TextLength;
            _lastText = _currentRichTextBox.Text;
        }

        // Изменение статуса кнопок 
        public void UpdateUndoRedoButtonStates()
        {
            if (_currentRichTextBox != null)
            {
                _btnUndo.Enabled = _undoStacks[_currentRichTextBox].Count > 0;
                _btnRedo.Enabled = _redoStacks[_currentRichTextBox].Count > 0;
            }
            else
            {
                _btnUndo.Enabled = false;
                _btnRedo.Enabled = false;
            }
        }

        // Заполнение стэков Undo и Redo
        public void UndoRedoStacksWork()
        {
            Stack<Command> undoStack = _undoStacks[this._currentRichTextBox];
            Stack<Command> redoStack = _redoStacks[this._currentRichTextBox];

            if (this._currentRichTextBox.TextLength > _lastTextLength)
            {
                // Вставка текста
                int insertPosition = _currentRichTextBox.SelectionStart - (_currentRichTextBox.TextLength - _lastTextLength);
                int insertLength = _currentRichTextBox.TextLength - _lastTextLength;
                string insertedText = _currentRichTextBox.Text.Substring(insertPosition, insertLength);

                InsertTextCommand command = new InsertTextCommand(insertPosition, insertedText);
                ExecuteCommand(command);
            }
            else if (_currentRichTextBox.TextLength < _lastTextLength)
            {
                int deletePosition = _currentRichTextBox.SelectionStart;
                int deleteLength = _lastTextLength - _currentRichTextBox.TextLength;
                string deletedText = _lastText;
                if (deletePosition >= 0 && deletePosition + deleteLength <= deletedText.Length)
                    deletedText = deletedText.Substring(deletePosition, deleteLength);
                else
                    deletedText = "";

                DeleteTextCommand command = new DeleteTextCommand(deletePosition, deletedText);
                ExecuteCommand(command);

            }

            _lastTextLength = _currentRichTextBox.TextLength;
            _lastText = _currentRichTextBox.Text;
        }
    }
}
