using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Compiler.Model;

namespace Compiler.Presenter
{
    public class PresenterForm
    {
        TextManager text_manager = new();
        HighLightning HL = new();
        UndoRedoManager URManager;
        // Словарь для хранения путей для сохранения файлов
        Dictionary<TabPage, string> tabPageFilePaths = new Dictionary<TabPage, string>();
        bool isProcessingText = false; // Флаг для предотвращения гонки потоков при быстром вводе
        bool _cutFlag = false; // Флаг для "Вырезать"
        // Изменяемые элементы UI
        TabControl _tabControl;
        RichTextBox _currentRichTextBox;

        public bool IsHighLight { get => HL.IsHighLight; set => HL.IsHighLight = value; }

        public PresenterForm(TabControl tabControl, Button btn_Undo, Button btn_Redo)
        {
            _tabControl = tabControl;
            URManager = new(btn_Undo, btn_Redo);
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
                // Условие чтобы прекратилась автоматическая подсветка кода, иначе будет мерцание
                if (currentRichTextBox.TextLength >= HL.TextLimitAutoHighLight) HL.IsVolumeRichTextBox = true; 
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

            if (! URManager.IsPerformingUndoRedo)
            {
                await Task.Run(() =>
                {
                    currentRichTextBox.Invoke((MethodInvoker)delegate { URManager.UndoRedoStacksWork(); });
                }
                );
            }

            currentRichTextBox.Invoke((MethodInvoker)delegate { UpdateLineNumbers(); });

            if (!HL.IsHighLight && !HL.IsVolumeRichTextBox)
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
                SetSelectedRichTextBox();
            }
        }

        // Инициализация элементов UI в TabPage
        private void InitializeTabPage(string name)
        {
            TabPage tabPage = new(name);
            SplitContainer splitContainer = new()
            {
                Orientation = Orientation.Horizontal,
                Dock = DockStyle.Fill,
                SplitterDistance = tabPage.Height / 2,
                SplitterWidth = 10,
                Margin = new Padding(3)

            };

            SplitContainer splitContainerWork = new()
            {
                Orientation = Orientation.Vertical,
                Dock = DockStyle.Fill,
                SplitterWidth = 1,
                SplitterDistance = 20
            };
            splitContainerWork.FixedPanel = FixedPanel.Panel1;
            RichTextBox lineNumberRichTextBox = new()
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
            RichTextBox richTextBox = InitializeRichTextBox();
            splitContainerWork.Panel1.Controls.Add(lineNumberRichTextBox);
            splitContainerWork.Panel2.Controls.Add(richTextBox);

            TabControl tabControlResult = InitializeTabControlResult();

            splitContainer.Panel1.Controls.Add(splitContainerWork);
            splitContainer.Panel2.Controls.Add(tabControlResult);

            tabPage.Controls.Add(splitContainer);

            _tabControl.TabPages.Add(tabPage);
        }

        // Создание рабочей области
        private RichTextBox InitializeRichTextBox()
        {
            RichTextBox richTextBox = new()
            {
                Dock = DockStyle.Fill,
                AcceptsTab = true,
                BackColor = Color.White,
                ForeColor = text_manager.SelectedColor,
                Font = text_manager.SelectedFont,
                AllowDrop = true
            };
            richTextBox.ContextMenuStrip = InitializeContextMenu();
            richTextBox.TextChanged += RichTextBox_TextChangedAsync;
            richTextBox.VScroll += RichTextBox_VScroll;
            richTextBox.DragEnter += RichTextBox_DragEnter;
            richTextBox.DragDrop += RichTextBox_DragDrop;
            richTextBox.MouseWheel += RichTextBox_MouseWheel;
            richTextBox.KeyDown += richTextBox_KeyDown;
            richTextBox.MouseUp += RichTextBox_MouseUp;
            URManager.UndoStacks[richTextBox] = new Stack<Command>();
            URManager.RedoStacks[richTextBox] = new Stack<Command>();

            return richTextBox;
        }

        // Создание контекстного меню
        private ContextMenuStrip InitializeContextMenu()
        {
            // Создаем ContextMenuStrip (всплывающее меню)
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Создаем пункты меню
            ToolStripMenuItem cutMenuItem = new ToolStripMenuItem(MyString.Cut);
            ToolStripMenuItem copyMenuItem = new ToolStripMenuItem(MyString.Copy);
            ToolStripMenuItem pasteMenuItem = new ToolStripMenuItem(MyString.Paste);
            ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem(MyString.Delete);

            // Добавляем обработчики событий для пунктов меню
            cutMenuItem.Click += CutMenuItem_Click;
            copyMenuItem.Click += CopyMenuItem_Click;
            pasteMenuItem.Click += PasteMenuItem_Click;
            deleteMenuItem.Click += DeleteMenuItem_Click;

            // Добавляем пункты меню в ContextMenuStrip
            contextMenu.Items.Add(cutMenuItem);
            contextMenu.Items.Add(copyMenuItem);
            contextMenu.Items.Add(pasteMenuItem);
            contextMenu.Items.Add(deleteMenuItem);

            return contextMenu;
        }

        // Создание окна вывода
        private TabControl InitializeTabControlResult()
        {
            TabControl tabControl = new()
            {
                Dock = DockStyle.Fill,
            };

            TabPage tabPageAnalyzer = new(MyString.Analyzer);
            DataGridView dataGridViewAnalyzer = InitializeDataGridView();
            tabPageAnalyzer.Controls.Add(dataGridViewAnalyzer);

            #region Рекурсивный спуск
            TabPage tabPageParser = new(MyString.Output);
            DataGridView dataGridViewParser = InitializeDataGridView();
            tabPageParser.Controls.Add(dataGridViewParser);
            tabControl.Controls.Add(tabPageParser);
            #endregion

            tabControl.Controls.Add(tabPageAnalyzer);
            return tabControl;
        }

        // Создания таблицы вывода для Анализатора
        private DataGridView InitializeDataGridView()
        {
            DataGridView dataGridViewAnalyzer = new DataGridView
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
            dataGridViewAnalyzer.AutoGenerateColumns = false;

            // Добавление колонок
            #region Парсер
            /*DataGridViewTextBoxColumn lineNumberColumn = new DataGridViewTextBoxColumn();
            lineNumberColumn.HeaderText = ""; 
            lineNumberColumn.Width = 30; 
            lineNumberColumn.ReadOnly = true; 
            dataGridViewAnalyzer.Columns.Add(lineNumberColumn);

            DataGridViewTextBoxColumn filePathColumn = new DataGridViewTextBoxColumn();
            filePathColumn.HeaderText = MyString.dgvFilePath;
            filePathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAnalyzer.Columns.Add(filePathColumn);

            DataGridViewTextBoxColumn lineColumn = new DataGridViewTextBoxColumn();
            lineColumn.HeaderText = MyString.dgvLine;
            lineColumn.Width = 50; 
            dataGridViewAnalyzer.Columns.Add(lineColumn);

            DataGridViewTextBoxColumn columnColumn = new DataGridViewTextBoxColumn();
            columnColumn.HeaderText = MyString.dgvColumn;
            columnColumn.Width = 60; 
            dataGridViewAnalyzer.Columns.Add(columnColumn);

            DataGridViewTextBoxColumn messageColumn = new DataGridViewTextBoxColumn();
            messageColumn.HeaderText = MyString.dgvMessage;
            messageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAnalyzer.Columns.Add(messageColumn);*/
            #endregion
            #region Тетрады
            /*DataGridViewTextBoxColumn filePathColumn = new DataGridViewTextBoxColumn();
            filePathColumn.HeaderText = "op";
            filePathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAnalyzer.Columns.Add(filePathColumn);

            DataGridViewTextBoxColumn lineColumn = new DataGridViewTextBoxColumn();
            lineColumn.HeaderText = "arg1";
            lineColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAnalyzer.Columns.Add(lineColumn);

            DataGridViewTextBoxColumn columnColumn = new DataGridViewTextBoxColumn();
            columnColumn.HeaderText = "arg2";
            columnColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAnalyzer.Columns.Add(columnColumn);

            DataGridViewTextBoxColumn messageColumn = new DataGridViewTextBoxColumn();
            messageColumn.HeaderText = "result";
            messageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAnalyzer.Columns.Add(messageColumn);*/
            #endregion
            #region Регулярные выражения
            /*DataGridViewTextBoxColumn filePathColumn = new DataGridViewTextBoxColumn();
            filePathColumn.HeaderText = "Тип";
            filePathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAnalyzer.Columns.Add(filePathColumn);

            DataGridViewTextBoxColumn lineColumn = new DataGridViewTextBoxColumn();
            lineColumn.HeaderText = "Найдено";
            lineColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAnalyzer.Columns.Add(lineColumn);

            DataGridViewTextBoxColumn columnColumn = new DataGridViewTextBoxColumn();
            columnColumn.HeaderText = "Позиция";
            columnColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAnalyzer.Columns.Add(columnColumn);*/
            #endregion

            dataGridViewAnalyzer.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray; // Чередование цветов строк
            dataGridViewAnalyzer.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke; // Цвет фона заголовков
            dataGridViewAnalyzer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            return dataGridViewAnalyzer;
        }

        // Появление диалогового окна для задания имени файла
        private string NameNewFile()
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = MyString.NameFile,
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 50, Top = 20, Text = MyString.NameFile, AutoSize = true };
            TextBox textBox = new TextBox() { Text = MyString.NewFile, Left = 50, Top = 50, Width = 200, };
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
                    DataGridView dataGridViewAnalyzer = GetDataGridViewAnalyzer();
                    dataGridViewAnalyzer.Rows.Clear();
                    dataGridViewAnalyzer.Rows.Add("1", filePath);
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
                    DataGridView dataGridViewAnalyzer = GetDataGridViewAnalyzer();
                    dataGridViewAnalyzer.Rows.Clear();
                    dataGridViewAnalyzer.Rows.Add("1", filePath);
                }
            }
            else return;
        }

        // Сохранение содержимого TabPage в файл
        private void SaveTabPageContentToFile(TabPage tabPage, string filePath)
        {
            try
            {
                File.WriteAllText(filePath, _currentRichTextBox.Text);
                MessageBox.Show(MyString.SuccessFileSave, MyString.Saving, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _currentRichTextBox.Modified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MyString.ErrorSaveFile + ex.Message, MyString.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region [ Закрыть вкладку/приложение ] 

        // Закрыть выбранную вкладку
        public void CloseSelectedTab()
        {
            TabPage tabPageToClose = _tabControl.SelectedTab;

            if (_currentRichTextBox.Modified)
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

        // Закрыть все вкладки
        public void CloseAllTabPages(Form form)
        {
            for (int i = _tabControl.Controls.Count; i > 0; i--)
            {
                _tabControl.SelectedIndex = i;

                TabPage tabPageToClose = _tabControl.TabPages[i - 1];
                /*RichTextBox richTextBox = SetSelectedRichTextBox(tabPageToClose);*/

                if (_currentRichTextBox.Modified)
                {
                    form.Activate();
                    DialogResult result = MessageBox.Show(MyString.SavingChangesFile + $"\"{tabPageToClose.Text}\"", MyString.Saving, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
        }

        // Перезагрузить приложение
        public void RestartCompilyator(Form form)
        {
            CloseAllTabPages(form);
            Application.Restart();
        }

        // Закрыть приложение
        public void CLoseCompilyator(Form form)
        {
            CloseAllTabPages(form);
        }

        #endregion


        #region [ Отменить/Повторить ]

        // Нажатие на кнопку "Отменить" 
        public void UndoButton_Click()
        {
            URManager.UndoButton_Click();
        }

        // Нажатие на кнопку "Повторить" 
        public void RedoButton_Click()
        {
            URManager.RedoButton_Click();
        }

        public void UpdateUndoRedoButtonStates()
        {
            URManager.UpdateUndoRedoButtonStates();
        }
        // Получаем последние состояние RichTextBox
        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            URManager.LastText = _currentRichTextBox.Text;
        }

        #endregion


        #region [ Копировать/Вырезать/Вставить/Удалить/Выделить_всё ]

        // Для Контекстного меню
        private void CutMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox_Cut();
        }
        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox_Copy();
        }
        private void PasteMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox_Paste();
        }
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox_Delete();
        }

        // Вырезать
        public void RichTextBox_Cut()
        {
            if (_currentRichTextBox.SelectionLength > 0)
            {
                _currentRichTextBox.Cut();
                _cutFlag = true;
            }
            
        }

        // Копировать
        public void RichTextBox_Copy()
        {
            if (_currentRichTextBox.SelectionLength > 0)
            {
                _currentRichTextBox.Copy();
            }
        }

        // Вставить
        public void RichTextBox_Paste()
        {
            if (Clipboard.ContainsText())
            {
                string clipboardText = Clipboard.GetText();
                _currentRichTextBox.SelectedText = clipboardText;
                if (_cutFlag) 
                { 
                    Clipboard.Clear();
                    _cutFlag = false;
                };
            }
        }

        // Удалить
        public void RichTextBox_Delete()
        {
            if (_currentRichTextBox.SelectionLength > 0)
            {
                _currentRichTextBox.SelectedText = "";
            }
            else
            {
                if (_currentRichTextBox.SelectionStart < _currentRichTextBox.TextLength)
                {
                    _currentRichTextBox.SelectionLength = 1;
                    _currentRichTextBox.SelectedText = "";
                }
            }
        }

        // Выделить всё
        public void RichTextBox_SelectAll()
        {
            _currentRichTextBox.SelectAll();
        }

        private void RichTextBox_MouseUp(object? sender, MouseEventArgs e)
        {
            RichTextBox richTextBox = (RichTextBox)sender;
            // Проверяем, была ли нажата правая кнопка мыши
            if (e.Button == MouseButtons.Right)
            {
                // Если нет выделенного текста, отключаем пункты "Вырезать", "Копировать", "Удалить"
                if (string.IsNullOrEmpty(richTextBox.SelectedText))
                {
                    richTextBox.ContextMenuStrip.Items[0].Enabled = false;
                    richTextBox.ContextMenuStrip.Items[1].Enabled = false;
                    richTextBox.ContextMenuStrip.Items[3].Enabled = false;
                    if (Clipboard.ContainsText())
                    {
                        richTextBox.ContextMenuStrip.Items[2].Enabled = true;
                    }
                    else
                    {
                        richTextBox.ContextMenuStrip.Items[2].Enabled = false;
                    }

                }
                else
                {
                    richTextBox.ContextMenuStrip.Items[0].Enabled = true;
                    richTextBox.ContextMenuStrip.Items[1].Enabled = true;
                    richTextBox.ContextMenuStrip.Items[3].Enabled = true;
                    if (Clipboard.ContainsText())
                    {
                        richTextBox.ContextMenuStrip.Items[2].Enabled = true;
                    }
                    else
                    {
                        richTextBox.ContextMenuStrip.Items[2].Enabled = false;
                    }
                }

                // Отображаем ContextMenuStrip в позиции курсора
                richTextBox.ContextMenuStrip.Show(richTextBox, e.Location);
            }
        }

        #endregion


        #region [ Шрифт/Цвет/Подсветка ]

        // Выбор шрифта
        public void SetFont()
        {
            text_manager.SettingsFont();
            FontRTBs();
        }

        // Выбор шрифта для окна вывода
        public void SetFontOutput()
        {
            text_manager.SettingsFontOutput();
            DataGridView dataGridView = GetDataGridViewAnalyzer();
            dataGridView.Font = text_manager.SelectedFontOutput;
        }

        // Форма выбора типа для изменения цвета
        public void SettingsColorAllKeywords()
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 350,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = MyString.ChooseColor,
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 30, Top = 20, Text = MyString.ChangeColorKeyword, AutoSize = true };
            Label currentLabelColor = new Label() { Left = 70, Top = 50, Text = MyString.CurrentColor, AutoSize = true };
            Panel pKeywords = new Panel() { BorderStyle = BorderStyle.Fixed3D, BackColor = text_manager.KeywordCategories["Keywords"], Width = 40, Height = 40, Left = 100, Top = 80 };
            Button btn_Keywords = new Button() { Text = MyString.Keywords, Left = 275, Width = 125, Height = 40, Top = 80 };
            Panel pTypes = new Panel() { BorderStyle = BorderStyle.Fixed3D, BackColor = text_manager.KeywordCategories["TypesData"], Width = 40, Height = 40, Left = 100, Top = 130 };
            Button btn_Types = new Button() { Text = MyString.TypesOfData, Left = 275, Width = 125, Height = 40, Top = 130 };
            Panel pOperators = new Panel() { BorderStyle = BorderStyle.Fixed3D, BackColor = text_manager.KeywordCategories["Operators"], Width = 40, Height = 40, Left = 100, Top = 180 };
            Button btn_Operators = new Button() { Text = MyString.Operators, Left = 275, Width = 125, Height = 40, Top = 180 };
            Panel pComments = new Panel() { BorderStyle = BorderStyle.Fixed3D, BackColor = text_manager.KeywordCategories["Comments"], Width = 40, Height = 40, Left = 100, Top = 230 };
            Button btn_Comments = new Button() { Text = MyString.Operators, Left = 275, Width = 125, Height = 40, Top = 230 };

            btn_Keywords.Click += (sender, e) => { prompt.Close(); SettingsColorKeywords(); };
            btn_Types.Click += (sender, e) => { prompt.Close(); SettingsColorTypes(); };
            btn_Operators.Click += (sender, e) => { prompt.Close(); SettingsColorOperators(); };
            btn_Comments.Click += (sender, e) => { prompt.Close(); SettingsColorComments(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(currentLabelColor);
            prompt.Controls.Add(pKeywords);
            prompt.Controls.Add(btn_Keywords);
            prompt.Controls.Add(pTypes);
            prompt.Controls.Add(btn_Types);
            prompt.Controls.Add(pOperators);
            prompt.Controls.Add(btn_Operators);
            prompt.Controls.Add(pComments);
            prompt.Controls.Add(btn_Comments);
            prompt.ShowDialog();
        }

        #region [ Цвет ключевых слов]

        // Ключевые слова
        public void SettingsColorKeywords()
        {
            text_manager.KeywordCategories["Keywords"] = text_manager.SettingsColorFont(text_manager.KeywordCategories["Keywords"]);
            HL.HighlightKeywords(_currentRichTextBox, text_manager);
        }

        // Типы данных
        public void SettingsColorTypes()
        {
            text_manager.KeywordCategories["TypesData"] = text_manager.SettingsColorFont(text_manager.KeywordCategories["TypesData"]);
            HL.HighlightKeywords(_currentRichTextBox, text_manager);
        }

        // Операторы
        public void SettingsColorOperators()
        {
            text_manager.KeywordCategories["Operators"] = text_manager.SettingsColorFont(text_manager.KeywordCategories["Operators"]);
            HL.HighlightKeywords(_currentRichTextBox, text_manager);
        }

        // Комменты
        public void SettingsColorComments()
        {
            text_manager.KeywordCategories["Comments"] = text_manager.SettingsColorFont(text_manager.KeywordCategories["Comments"]);
            HL.HighlightKeywords(_currentRichTextBox,text_manager);
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

        public void HighlightKeywords()
        {
            HL.HighlightKeywords(_currentRichTextBox, text_manager);
        }

        #endregion


        #region [ Прокрутка ]

        // Событие прокурутки скролла у текстовго поля
        private void RichTextBox_VScroll(object sender, EventArgs e)
        {
            UpdateLineNumberScroll();
        }

        // Обновление чисел по количеству строк в рабочей области
        private void UpdateLineNumbers()
        {
            RichTextBox lineNumberRichTextBox = GetlineNumberRichTextBox();
            SplitContainer splitContainer = GetlineNumberSplitContainer();
            System.Text.StringBuilder lineNumbersText = new System.Text.StringBuilder();
            int lineCount = _currentRichTextBox.Lines.Length;
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

        // Изменение положения в соответствии с главным скроллом
        private void UpdateLineNumberScroll()
        {
            RichTextBox lineNumberRichTextBox = GetlineNumberRichTextBox();
            if (_currentRichTextBox == null || lineNumberRichTextBox == null) return;
            try
            {
                int firstVisibleCharIndex = _currentRichTextBox.GetCharIndexFromPosition(new Point(0, 0));
                int firstVisibleLineNumber = _currentRichTextBox.GetLineFromCharIndex(firstVisibleCharIndex);
                if (firstVisibleLineNumber >= 0 && firstVisibleLineNumber < _currentRichTextBox.Lines.Length)
                {
                    int startOfLineIndex = _currentRichTextBox.GetFirstCharIndexFromLine(firstVisibleLineNumber);
                    if (lineNumberRichTextBox.SelectionStart != lineNumberRichTextBox.GetFirstCharIndexFromLine(firstVisibleLineNumber))
                    {
                        lineNumberRichTextBox.SelectionStart = lineNumberRichTextBox.GetFirstCharIndexFromLine(firstVisibleLineNumber);
                    }

                    lineNumberRichTextBox.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(MyString.ErrorScroll + ex.Message);
            }
        }

        // Расчет ширины на основе количества цифр
        private int CalculateWidth(int digits)
        {
            int baseWidth = 10;
            int extraWidthPerDigit = 12;
            return baseWidth + (digits * extraWidthPerDigit);
        }

        // Получение текущего вспомогательно rtb
        private RichTextBox GetlineNumberRichTextBox(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = _tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            SplitContainer splitContainerP1 = (SplitContainer)splitContainer.Panel1.Controls[0];
            return (RichTextBox)splitContainerP1.Panel1.Controls[0];
        }

        // Получение Рабочей области
        private SplitContainer GetlineNumberSplitContainer(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = _tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            return (SplitContainer)splitContainer.Panel1.Controls[0];
        }

        #endregion

        #region [ Пуск ]

        public void Start()
        {
            string path = "";
            DataGridView dataGridViewAnalyzer = GetDataGridViewAnalyzer();
            dataGridViewAnalyzer.Rows.Clear();
            //string input = _currentRichTextBox.Text;
            #region Парсер
            /*Scanner scanner = new Scanner();
            List<Token> tokens = scanner.Scan(input);
            List<Token> errors = scanner.CheckEnumConstruction(tokens);
            //if (errors.Count != tokens.Count) { tokens = errors;}
            if (tabPageFilePaths.ContainsKey(_tabControl.SelectedTab)) path = tabPageFilePaths[_tabControl.SelectedTab];
            if (errors.Count == 0)
            {
                dataGridViewAnalyzer.Rows.Add(dataGridViewAnalyzer.Rows.Count + 1, path, "0","0", "Ошибок не обнаружено.");
            } else
            {
                foreach (Token error in errors)
                {
                    dataGridViewAnalyzer.Rows.Add(dataGridViewAnalyzer.Rows.Count + 1, path, error.Line, $"{error.StartPos}-{error.EndPos}", error.ToString());
                }
            }*/
            #endregion
            #region Тетрады
            /*try
            {
                if (string.IsNullOrEmpty(input))
                {
                    MessageBox.Show("Введите выражение для анализа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Обработка присваивания
                string[] parts = input.Split(new[] { '=' }, 2);
                if (parts.Length != 2)
                {
                    throw new Exception("Отсутствует оператор присваивания '='");
                }

                string leftPart = parts[0].Trim();
                string rightPart = parts[1].Trim();

                var tetrads = new List<Tetrad>();
                CompilerTetrads compilerTetrads = new();
                string result =  compilerTetrads.ParseExpression(rightPart, tetrads);
                tetrads.Add(new Tetrad("=", result, null, leftPart));
                foreach (var tetrad in tetrads)
                {
                    dataGridViewAnalyzer.Rows.Add(tetrad.Op, tetrad.Arg1, tetrad.Arg2, tetrad.Result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка разбора", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
            #endregion
            #region Рекурсивный спуск
            DataGridView dataGridViewParser = GetDataGridViewParser();
            // Установка колонок
            dataGridViewAnalyzer.Columns.Clear();
            dataGridViewAnalyzer.Columns.Add("TypeColumn", "Тип токена");
            dataGridViewAnalyzer.Columns.Add("ValueColumn", "Значение");
            dataGridViewAnalyzer.Columns.Add("PositionColumn", "Позиция");

            dataGridViewParser.Columns.Clear();
            dataGridViewParser.Columns.Add("StepColumn", "Шаг");
            dataGridViewParser.Columns.Add("RuleColumn", "Правило");
            dataGridViewParser.Columns.Add("ResultColumn", "Результат");

            string input = _currentRichTextBox.Text.Trim();

            try
            {
                var (tokensData, parseStepsData) = SyntaxAnalyzer.AnalyzeExpression(input);

                // Заполнение таблицы токенов
                foreach (var token in tokensData)
                {
                    dataGridViewAnalyzer.Rows.Add(token.Type, token.Value, token.Position);
                }

                // Заполнение таблицы шагов разбора
                foreach (var step in parseStepsData)
                {
                    dataGridViewParser.Rows.Add(step.StepNumber, step.Rule, step.Result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка анализа: {ex.Message}");
            }
            #endregion
        }
        #region Регулярные выражения
        public void StartPochta()
        {
            DataGridView dataGridViewAnalyzer = GetDataGridViewAnalyzer();
            dataGridViewAnalyzer.Rows.Clear();
            string input = _currentRichTextBox.Text;
            // 1. Поиск почтовых индексов
            FindMatches(input, @"\d{3}\s?\d{3}", "Почтовый индекс");
        }

        public void StartFIO()
        {
            DataGridView dataGridViewAnalyzer = GetDataGridViewAnalyzer();
            dataGridViewAnalyzer.Rows.Clear();
            string input = _currentRichTextBox.Text;
            // 2. Поиск ФИО (Фамилия И.О.)
            FindMatches(input, @"[А-ЯЁ][а-яё]+(?:\s[А-ЯЁ](?:\s*\.\s*[А-ЯЁ])?\s*\.?|\s[А-ЯЁ]\s*\.\s*[А-ЯЁ]\s*\.)", "ФИО");
        }

        public void StartURL()
        {
            DataGridView dataGridViewAnalyzer = GetDataGridViewAnalyzer();
            dataGridViewAnalyzer.Rows.Clear();
            string input = _currentRichTextBox.Text;
            // 3. Поиск URL (http, https, ftp)
            FindMatches(input, @"(https?|ftp):\/\/[a-z0-9\-]+(\.[a-z0-9\-]+)*(:[0-9]+)?(\/[^\s]*)?", "URL");
        }

        private void FindMatches(string text, string pattern, string matchType)
        {
            DataGridView dataGridViewAnalyzer = GetDataGridViewAnalyzer();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                dataGridViewAnalyzer.Rows.Add(matchType, match.Value, match.Index);
            }
        }

        public void FindUrlMatches()
        {
            string input = _currentRichTextBox.Text;
            AvtomatURL avtomat = new();
            DataGridView dataGridViewAnalyzer = GetDataGridViewAnalyzer();
            dataGridViewAnalyzer.Rows.Clear();
            List<UrlMatch> urlMatches = avtomat.FindUrlsWithAutomaton(input);

            foreach (var match in urlMatches)
            {
                dataGridViewAnalyzer.Rows.Add("URL", match.Url, match.Position);
            }
        }
        #endregion
        #endregion

        // Получение текущего текстового поля
        public RichTextBox GetSelectedRichTextBox(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = _tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            SplitContainer splitContainerP1 = (SplitContainer)splitContainer.Panel1.Controls[0];
            return (RichTextBox)splitContainerP1.Panel2.Controls[0];
        }

        // Получение текущего текстового поля
        public void SetSelectedRichTextBox(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = _tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            SplitContainer splitContainerP1 = (SplitContainer)splitContainer.Panel1.Controls[0];
            _currentRichTextBox = (RichTextBox)splitContainerP1.Panel2.Controls[0];
            URManager.CurrentRichTextBox = _currentRichTextBox;
        }

        // Получение текущей таблицы анализатора
        private DataGridView GetDataGridViewAnalyzer(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = _tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            TabControl TabControl = (TabControl)splitContainer.Panel2.Controls[0];
            TabPage tabPageAnalyzer = (TabPage)TabControl.Controls[0];
            return (DataGridView)tabPageAnalyzer.Controls[0];
        }

        private DataGridView GetDataGridViewParser(TabPage tabPage = null)
        {
            if (tabPage == null) tabPage = _tabControl.SelectedTab;
            SplitContainer splitContainer = (SplitContainer)tabPage.Controls[0];
            TabControl TabControl = (TabControl)splitContainer.Panel2.Controls[0];
            TabPage tabPageAnalyzer = (TabPage)TabControl.Controls[1];
            return (DataGridView)tabPageAnalyzer.Controls[0];
        }

    }
}