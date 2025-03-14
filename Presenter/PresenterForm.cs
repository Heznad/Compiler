﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Compiler.Presenter
{
    public class PresenterForm
    {
        // Словарь для хранения стеков undo/redo для каждого RichTextBox
        private Dictionary<RichTextBox, Stack<string>> undoStacks = new Dictionary<RichTextBox, Stack<string>>();
        private Dictionary<RichTextBox, Stack<string>> redoStacks = new Dictionary<RichTextBox, Stack<string>>();
        private int maxUndoLevels = 100;
        // Флаг для предотвращения гонки потоков при быстром вводе
        private bool isProcessingText = false;
        private bool isUndoRedoInProgress = false; // Добавляем флаг для блокировки TextChanged во время Undo/Redo
        private Dictionary<TabPage, string> tabPageFilePaths = new Dictionary<TabPage, string>();
        string[] keywords = { "int", "if", "while", "void", "return", "bool", "char", "double", "float" };
        TabControl tabControl;
        Button btn_Undo;
        Button btn_Redo;
        private Font selectedFont = new Font("Verdana", 12F); // Значение по умолчанию
        private Color selectedColor = Color.Black;
        public PresenterForm(TabControl tabControl, Button btn_Undo, Button btn_Redo)
        {
            this.tabControl = tabControl;
            this.btn_Undo = btn_Undo;
            this.btn_Redo = btn_Redo;
        }

        public void AddTabPage(string name = "")
        {
            if (name == "") name = NameNewFile();
            if (name != "")
            {
                TabPage tabPage = new(name);
                SplitContainer splitContainer = new SplitContainer
                {
                    Orientation = Orientation.Horizontal,
                    Dock = DockStyle.Fill,
                    SplitterDistance = tabPage.Height / 2,
                    SplitterWidth = 10,
                    Margin = new Padding(3)

                };
                SplitContainer splitContainerP1 = new SplitContainer
                {
                    Orientation = Orientation.Vertical,
                    Dock = DockStyle.Fill,
                    SplitterWidth = 1,
                    SplitterDistance = 10
                };
                splitContainerP1.FixedPanel = FixedPanel.Panel1;
                RichTextBox lineNumberRichTextBox = new RichTextBox
                {
                    Dock = DockStyle.Left,
                    Width = 40,
                    BorderStyle = BorderStyle.None, // Убираем границу
                    BackColor = Color.White,
                    ForeColor = selectedColor,
                    Font = selectedFont,
                    Enabled = false,
                    Multiline = true, // Важно для отображения нескольких строк
                    ScrollBars = RichTextBoxScrollBars.None, // Убираем полосы прокрутки
                    WordWrap = false
                };

                RichTextBox richTextBox = new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    AcceptsTab = true,
                    BackColor = Color.White,
                    ForeColor = selectedColor,
                    Font = selectedFont,
                    AllowDrop = true
                };
                richTextBox.TextChanged += RichTextBox_TextChangedAsync;
                richTextBox.VScroll += RichTextBox_VScroll;
                richTextBox.DragEnter += RichTextBox_DragEnter;
                richTextBox.DragDrop += RichTextBox_DragDrop;
                undoStacks[richTextBox] = new Stack<string>();
                redoStacks[richTextBox] = new Stack<string>();

                splitContainerP1.Panel1.Controls.Add(lineNumberRichTextBox);
                splitContainerP1.Panel2.Controls.Add(richTextBox);

                DataGridView dataGridView = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    BackgroundColor = Color.White,
                    ReadOnly = true
                };

                splitContainer.Panel1.Controls.Add(splitContainerP1);
                splitContainer.Panel2.Controls.Add(dataGridView);

                tabPage.Controls.Add(splitContainer);

                tabControl.TabPages.Add(tabPage);
                tabControl.SelectedIndex = tabControl.TabCount - 1;
            }
        }
        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите текстовый файл";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    IncludeTextFromFile(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void IncludeTextFromFile(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string fileContent = File.ReadAllText(filePath);
            AddTabPage(fileName);
            TabPage tp = tabControl.TabPages[tabControl.TabCount - 1];
            RichTextBox richTextBox = GetSelectedRichTextBox(tp);
            richTextBox.Text = fileContent;
            tabPageFilePaths.Add(tp, filePath);
            tabControl.SelectedIndex = tabControl.TabCount - 1;
        }
        public void SaveFile()
        {
            if (tabControl.TabPages.Count != 0)
            {
                TabPage currentTabPage = tabControl.SelectedTab;
                if (!tabPageFilePaths.ContainsKey(currentTabPage) || string.IsNullOrEmpty(tabPageFilePaths[currentTabPage]))
                {
                    SaveFileAs(); // Если файл еще не был сохранен, показываем "Сохранить как..."
                }
                else
                {
                    string filePath = tabPageFilePaths[currentTabPage];
                    SaveTabPageContentToFile(currentTabPage, filePath);
                }
            }
            else return;
        }
        public void SaveFileAs()
        {
            if (tabControl.TabPages.Count != 0)
            {
                TabPage currentTabPage = tabControl.SelectedTab;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Сохранить как";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    SaveTabPageContentToFile(currentTabPage, filePath);
                    tabPageFilePaths[currentTabPage] = filePath; // Сохраняем путь к файлу
                    currentTabPage.Text = Path.GetFileName(filePath); // Обновляем заголовок вкладки
                }
            }
            else return;
        }
        public void CloseSelectedTab()
        {
            TabPage tabPageToClose = tabControl.SelectedTab;
            RichTextBox richTextBox = GetSelectedRichTextBox();

            if (richTextBox.Modified)
            {
                DialogResult result = MessageBox.Show($"Сохранить изменения в файле \"{tabPageToClose.Text}\" перед закрытием?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    tabControl.SelectedTab = tabPageToClose; 
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    return; 
                }
            }

            tabPageFilePaths.Remove(tabPageToClose);

            tabControl.TabPages.Remove(tabPageToClose);
        }
        public void CLoseCompilyator()
        {
            for(int i = 0;i<tabControl.Controls.Count;i++)
            {
                tabControl.SelectedIndex = i;

                TabPage tabPageToClose = tabControl.TabPages[i];
                RichTextBox richTextBox = GetSelectedRichTextBox(tabPageToClose);

                if (richTextBox.Modified)
                {
                    DialogResult result = MessageBox.Show($"Сохранить изменения в файле \"{tabPageToClose.Text}\" перед выходом?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        SaveFile();
                    }
                    else if (result == DialogResult.No)
                    {
                        if (i != tabControl.Controls.Count) continue;
                    }
                }
                tabPageFilePaths.Remove(tabPageToClose);
                tabControl.TabPages.Remove(tabPageToClose);
            }
            Application.Exit();
        }
        public async void UndoButton_Click()
        {
            RichTextBox currentTextBox = GetSelectedRichTextBox();
            if (currentTextBox == null) return;

            Stack<string> undoStack = undoStacks[currentTextBox];
            Stack<string> redoStack = redoStacks[currentTextBox];


            if (undoStack.Count > 1)
            {
                isUndoRedoInProgress = true; // Блокируем TextChanged
                try
                {
                    if (redoStack.Count == 0 && undoStack.Peek() == currentTextBox.Text) redoStack.Push(undoStack.Pop());
                    else redoStack.Push(currentTextBox.Text);
                    string previousState = undoStack.Pop();
                    currentTextBox.Text = previousState;
                    currentTextBox.SelectionStart = currentTextBox.TextLength;
                    currentTextBox.ScrollToCaret();
                    UpdateUndoRedoButtonStates();
                }
                finally
                {
                    isUndoRedoInProgress = false; // Разблокируем TextChanged
                }
            }
        }
        public async void RedoButton_Click()
        {
            RichTextBox currentTextBox = GetSelectedRichTextBox();
            if (currentTextBox == null) return;


            Stack<string> undoStack = undoStacks[currentTextBox];
            Stack<string> redoStack = redoStacks[currentTextBox];


            if (redoStack.Count > 0)
            {
                isUndoRedoInProgress = true; // Блокируем TextChanged
                try
                {
                    undoStack.Push(currentTextBox.Text);

                    string nextState = redoStack.Pop();
                    currentTextBox.Text = nextState;
                    currentTextBox.SelectionStart = currentTextBox.TextLength;
                    currentTextBox.ScrollToCaret();
                    UpdateUndoRedoButtonStates();
                }
                finally
                {
                    isUndoRedoInProgress = false; // Разблокируем TextChanged
                }
            }
        }

        public void UpdateUndoRedoButtonStates()
        {
            RichTextBox currentTextBox = GetSelectedRichTextBox();

            if (currentTextBox != null)
            {
                btn_Undo.Enabled = undoStacks[currentTextBox].Count > 1;
                btn_Redo.Enabled = redoStacks[currentTextBox].Count > 0;
            }
        }

        public void RichTextBox_Copy()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            // Проверяем, выделен ли текст
            if (richTextBox.SelectionLength > 0)
            {
                // Копируем выделенный текст в буфер обмена
                richTextBox.Copy();
            }
        }

        public void RichTextBox_Cut()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            // Проверяем, выделен ли текст
            if (richTextBox.SelectionLength > 0)
            {
                // Вырезаем выделенный текст в буфер обмена
                richTextBox.Cut();
            }
        }

        public void RichTextBox_Paste()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            richTextBox.Paste();
        }

        public void RichTextBox_Delete()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            // Проверяем, выделен ли текст
            if (richTextBox.SelectionLength > 0)
            {
                // Удаляем выделенный текст
                richTextBox.SelectedText = "";
            }
            else
            {
                // Если текст не выделен, удаляем символ справа от курсора
                if (richTextBox.SelectionStart < richTextBox.TextLength)
                {
                    richTextBox.SelectionLength = 1;
                    richTextBox.SelectedText = "";
                }
            }
        }

        public void RichTextBox_SelectAll()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            richTextBox.SelectAll();
        }

        #region Выбор Шрифта и Цвета
        public void SettingsFont()
        {
            // Диалог выбора шрифта
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = selectedFont; // Устанавливаем текущий шрифт

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFont = fontDialog.Font;
            }

            
            FontRTBs();
        }
        public void SettingsColorFont()
        {
            // Диалог выбора цвета
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = selectedColor; // Устанавливаем текущий цвет

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog.Color;
            }
            // Применяем новые настройки
            ColorFontRTBs();
        }
        #endregion

        private void SaveTabPageContentToFile(TabPage tabPage, string filePath)
        {
            try
            {
                RichTextBox richTextBox = GetSelectedRichTextBox();
                File.WriteAllText(filePath, richTextBox.Text);
                MessageBox.Show("Файл успешно сохранен.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTextBox.Modified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string NameNewFile()
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Введите название файла",
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 50, Top = 20, Text = "Название файла:" };
            TextBox textBox = new TextBox() { Text = "NewFile", Left = 50, Top = 50, Width = 200 };
            Button confirmation = new Button() { Text = "OK", Left = 50, Width = 100, Height = 30, Top = 90, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Отмена", Left = 150, Width = 100, Height = 30, Top = 90, DialogResult = DialogResult.Cancel };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);

            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
        private async void RichTextBox_TextChangedAsync(object sender, EventArgs e)
        {
            // Предотвращаем гонку потоков
            if (isProcessingText)
            {
                return;
            }

            isProcessingText = true;
            try
            {
                RichTextBox currentRichTextBox = (RichTextBox)sender;
                string currentText = currentRichTextBox.Text; // Получаем текст

                // Все операции в фоне
                await Task.Run(() => ProcessRichTextBoxChanges(currentRichTextBox));
            }
            finally
            {
                isProcessingText = false;
            }
        }

        private async void ProcessRichTextBoxChanges(RichTextBox currentRichTextBox)
        {
            // Выполняем операции, которые ранее были в RichTextBox_TextChanged
            // Все изменения UI нужно выполнять через Invoke

            // 1. Работа со стеками undo/redo (вызываем через Invoke)
            if (!isUndoRedoInProgress)
            {
               currentRichTextBox.Invoke((MethodInvoker)delegate { UndoRedoStacksWork(currentRichTextBox); });// Передаем текст, а не RichTextBox
            }      

            // 2. Обновление номеров строк (вызываем через Invoke)
            currentRichTextBox.Invoke((MethodInvoker)delegate { UpdateLineNumbers(); }); // Не передаем richtextbox, используем член класса lineNumberRichTextBox

            // 3. Подсветка ключевых слов (вызываем через Invoke)
            currentRichTextBox.Invoke((MethodInvoker)delegate { HighlightKeywords(currentRichTextBox, keywords, Color.DarkRed); });

            // 4. Установка флага Modified (вызываем через Invoke)
            currentRichTextBox.Invoke((MethodInvoker)delegate { currentRichTextBox.Modified = true; }); // Используем currentRichTextBox, а не this.richTextBox
        }

        private void UndoRedoStacksWork(RichTextBox richTextBox)
        {
            Stack<string> undoStack = undoStacks[richTextBox];
            Stack<string> redoStack = redoStacks[richTextBox];

            // 1. Проверка на изменение текста
            if (undoStack.Count == 0 || richTextBox.Text != undoStack.Peek())
            {
                // 2. Добавляем новое состояние в undoStack
                undoStack.Push(richTextBox.Text);
                UpdateUndoRedoButtonStates();

                // 3. Очищаем redoStack, так как произошло новое изменение
                redoStack.Clear();
            }

            // 4. Обрезка undoStack (после добавления нового состояния)
            if (undoStack.Count >= maxUndoLevels) // Используем > вместо >=, так как уже добавили элемент
            {
                // Получаем элементы из старого стека в прямом порядке (от старых к новым)
                string[] undoArray = undoStack.ToArray();
                undoStack.Clear();
                // Копируем только последние maxUndoLevels состояний в новый стек
                for (int i = undoArray.Length-2; i>-1; i--)
                {
                    undoStack.Push(undoArray[i]);
                }
            }
        }

        #region Прокрутка
        private void RichTextBox_VScroll(object sender, EventArgs e)
        {
            UpdateLineNumberScroll();
        }
        private void UpdateLineNumbers()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            RichTextBox lineNumberRichTextBox = GetlineNumberRichTextBox();

            lineNumberRichTextBox.Clear();
            int lineCount = richTextBox.Lines.Length;

            for (int i = 1; i <= lineCount; i++)
            {
                lineNumberRichTextBox.AppendText(i.ToString() + Environment.NewLine);
            }
            UpdateLineNumberScroll();
        }
        private void UpdateLineNumberScroll()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            RichTextBox lineNumberRichTextBox = GetlineNumberRichTextBox();
            if (richTextBox == null || lineNumberRichTextBox == null) return;

            try
            {
                int firstVisibleCharIndex = richTextBox.GetCharIndexFromPosition(new Point(0, 0));
                int firstVisibleLineNumber = richTextBox.GetLineFromCharIndex(firstVisibleCharIndex);

                if (firstVisibleLineNumber >= 0 && firstVisibleLineNumber < richTextBox.Lines.Length)
                {
                    // Get the starting index of the first visible line in the RichTextBox
                    int startOfLineIndex = richTextBox.GetFirstCharIndexFromLine(firstVisibleLineNumber);
                    lineNumberRichTextBox.SelectionStart = lineNumberRichTextBox.GetFirstCharIndexFromLine(firstVisibleLineNumber);
                    lineNumberRichTextBox.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateLineNumberScroll: {ex.Message}");
            }
        }
        private RichTextBox GetlineNumberRichTextBox(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            SplitContainer splitContainerP1 = (SplitContainer)splitContainer.Panel1.Controls[0];
            return (RichTextBox)splitContainerP1.Panel1.Controls[0];
        }
        #endregion

        private RichTextBox GetSelectedRichTextBox(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            SplitContainer splitContainerP1 = (SplitContainer)splitContainer.Panel1.Controls[0];
            return (RichTextBox)splitContainerP1.Panel2.Controls[0];
        }
        private void HighlightKeywords(RichTextBox richTextBox, string[] keywords, Color highlightColor)
        {
            if (richTextBox == null || keywords == null || keywords.Length == 0) return;

            // Сохраняем текущую позицию курсора и цвет, чтобы потом их восстановить
            int originalSelectionStart = richTextBox.SelectionStart;
            int originalSelectionLength = richTextBox.SelectionLength;
            Color originalColor = richTextBox.SelectionColor;

            try
            {
                foreach (string keyword in keywords)
                {
                    // Находим все вхождения ключевого слова
                    int start = 0;
                    while (start < richTextBox.TextLength)
                    {
                        int wordStartIndex = richTextBox.Find(keyword, start, RichTextBoxFinds.WholeWord);
                        if (wordStartIndex != -1)
                        {
                            // Подсвечиваем ключевое слово
                            richTextBox.SelectionStart = wordStartIndex;
                            richTextBox.SelectionLength = keyword.Length;
                            richTextBox.SelectionColor = highlightColor;

                            // Перемещаем позицию для следующего поиска
                            start = wordStartIndex + keyword.Length;
                        }
                        else
                        {
                            break; // Ключевое слово больше не найдено
                        }
                    }
                }
            }
            finally
            {
                // Восстанавливаем исходные настройки RichTextBox
                richTextBox.SelectionStart = originalSelectionStart;
                richTextBox.SelectionLength = originalSelectionLength;
                richTextBox.SelectionColor = originalColor;
            }
        }

        #region Изменение шрифта и цвета
        private void FontRTBs()
        {
            foreach (TabPage tab in tabControl.TabPages)
            {
                RichTextBox richTextBox = GetSelectedRichTextBox(tab);
                richTextBox.Font = selectedFont;
            }
            // Обновляем и RichTextBox нумерации строк, если он есть
            foreach (TabPage tab in tabControl.TabPages)
            {
                RichTextBox lineNumberRtb = GetlineNumberRichTextBox(tab);
                if (lineNumberRtb != null)
                {
                    lineNumberRtb.Font = selectedFont;
                }
            }
        }
        private void ColorFontRTBs()
        {
            foreach (TabPage tab in tabControl.TabPages)
            {
                RichTextBox richTextBox = GetSelectedRichTextBox(tab);
                richTextBox.ForeColor = selectedColor;
            }

            // Обновляем и RichTextBox нумерации строк, если он есть
            foreach (TabPage tab in tabControl.TabPages)
            {
                RichTextBox lineNumberRtb = GetlineNumberRichTextBox(tab);
                if (lineNumberRtb != null)
                {
                    lineNumberRtb.ForeColor = selectedColor;
                }
            }
        }
        #endregion


        private void RichTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void RichTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                IncludeTextFromFile(file);
            }
        }
    }
}
