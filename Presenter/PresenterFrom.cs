using System.Windows.Forms;
using Compiler.Model;
using System.Text.RegularExpressions;
namespace Compiler.Presenter
{
    public class PresenterForm
    {
        TextManager text_manager = new();
        // Словарь для хранения путей для сохранения файлов
        private Dictionary<TabPage, string> tabPageFilePaths = new Dictionary<TabPage, string>();
        // Словарь для хранения стеков undo/redo для каждого RichTextBox
        private Dictionary<RichTextBox, Stack<Command>> _undoStacks = new Dictionary<RichTextBox, Stack<Command>>();
        private Dictionary<RichTextBox, Stack<Command>> _redoStacks = new Dictionary<RichTextBox, Stack<Command>>();
        private string _lastText = ""; // Сохраняем предыдущий текст
        private int _lastTextLength = 0;
        private bool isProcessingText = false; // Флаг для предотвращения гонки потоков при быстром вводе
        private bool _isPerformingUndoRedo = false; // Добавляем флаг для блокировки TextChanged во время Undo/Redo
        private bool _isHighLight = false; // Подсветка текста
        // Изменяемые элементы UI
        TabControl _tabControl;
        Button _btnUndo;
        Button _btnRedo;
        
        public bool IsHighLight { get => _isHighLight; set => _isHighLight = value; }

        public PresenterForm(TabControl tabControl, Button btn_Undo, Button btn_Redo)
        {
            _tabControl = tabControl;
            _btnUndo = btn_Undo;
            _btnRedo = btn_Redo;
        }


        #region [ Отслеживание изменения состояния текстового редактора ]

        // Проверяем текстовое поле, запуская дочерний поток
        public async void RichTextBox_TextChangedAsync(object sender, EventArgs e)
        {
            if (isProcessingText)
            {
                return;
            }
            isProcessingText = true;
            try
            {
                RichTextBox currentRichTextBox = (RichTextBox)sender;
                await Task.Run(() => ProcessRichTextBoxChanges(currentRichTextBox));
            }
            finally
            {
                isProcessingText = false;
            }
        }

        // Стэки, прокрутка, подсветка, статутус rtb
        private async void ProcessRichTextBoxChanges(RichTextBox currentRichTextBox)
        {

            if (!_isPerformingUndoRedo)
            {
                await Task.Run(() =>
                {
                    currentRichTextBox.Invoke((MethodInvoker)delegate { UndoRedoStacksWork(currentRichTextBox); });
                }
                );
            }

            currentRichTextBox.Invoke((MethodInvoker)delegate { UpdateLineNumbers(); });

            if (!_isHighLight)
            {
                await Task.Run(() =>
                {
                    currentRichTextBox.Invoke((MethodInvoker)delegate { HighlightKeywords(); });
                }
                );
            }

            currentRichTextBox.Invoke((MethodInvoker)delegate { currentRichTextBox.Modified = true; });
        }

        // Запрет на прокрутку с помощью колёсика мыши
        private void RichTextBox_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                ((HandledMouseEventArgs)e).Handled = true;
            }
        }

        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            _lastText = richTextBox.Text;
        }
        #endregion


        #region [ Создание вкладки ]

        // Метод для создание TabPage с моими элементами
        public void AddTabPage(string name = "")
        {
            if (name == "") name = NameNewFile();
            if (name != "")
            {
                InitializeTabPage(name);
                _tabControl.SelectedIndex = _tabControl.TabCount - 1;
            }
        }

        // Инициализация элементов UI в TabPage
        private void InitializeTabPage(string name)
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
                SplitterDistance = 20
            };
            splitContainerP1.FixedPanel = FixedPanel.Panel1;
            RichTextBox lineNumberRichTextBox = new RichTextBox
            {
                Dock = DockStyle.Left,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                ForeColor = text_manager.SelectedColor,
                Font = text_manager.SelectedFont,
                Enabled = false,
                Multiline = true,
                ScrollBars = RichTextBoxScrollBars.None,
                WordWrap = false
            };
            RichTextBox richTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                AcceptsTab = true,
                BackColor = Color.White,
                ForeColor = text_manager.SelectedColor,
                Font = text_manager.SelectedFont,
                AllowDrop = true
            };
            richTextBox.TextChanged += RichTextBox_TextChangedAsync;
            richTextBox.VScroll += RichTextBox_VScroll;
            richTextBox.DragEnter += RichTextBox_DragEnter;
            richTextBox.DragDrop += RichTextBox_DragDrop;
            richTextBox.MouseWheel += RichTextBox_MouseWheel;
            richTextBox.KeyDown += richTextBox_KeyDown;
            _undoStacks[richTextBox] = new Stack<Command>();
            _redoStacks[richTextBox] = new Stack<Command>();
            splitContainerP1.Panel1.Controls.Add(lineNumberRichTextBox);
            splitContainerP1.Panel2.Controls.Add(richTextBox);

            DataGridView dataGridView = InitializeDataGridView();

            splitContainer.Panel1.Controls.Add(splitContainerP1);
            splitContainer.Panel2.Controls.Add(dataGridView);

            tabPage.Controls.Add(splitContainer);

            _tabControl.TabPages.Add(tabPage);
        }

        private DataGridView InitializeDataGridView()
        {
            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                Font = text_manager.SelectedFontOutput,
                ForeColor = text_manager.SelectedColor,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                ReadOnly = true,
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
            };
            // Отключаем автоматическое создание колонок.
            dataGridView.AutoGenerateColumns = false;

            // Добавление колонок
            DataGridViewTextBoxColumn lineNumberColumn = new DataGridViewTextBoxColumn();
            lineNumberColumn.HeaderText = ""; // № строк, который вы добавляете сами, без заголовка
            lineNumberColumn.Width = 30; // Задайте нужную ширину
            lineNumberColumn.ReadOnly = true; // Чтобы нельзя было редактировать столбец с номером строки.
            dataGridView.Columns.Add(lineNumberColumn);

            DataGridViewTextBoxColumn filePathColumn = new DataGridViewTextBoxColumn();
            filePathColumn.HeaderText = MyString.dgvFilePath;
            filePathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns.Add(filePathColumn);

            DataGridViewTextBoxColumn lineColumn = new DataGridViewTextBoxColumn();
            lineColumn.HeaderText = MyString.dgvLine;
            lineColumn.Width = 50; // Задайте нужную ширину
            dataGridView.Columns.Add(lineColumn);

            DataGridViewTextBoxColumn columnColumn = new DataGridViewTextBoxColumn();
            columnColumn.HeaderText = MyString.dgvColumn;
            columnColumn.Width = 60; // Задайте нужную ширину
            dataGridView.Columns.Add(columnColumn);

            DataGridViewTextBoxColumn messageColumn = new DataGridViewTextBoxColumn();
            messageColumn.HeaderText = MyString.dgvMessage;
            messageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns.Add(messageColumn);

            dataGridView.Rows.Add("1", @"D:\clang\project\StaticAnalyzer\StaticAnalyzer\codeTests.cpp", "28", "27", "Possible null pointer dereference: file");
            dataGridView.Rows.Add("2", @"D:\clang\project\StaticAnalyzer\StaticAnalyzer\codeTests.cpp", "28", "27", "Uninitialized pointer: file");

            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray; // Чередование цветов строк
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke; // Цвет фона заголовков
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            filePathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            return dataGridView;
        }

        // Появление диалогового окна для задания имени файла
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

            Label textLabel = new Label() { Left = 50, Top = 20, Text = MyString.NameFile };
            TextBox textBox = new TextBox() { Text = "NewFile", Left = 50, Top = 50, Width = 200 };
            Button confirmation = new Button() { Text = MyString.OK, Left = 50, Width = 100, Height = 30, Top = 90, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = MyString.Cancel, Left = 150, Width = 100, Height = 30, Top = 90, DialogResult = DialogResult.Cancel };

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
        #endregion


        #region [ Открытие файла ]

        // Методы для открытия файла с считыванием его содержимого
        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.Title = MyString.ChooseFile;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    IncludeTextFromFile(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(MyString.ErrorOpenFile + ex.Message, MyString.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Считывание файла, запоминание пути
        public void IncludeTextFromFile(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string fileContent = File.ReadAllText(filePath);
            AddTabPage(fileName);
            TabPage tp = _tabControl.TabPages[_tabControl.TabCount - 1];
            RichTextBox richTextBox = GetSelectedRichTextBox(tp);
            richTextBox.Text = fileContent;
            richTextBox.SelectionStart = richTextBox.TextLength;
            tabPageFilePaths.Add(tp, filePath);
            _tabControl.SelectedIndex = _tabControl.TabCount - 1;
        }
        
        // Отображение курсора при наведении на RichTextBox при перетаскивания файла 
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

        // Возможность перетащить файл и открыть его
        private void RichTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                IncludeTextFromFile(file);
            }
        }

        #endregion


        #region [ Сохранение файлов ]

        // Сохранить файл
        public void SaveFile()
        {
            if (_tabControl.TabPages.Count != 0)
            {
                TabPage currentTabPage = _tabControl.SelectedTab;
                if (!tabPageFilePaths.ContainsKey(currentTabPage) || string.IsNullOrEmpty(tabPageFilePaths[currentTabPage]))
                {
                    SaveFileAs(); 
                }
                else
                {
                    string filePath = tabPageFilePaths[currentTabPage];
                    SaveTabPageContentToFile(currentTabPage, filePath);
                }
            }
            else return;
        }

        // Создание нового файла с именем вкладки и последующим сохранением
        public void SaveFileAs()
        {
            if (_tabControl.TabPages.Count != 0)
            {
                TabPage currentTabPage = _tabControl.SelectedTab;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
                saveFileDialog.Title = MyString.SaveAs;

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

        // Сохранение содержимого TabPage в файл
        private void SaveTabPageContentToFile(TabPage tabPage, string filePath)
        {
            try
            {
                RichTextBox richTextBox = GetSelectedRichTextBox();
                File.WriteAllText(filePath, richTextBox.Text);
                MessageBox.Show(MyString.SuccessFileSave, MyString.Saving, MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTextBox.Modified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MyString.ErrorSaveFile + ex.Message , MyString.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region [ Закрыть вкладку/приложение ] 

        // Закрыть выбранную вкладку
        public void CloseSelectedTab()
        {
            TabPage tabPageToClose = _tabControl.SelectedTab;
            RichTextBox richTextBox = GetSelectedRichTextBox();

            if (richTextBox.Modified)
            {
                DialogResult result = MessageBox.Show(MyString.SavingChangesFile + $"\"{tabPageToClose.Text}\"", MyString.Saving, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _tabControl.SelectedTab = tabPageToClose;
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            tabPageFilePaths.Remove(tabPageToClose);
            _tabControl.TabPages.Remove(tabPageToClose);
        }

        // Закрыть приложение
        public void CLoseCompilyator()
        {
            for (int i = _tabControl.Controls.Count; i >0 ; i--)
            {
                _tabControl.SelectedIndex = i;

                TabPage tabPageToClose = _tabControl.TabPages[i-1];
                RichTextBox richTextBox = GetSelectedRichTextBox(tabPageToClose);

                if (richTextBox.Modified)
                {
                    DialogResult result = MessageBox.Show( MyString.SavingChangesFile + $"\"{tabPageToClose.Text}\"", MyString.Saving, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        SaveFile();
                    }
                    else if (result == DialogResult.No)
                    {
                        if (i != _tabControl.Controls.Count) continue;
                    }
                }
                tabPageFilePaths.Remove(tabPageToClose);
                _tabControl.TabPages.Remove(tabPageToClose);
            }
            Application.Exit();
        }

        #endregion


        #region [ Отменить/Повторить ]

        private void ExecuteCommand(Command command)
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            Stack<Command> undoStack = _undoStacks[richTextBox];
            Stack<Command> redoStack = _redoStacks[richTextBox];
            undoStack.Push(command);
            redoStack.Clear();
            UpdateUndoRedoButtonStates();
        }
        // Нажатие на кнопку "Отменить" 
        public void UndoButton_Click()
        {
            RichTextBox currentTextBox = GetSelectedRichTextBox();
            Stack<Command> undoStack = _undoStacks[currentTextBox];
            Stack<Command> redoStack = _redoStacks[currentTextBox];
            if (undoStack.Count > 0)
            {
                _isPerformingUndoRedo = true;  // Предотвращаем запись операций во время Undo
                Command command = undoStack.Pop();
                command.Undo(currentTextBox);
                redoStack.Push(command);  // Перемещаем в Redo
                UpdateUndoRedoButtonStates();
                _isPerformingUndoRedo = false;
            }
            _lastTextLength = currentTextBox.TextLength;
            _lastText = currentTextBox.Text;

        }

        // Нажатие на кнопку "Повторить" 
        public void RedoButton_Click()
        {
            RichTextBox currentTextBox = GetSelectedRichTextBox();
            Stack<Command> undoStack = _undoStacks[currentTextBox];
            Stack<Command> redoStack = _redoStacks[currentTextBox];
            if (redoStack.Count > 0)
            {
                _isPerformingUndoRedo = true; 
                Command command = redoStack.Pop();
                command.Execute(currentTextBox);
                undoStack.Push(command); 
                UpdateUndoRedoButtonStates();
                _isPerformingUndoRedo = false;
            }

            _lastTextLength = currentTextBox.TextLength;
            _lastText = currentTextBox.Text;
        }

        // Изменение статуса кнопок 
        public void UpdateUndoRedoButtonStates()
        {
            RichTextBox currentTextBox = GetSelectedRichTextBox();
            if (currentTextBox != null)
            {
                _btnUndo.Enabled = _undoStacks[currentTextBox].Count > 0;
                _btnRedo.Enabled = _redoStacks[currentTextBox].Count > 0;
            }
            else
            {
                _btnUndo.Enabled = false;
                _btnRedo.Enabled = false;
            }
        }

        // Заполнение стэков Undo и Redo
        private void UndoRedoStacksWork(RichTextBox richTextBox)
        {
            Stack<Command> undoStack = _undoStacks[richTextBox];
            Stack<Command> redoStack = _redoStacks[richTextBox];

            if (richTextBox.TextLength > _lastTextLength)
            {
                // Вставка текста
                int insertPosition = richTextBox.SelectionStart - (richTextBox.TextLength - _lastTextLength);
                int insertLength = richTextBox.TextLength - _lastTextLength;
                string insertedText = richTextBox.Text.Substring(insertPosition, insertLength);

                InsertTextCommand command = new InsertTextCommand(insertPosition, insertedText);
                ExecuteCommand(command);
            }
            else if (richTextBox.TextLength < _lastTextLength)
            {
                int deletePosition = richTextBox.SelectionStart;
                int deleteLength = _lastTextLength - richTextBox.TextLength;
                string deletedText = _lastText;
                if (deletePosition >= 0 && deletePosition + deleteLength <= deletedText.Length)
                    deletedText = deletedText.Substring(deletePosition, deleteLength);
                else
                    deletedText = ""; 

                DeleteTextCommand command = new DeleteTextCommand(deletePosition, deletedText);
                ExecuteCommand(command);

            }

            _lastTextLength = richTextBox.TextLength;
            _lastText = richTextBox.Text;
        }
        #endregion


        #region [ Копировать/Вырезать/Вставить/Удалить/Выделить_всё ]

        // Копировать
        public void RichTextBox_Copy()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            if (richTextBox.SelectionLength > 0)
            {
                richTextBox.Copy();
            }
        }

        // Вырезать
        public void RichTextBox_Cut()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            if (richTextBox.SelectionLength > 0)
            {
                richTextBox.Cut();
            }
        }

        // Вставить
        public void RichTextBox_Paste()
        {
            if (Clipboard.ContainsText())
            {
                RichTextBox richTextBox = GetSelectedRichTextBox();
                string clipboardText = Clipboard.GetText();
                richTextBox.SelectedText = clipboardText;
            }
        }

        // Удалить
        public void RichTextBox_Delete()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            if (richTextBox.SelectionLength > 0)
            {
                richTextBox.SelectedText = "";
            }
            else
            {
                if (richTextBox.SelectionStart < richTextBox.TextLength)
                {
                    richTextBox.SelectionLength = 1;
                    richTextBox.SelectedText = "";
                }
            }
        }

        // Выделить всё
        public void RichTextBox_SelectAll()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            richTextBox.SelectAll();
        }

        #endregion


        #region [ Шрифт/Цвет/Подсветка ]

        // Выбор шрифта
        public void SetFont()
        {
            text_manager.SettingsFont();
            FontRTBs();
        }
        public void SetFontOutput()
        {
            text_manager.SettingsFontOutput();
            TabPage currentTabPage = _tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)currentTabPage.Controls[0];
            DataGridView dataGridView = (DataGridView)splitContainer.Panel2.Controls[0];
            dataGridView.Font = text_manager.SelectedFontOutput;
        }
        public void SettingsColorAllKeywords()
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 300,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = MyString.ChooseColor,
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 30, Top = 20, Text = MyString.ChangeColorKeyword,AutoSize = true};
            Label currentLabelColor = new Label() { Left = 70, Top = 50, Text = MyString.CurrentColor, AutoSize = true };
            Panel pKeywords = new Panel() { BorderStyle = BorderStyle.Fixed3D,BackColor = text_manager.KeywordCategories["Keywords"], Width = 40, Height = 40, Left = 100, Top = 80 };
            Button btn_Keywords = new Button() { Text = MyString.Keywords, Left = 275, Width = 125, Height = 40, Top = 80 };
            Panel pTypes = new Panel() { BorderStyle = BorderStyle.Fixed3D, BackColor = text_manager.KeywordCategories["TypesData"], Width = 40, Height = 40, Left = 100, Top = 130 };
            Button btn_Types = new Button() { Text = MyString.TypesOfData, Left = 275, Width = 125, Height = 40, Top = 130 };
            Panel pOperators = new Panel() { BorderStyle = BorderStyle.Fixed3D, BackColor = text_manager.KeywordCategories["Operators"], Width = 40, Height = 40, Left = 100, Top = 180 };
            Button btn_Operators = new Button() { Text = MyString.Operators, Left = 275, Width = 125, Height = 40, Top = 180 };


            btn_Keywords.Click += (sender, e) => { prompt.Close(); SettingsColorKeywords(); };
            btn_Types.Click += (sender, e) => { prompt.Close(); SettingsColorTypes(); };
            btn_Operators.Click += (sender, e) => { prompt.Close(); SettingsColorOperators(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(currentLabelColor);
            prompt.Controls.Add(pKeywords);
            prompt.Controls.Add(btn_Keywords);
            prompt.Controls.Add(pTypes);
            prompt.Controls.Add(btn_Types);
            prompt.Controls.Add(pOperators);
            prompt.Controls.Add(btn_Operators);
            prompt.ShowDialog();
        }

        #region [ Цвет ключевых слов]

        // Типы данных
        public void SettingsColorTypes()
        {
            text_manager.SettingsColorFont(text_manager.KeywordCategories["TypesData"]);
            //HighlightKeywords(GetSelectedRichTextBox(), text_manager.KeywordsTypes, text_manager.ColorTypes);
        }

        // Операторы
        public void SettingsColorOperators()
        {
            text_manager.SettingsColorFont(text_manager.KeywordCategories["Operators"]);
            //HighlightKeywords(GetSelectedRichTextBox(), text_manager.KeywordsOperators, text_manager.ColorOperators);
        }

        // Перечисление
        public void SettingsColorKeywords()
        {
            text_manager.SettingsColorFont(text_manager.KeywordCategories["Keywords"]);
            //HighlightKeywords();
        }

        #endregion

        // Выбор цвета
        public void SettingsColorFont()
        {
            text_manager.SettingsColorFont(text_manager.SelectedColor);
            ColorFontRTBs();
        }

        // Установка шрифта
        private void FontRTBs()
        {
            foreach (TabPage tab in _tabControl.TabPages)
            {
                RichTextBox richTextBox = GetSelectedRichTextBox(tab);
                richTextBox.Font = text_manager.SelectedFont;
            }
            // Обновляем и RichTextBox нумерации строк, если он есть
            foreach (TabPage tab in _tabControl.TabPages)
            {
                RichTextBox lineNumberRtb = GetlineNumberRichTextBox(tab);
                if (lineNumberRtb != null)
                {
                    lineNumberRtb.Font = text_manager.SelectedFont;
                }
            }
        }

        // Установка цвета
        private void ColorFontRTBs()
        {
            foreach (TabPage tab in _tabControl.TabPages)
            {
                RichTextBox richTextBox = GetSelectedRichTextBox(tab);
                richTextBox.ForeColor = text_manager.SelectedColor;
            }

            // Обновляем и RichTextBox нумерации строк, если он есть
            foreach (TabPage tab in _tabControl.TabPages)
            {
                RichTextBox lineNumberRtb = GetlineNumberRichTextBox(tab);
                if (lineNumberRtb != null)
                {
                    lineNumberRtb.ForeColor = text_manager.SelectedColor;
                }
            }
        }

        private void HighlightKeywords()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            int selectionStart = richTextBox.SelectionStart;
            int selectionLength = richTextBox.SelectionLength;
            _isHighLight = true;
            richTextBox.SuspendLayout();

            // Создаем HashSet для хранения индексов уже подсвеченных слов
            HashSet<int> highlightedIndices = new HashSet<int>();

            richTextBox.SelectionStart = 0;
            richTextBox.SelectionLength = richTextBox.TextLength;
            richTextBox.SelectionColor = text_manager.SelectedColor;

            // Перебираем каждую категорию ключевых слов
            foreach (var category in text_manager.Keywords)
            {
                string categoryName = category.Key;
                List<string> keywordList = category.Value;
                Color keywordColor = text_manager.KeywordCategories[categoryName];  // Получаем цвет для данной категории

                // Перебираем ключевые слова в категории
                foreach (string keyword in keywordList)
                {
                    int index = 0;
                    while (index < richTextBox.Text.Length)
                    {
                        index = richTextBox.Text.IndexOf(keyword, index, StringComparison.OrdinalIgnoreCase);
                        if (index >= 0)
                        {
                            // Проверяем, чтобы слово было целым и не было частью другого слова
                            if ((index == 0 || !char.IsLetterOrDigit(richTextBox.Text[index - 1])) &&
                                (index + keyword.Length == richTextBox.Text.Length || !char.IsLetterOrDigit(richTextBox.Text[index + keyword.Length])))
                            {
                                bool alreadyHighlighted = false;
                                // Проверяем, чтобы этот индекс не был уже подсвечен
                                for (int i = index; i < index + keyword.Length; i++)
                                {
                                    if (highlightedIndices.Contains(i))
                                    {
                                        alreadyHighlighted = true;
                                        break;
                                    }
                                }

                                if (!alreadyHighlighted)
                                {
                                    richTextBox.SelectionStart = index;
                                    richTextBox.SelectionLength = keyword.Length;
                                    richTextBox.SelectionColor = keywordColor;  // Используем цвет для данной категории

                                    // Добавляем индексы подсвеченного слова в HashSet
                                    for (int i = index; i < index + keyword.Length; i++)
                                    {
                                        highlightedIndices.Add(i);
                                    }
                                }
                            }
                            index += keyword.Length;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            richTextBox.SelectionStart = selectionStart;
            richTextBox.SelectionLength = selectionLength;
            richTextBox.SelectionColor = text_manager.SelectedColor;
            richTextBox.ResumeLayout();
            //richTextBox.Focus();
        }



        #endregion


        #region [ Прокрутка ]

        // Событие прокурутки скролла у текстовго поля
        private void RichTextBox_VScroll(object sender, EventArgs e)
        {
            UpdateLineNumberScroll();
        }

        private void UpdateLineNumbers()
        {
            RichTextBox richTextBox = GetSelectedRichTextBox();
            RichTextBox lineNumberRichTextBox = GetlineNumberRichTextBox();
            SplitContainer splitContainer = GetlineNumberSplitContainer();
            System.Text.StringBuilder lineNumbersText = new System.Text.StringBuilder();
            int lineCount = richTextBox.Lines.Length;
            int maxDigits = lineCount.ToString().Length;
            int requiredWidth = CalculateWidth(maxDigits);
            if (splitContainer.SplitterDistance != requiredWidth)
            {
                splitContainer.SplitterDistance = Math.Min(requiredWidth, splitContainer.Width - splitContainer.Panel2MinSize);
            }
            for (int i = 1; i <= lineCount; i++)
            {
                lineNumbersText.AppendLine(i.ToString());
            }
            lineNumberRichTextBox.Text = lineNumbersText.ToString();
            UpdateLineNumberScroll();
        }

        // Прокрутка
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
                    int startOfLineIndex = richTextBox.GetFirstCharIndexFromLine(firstVisibleLineNumber);
                    if (lineNumberRichTextBox.SelectionStart != lineNumberRichTextBox.GetFirstCharIndexFromLine(firstVisibleLineNumber))
                    {
                        lineNumberRichTextBox.SelectionStart = lineNumberRichTextBox.GetFirstCharIndexFromLine(firstVisibleLineNumber);
                    }

                    lineNumberRichTextBox.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during scroll update: " + ex.Message);
            }
        }

        // Расчет ширины на основе количества цифр
        private int CalculateWidth(int digits)
        {
            int baseWidth = 10;
            int extraWidthPerDigit = 12;
            return baseWidth + (digits * extraWidthPerDigit);
        }

        // Получение текщего вспомогательно rtb
        private RichTextBox GetlineNumberRichTextBox(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = _tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            SplitContainer splitContainerP1 = (SplitContainer)splitContainer.Panel1.Controls[0];
            return (RichTextBox)splitContainerP1.Panel1.Controls[0];
        }

        private SplitContainer GetlineNumberSplitContainer(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = _tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            return (SplitContainer)splitContainer.Panel1.Controls[0];
        }

        #endregion

        // Получение текущего текстового поля
        private RichTextBox GetSelectedRichTextBox(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = _tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            SplitContainer splitContainerP1 = (SplitContainer)splitContainer.Panel1.Controls[0];
            return (RichTextBox)splitContainerP1.Panel2.Controls[0];
        }
        
    }
}