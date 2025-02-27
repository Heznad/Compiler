namespace Compiler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            создатьToolStripMenuItem = new ToolStripMenuItem();
            открытьToolStripMenuItem = new ToolStripMenuItem();
            сохранитьToolStripMenuItem = new ToolStripMenuItem();
            сохранитьКакToolStripMenuItem = new ToolStripMenuItem();
            выходToolStripMenuItem = new ToolStripMenuItem();
            правкаToolStripMenuItem = new ToolStripMenuItem();
            отменитьToolStripMenuItem = new ToolStripMenuItem();
            повторитьToolStripMenuItem = new ToolStripMenuItem();
            вырезатьToolStripMenuItem = new ToolStripMenuItem();
            копироватьToolStripMenuItem = new ToolStripMenuItem();
            вставитьToolStripMenuItem = new ToolStripMenuItem();
            удалитьToolStripMenuItem = new ToolStripMenuItem();
            выделитьВсеToolStripMenuItem = new ToolStripMenuItem();
            текстToolStripMenuItem = new ToolStripMenuItem();
            постановкаЗадачиToolStripMenuItem = new ToolStripMenuItem();
            грамматикаToolStripMenuItem = new ToolStripMenuItem();
            классификацияГрамматикиToolStripMenuItem = new ToolStripMenuItem();
            методАнализаToolStripMenuItem = new ToolStripMenuItem();
            диагностикаИToolStripMenuItem = new ToolStripMenuItem();
            тестовыйПримерToolStripMenuItem = new ToolStripMenuItem();
            списокЛитературыToolStripMenuItem = new ToolStripMenuItem();
            исходныйКодПрограммыToolStripMenuItem = new ToolStripMenuItem();
            пускToolStripMenuItem = new ToolStripMenuItem();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            вызовСправкиToolStripMenuItem = new ToolStripMenuItem();
            оПрограммеToolStripMenuItem = new ToolStripMenuItem();
            локализацияToolStripMenuItem = new ToolStripMenuItem();
            русскийToolStripMenuItem = new ToolStripMenuItem();
            английскийToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelText = new ToolStripStatusLabel();
            toolStripStatusLabelTime = new ToolStripStatusLabel();
            toolStripStatusLabelDate = new ToolStripStatusLabel();
            panel_Buttons = new Panel();
            panel1 = new Panel();
            btn_Info = new Button();
            btn_Help = new Button();
            btn_Start = new Button();
            btn_Put = new Button();
            btn_Cut = new Button();
            btn_Copy = new Button();
            btn_Forward = new Button();
            btn_Back = new Button();
            btn_Save = new Button();
            btn_Folder = new Button();
            btn_File = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            richTextBox1 = new RichTextBox();
            richTextBox2 = new RichTextBox();
            tabControl1 = new TabControl();
            toolTip1 = new ToolTip(components);
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            panel_Buttons.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, правкаToolStripMenuItem, текстToolStripMenuItem, пускToolStripMenuItem, справкаToolStripMenuItem, локализацияToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { создатьToolStripMenuItem, открытьToolStripMenuItem, сохранитьToolStripMenuItem, сохранитьКакToolStripMenuItem, выходToolStripMenuItem });
            файлToolStripMenuItem.ForeColor = Color.Black;
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(59, 24);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // создатьToolStripMenuItem
            // 
            создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            создатьToolStripMenuItem.Size = new Size(192, 26);
            создатьToolStripMenuItem.Text = "Создать";
            // 
            // открытьToolStripMenuItem
            // 
            открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            открытьToolStripMenuItem.Size = new Size(192, 26);
            открытьToolStripMenuItem.Text = "Открыть";
            // 
            // сохранитьToolStripMenuItem
            // 
            сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            сохранитьToolStripMenuItem.Size = new Size(192, 26);
            сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // сохранитьКакToolStripMenuItem
            // 
            сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            сохранитьКакToolStripMenuItem.Size = new Size(192, 26);
            сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            // 
            // выходToolStripMenuItem
            // 
            выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            выходToolStripMenuItem.Size = new Size(192, 26);
            выходToolStripMenuItem.Text = "Выход";
            // 
            // правкаToolStripMenuItem
            // 
            правкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { отменитьToolStripMenuItem, повторитьToolStripMenuItem, вырезатьToolStripMenuItem, копироватьToolStripMenuItem, вставитьToolStripMenuItem, удалитьToolStripMenuItem, выделитьВсеToolStripMenuItem });
            правкаToolStripMenuItem.ForeColor = Color.Black;
            правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            правкаToolStripMenuItem.Size = new Size(74, 24);
            правкаToolStripMenuItem.Text = "Правка";
            // 
            // отменитьToolStripMenuItem
            // 
            отменитьToolStripMenuItem.Name = "отменитьToolStripMenuItem";
            отменитьToolStripMenuItem.Size = new Size(186, 26);
            отменитьToolStripMenuItem.Text = "Отменить";
            // 
            // повторитьToolStripMenuItem
            // 
            повторитьToolStripMenuItem.Name = "повторитьToolStripMenuItem";
            повторитьToolStripMenuItem.Size = new Size(186, 26);
            повторитьToolStripMenuItem.Text = "Повторить";
            // 
            // вырезатьToolStripMenuItem
            // 
            вырезатьToolStripMenuItem.Name = "вырезатьToolStripMenuItem";
            вырезатьToolStripMenuItem.Size = new Size(186, 26);
            вырезатьToolStripMenuItem.Text = "Вырезать";
            // 
            // копироватьToolStripMenuItem
            // 
            копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            копироватьToolStripMenuItem.Size = new Size(186, 26);
            копироватьToolStripMenuItem.Text = "Копировать";
            // 
            // вставитьToolStripMenuItem
            // 
            вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            вставитьToolStripMenuItem.Size = new Size(186, 26);
            вставитьToolStripMenuItem.Text = "Вставить";
            // 
            // удалитьToolStripMenuItem
            // 
            удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            удалитьToolStripMenuItem.Size = new Size(186, 26);
            удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // выделитьВсеToolStripMenuItem
            // 
            выделитьВсеToolStripMenuItem.Name = "выделитьВсеToolStripMenuItem";
            выделитьВсеToolStripMenuItem.Size = new Size(186, 26);
            выделитьВсеToolStripMenuItem.Text = "Выделить все";
            // 
            // текстToolStripMenuItem
            // 
            текстToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { постановкаЗадачиToolStripMenuItem, грамматикаToolStripMenuItem, классификацияГрамматикиToolStripMenuItem, методАнализаToolStripMenuItem, диагностикаИToolStripMenuItem, тестовыйПримерToolStripMenuItem, списокЛитературыToolStripMenuItem, исходныйКодПрограммыToolStripMenuItem });
            текстToolStripMenuItem.ForeColor = Color.Black;
            текстToolStripMenuItem.Name = "текстToolStripMenuItem";
            текстToolStripMenuItem.Size = new Size(59, 24);
            текстToolStripMenuItem.Text = "Текст";
            // 
            // постановкаЗадачиToolStripMenuItem
            // 
            постановкаЗадачиToolStripMenuItem.Name = "постановкаЗадачиToolStripMenuItem";
            постановкаЗадачиToolStripMenuItem.Size = new Size(363, 26);
            постановкаЗадачиToolStripMenuItem.Text = "Постановка задачи";
            // 
            // грамматикаToolStripMenuItem
            // 
            грамматикаToolStripMenuItem.Name = "грамматикаToolStripMenuItem";
            грамматикаToolStripMenuItem.Size = new Size(363, 26);
            грамматикаToolStripMenuItem.Text = "Грамматика";
            // 
            // классификацияГрамматикиToolStripMenuItem
            // 
            классификацияГрамматикиToolStripMenuItem.Name = "классификацияГрамматикиToolStripMenuItem";
            классификацияГрамматикиToolStripMenuItem.Size = new Size(363, 26);
            классификацияГрамматикиToolStripMenuItem.Text = "Классификация грамматики";
            // 
            // методАнализаToolStripMenuItem
            // 
            методАнализаToolStripMenuItem.Name = "методАнализаToolStripMenuItem";
            методАнализаToolStripMenuItem.Size = new Size(363, 26);
            методАнализаToolStripMenuItem.Text = "Метод анализа";
            // 
            // диагностикаИToolStripMenuItem
            // 
            диагностикаИToolStripMenuItem.Name = "диагностикаИToolStripMenuItem";
            диагностикаИToolStripMenuItem.Size = new Size(363, 26);
            диагностикаИToolStripMenuItem.Text = "Диагностика и нейтрализация ошибок";
            // 
            // тестовыйПримерToolStripMenuItem
            // 
            тестовыйПримерToolStripMenuItem.Name = "тестовыйПримерToolStripMenuItem";
            тестовыйПримерToolStripMenuItem.Size = new Size(363, 26);
            тестовыйПримерToolStripMenuItem.Text = "Тестовый пример";
            // 
            // списокЛитературыToolStripMenuItem
            // 
            списокЛитературыToolStripMenuItem.Name = "списокЛитературыToolStripMenuItem";
            списокЛитературыToolStripMenuItem.Size = new Size(363, 26);
            списокЛитературыToolStripMenuItem.Text = "Список литературы";
            // 
            // исходныйКодПрограммыToolStripMenuItem
            // 
            исходныйКодПрограммыToolStripMenuItem.Name = "исходныйКодПрограммыToolStripMenuItem";
            исходныйКодПрограммыToolStripMenuItem.Size = new Size(363, 26);
            исходныйКодПрограммыToolStripMenuItem.Text = "Исходный код программы";
            // 
            // пускToolStripMenuItem
            // 
            пускToolStripMenuItem.ForeColor = Color.Black;
            пускToolStripMenuItem.Name = "пускToolStripMenuItem";
            пускToolStripMenuItem.Size = new Size(55, 24);
            пускToolStripMenuItem.Text = "Пуск";
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { вызовСправкиToolStripMenuItem, оПрограммеToolStripMenuItem });
            справкаToolStripMenuItem.ForeColor = Color.Black;
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(81, 24);
            справкаToolStripMenuItem.Text = "Справка";
            // 
            // вызовСправкиToolStripMenuItem
            // 
            вызовСправкиToolStripMenuItem.Name = "вызовСправкиToolStripMenuItem";
            вызовСправкиToolStripMenuItem.Size = new Size(197, 26);
            вызовСправкиToolStripMenuItem.Text = "Вызов справки";
            // 
            // оПрограммеToolStripMenuItem
            // 
            оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            оПрограммеToolStripMenuItem.Size = new Size(197, 26);
            оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // локализацияToolStripMenuItem
            // 
            локализацияToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { русскийToolStripMenuItem, английскийToolStripMenuItem });
            локализацияToolStripMenuItem.ForeColor = Color.Black;
            локализацияToolStripMenuItem.Name = "локализацияToolStripMenuItem";
            локализацияToolStripMenuItem.Size = new Size(115, 24);
            локализацияToolStripMenuItem.Text = "Локализация";
            // 
            // русскийToolStripMenuItem
            // 
            русскийToolStripMenuItem.Name = "русскийToolStripMenuItem";
            русскийToolStripMenuItem.Size = new Size(146, 26);
            русскийToolStripMenuItem.Text = "Русский";
            // 
            // английскийToolStripMenuItem
            // 
            английскийToolStripMenuItem.Name = "английскийToolStripMenuItem";
            английскийToolStripMenuItem.Size = new Size(146, 26);
            английскийToolStripMenuItem.Text = "English";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelText, toolStripStatusLabelTime, toolStripStatusLabelDate });
            statusStrip1.Location = new Point(0, 424);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelText
            // 
            toolStripStatusLabelText.ForeColor = Color.Black;
            toolStripStatusLabelText.Name = "toolStripStatusLabelText";
            toolStripStatusLabelText.Size = new Size(165, 20);
            toolStripStatusLabelText.Text = "Текущая дата и время:";
            // 
            // toolStripStatusLabelTime
            // 
            toolStripStatusLabelTime.ForeColor = Color.Black;
            toolStripStatusLabelTime.Name = "toolStripStatusLabelTime";
            toolStripStatusLabelTime.Size = new Size(137, 20);
            toolStripStatusLabelTime.Text = "28 февраля 2025 г.";
            // 
            // toolStripStatusLabelDate
            // 
            toolStripStatusLabelDate.ForeColor = Color.Black;
            toolStripStatusLabelDate.Name = "toolStripStatusLabelDate";
            toolStripStatusLabelDate.Size = new Size(55, 20);
            toolStripStatusLabelDate.Text = "0:08:05";
            // 
            // panel_Buttons
            // 
            panel_Buttons.Controls.Add(panel1);
            panel_Buttons.Controls.Add(btn_Info);
            panel_Buttons.Controls.Add(btn_Help);
            panel_Buttons.Controls.Add(btn_Start);
            panel_Buttons.Controls.Add(btn_Put);
            panel_Buttons.Controls.Add(btn_Cut);
            panel_Buttons.Controls.Add(btn_Copy);
            panel_Buttons.Controls.Add(btn_Forward);
            panel_Buttons.Controls.Add(btn_Back);
            panel_Buttons.Controls.Add(btn_Save);
            panel_Buttons.Controls.Add(btn_Folder);
            panel_Buttons.Controls.Add(btn_File);
            panel_Buttons.Location = new Point(0, 28);
            panel_Buttons.Margin = new Padding(0, 0, 0, 5);
            panel_Buttons.Name = "panel_Buttons";
            panel_Buttons.Padding = new Padding(5);
            panel_Buttons.Size = new Size(800, 58);
            panel_Buttons.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.ForeColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(0, 61);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 332);
            panel1.TabIndex = 3;
            // 
            // btn_Info
            // 
            btn_Info.FlatStyle = FlatStyle.Flat;
            btn_Info.ForeColor = Color.Gainsboro;
            btn_Info.Image = (Image)resources.GetObject("btn_Info.Image");
            btn_Info.Location = new Point(606, 5);
            btn_Info.Margin = new Padding(5);
            btn_Info.Name = "btn_Info";
            btn_Info.Size = new Size(48, 48);
            btn_Info.TabIndex = 10;
            toolTip1.SetToolTip(btn_Info, "О программе");
            btn_Info.UseVisualStyleBackColor = true;
            // 
            // btn_Help
            // 
            btn_Help.FlatStyle = FlatStyle.Flat;
            btn_Help.ForeColor = Color.Gainsboro;
            btn_Help.Image = (Image)resources.GetObject("btn_Help.Image");
            btn_Help.Location = new Point(548, 5);
            btn_Help.Margin = new Padding(5);
            btn_Help.Name = "btn_Help";
            btn_Help.Size = new Size(48, 48);
            btn_Help.TabIndex = 9;
            toolTip1.SetToolTip(btn_Help, "Вызов справки");
            btn_Help.UseVisualStyleBackColor = true;
            // 
            // btn_Start
            // 
            btn_Start.FlatStyle = FlatStyle.Flat;
            btn_Start.ForeColor = Color.Gainsboro;
            btn_Start.Image = (Image)resources.GetObject("btn_Start.Image");
            btn_Start.Location = new Point(490, 5);
            btn_Start.Margin = new Padding(5);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(48, 48);
            btn_Start.TabIndex = 8;
            toolTip1.SetToolTip(btn_Start, "Пуск");
            btn_Start.UseVisualStyleBackColor = true;
            // 
            // btn_Put
            // 
            btn_Put.FlatStyle = FlatStyle.Flat;
            btn_Put.ForeColor = Color.Gainsboro;
            btn_Put.Image = (Image)resources.GetObject("btn_Put.Image");
            btn_Put.Location = new Point(432, 5);
            btn_Put.Margin = new Padding(5);
            btn_Put.Name = "btn_Put";
            btn_Put.Size = new Size(48, 48);
            btn_Put.TabIndex = 7;
            toolTip1.SetToolTip(btn_Put, "Вставить");
            btn_Put.UseVisualStyleBackColor = true;
            // 
            // btn_Cut
            // 
            btn_Cut.FlatStyle = FlatStyle.Flat;
            btn_Cut.ForeColor = Color.Gainsboro;
            btn_Cut.Image = (Image)resources.GetObject("btn_Cut.Image");
            btn_Cut.Location = new Point(374, 5);
            btn_Cut.Margin = new Padding(5);
            btn_Cut.Name = "btn_Cut";
            btn_Cut.Size = new Size(48, 48);
            btn_Cut.TabIndex = 6;
            toolTip1.SetToolTip(btn_Cut, "Вырезать");
            btn_Cut.UseVisualStyleBackColor = true;
            // 
            // btn_Copy
            // 
            btn_Copy.FlatStyle = FlatStyle.Flat;
            btn_Copy.ForeColor = Color.Gainsboro;
            btn_Copy.Image = (Image)resources.GetObject("btn_Copy.Image");
            btn_Copy.Location = new Point(316, 5);
            btn_Copy.Margin = new Padding(5);
            btn_Copy.Name = "btn_Copy";
            btn_Copy.Size = new Size(48, 48);
            btn_Copy.TabIndex = 5;
            toolTip1.SetToolTip(btn_Copy, "Копировать");
            btn_Copy.UseVisualStyleBackColor = true;
            // 
            // btn_Forward
            // 
            btn_Forward.FlatStyle = FlatStyle.Flat;
            btn_Forward.ForeColor = Color.Gainsboro;
            btn_Forward.Image = (Image)resources.GetObject("btn_Forward.Image");
            btn_Forward.Location = new Point(258, 5);
            btn_Forward.Margin = new Padding(5);
            btn_Forward.Name = "btn_Forward";
            btn_Forward.Size = new Size(48, 48);
            btn_Forward.TabIndex = 4;
            toolTip1.SetToolTip(btn_Forward, "Повторить");
            btn_Forward.UseVisualStyleBackColor = true;
            // 
            // btn_Back
            // 
            btn_Back.FlatStyle = FlatStyle.Flat;
            btn_Back.ForeColor = Color.Gainsboro;
            btn_Back.Image = (Image)resources.GetObject("btn_Back.Image");
            btn_Back.Location = new Point(200, 5);
            btn_Back.Margin = new Padding(5);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(48, 48);
            btn_Back.TabIndex = 3;
            toolTip1.SetToolTip(btn_Back, "Отменить");
            btn_Back.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            btn_Save.FlatStyle = FlatStyle.Flat;
            btn_Save.ForeColor = Color.Gainsboro;
            btn_Save.Image = (Image)resources.GetObject("btn_Save.Image");
            btn_Save.Location = new Point(126, 5);
            btn_Save.Margin = new Padding(5);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new Size(48, 48);
            btn_Save.TabIndex = 2;
            toolTip1.SetToolTip(btn_Save, "Сохранить");
            btn_Save.UseVisualStyleBackColor = true;
            // 
            // btn_Folder
            // 
            btn_Folder.FlatStyle = FlatStyle.Flat;
            btn_Folder.ForeColor = Color.Gainsboro;
            btn_Folder.Image = (Image)resources.GetObject("btn_Folder.Image");
            btn_Folder.Location = new Point(68, 5);
            btn_Folder.Margin = new Padding(5);
            btn_Folder.Name = "btn_Folder";
            btn_Folder.Size = new Size(48, 48);
            btn_Folder.TabIndex = 1;
            toolTip1.SetToolTip(btn_Folder, "Открыть");
            btn_Folder.UseVisualStyleBackColor = true;
            // 
            // btn_File
            // 
            btn_File.FlatStyle = FlatStyle.Flat;
            btn_File.ForeColor = Color.Gainsboro;
            btn_File.Image = (Image)resources.GetObject("btn_File.Image");
            btn_File.Location = new Point(10, 5);
            btn_File.Margin = new Padding(5);
            btn_File.Name = "btn_File";
            btn_File.Size = new Size(48, 48);
            btn_File.TabIndex = 0;
            toolTip1.SetToolTip(btn_File, "Создать");
            btn_File.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(791, 303);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Новый документ";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = SystemColors.Control;
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.ForeColor = SystemColors.ActiveCaptionText;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(richTextBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(richTextBox2);
            splitContainer1.Size = new Size(785, 297);
            splitContainer1.SplitterDistance = 148;
            splitContainer1.SplitterWidth = 10;
            splitContainer1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new Point(0, 0);
            richTextBox1.Margin = new Padding(0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(783, 146);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            richTextBox2.BackColor = SystemColors.Window;
            richTextBox2.Dock = DockStyle.Fill;
            richTextBox2.Location = new Point(0, 0);
            richTextBox2.Margin = new Padding(0);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(783, 137);
            richTextBox2.TabIndex = 0;
            richTextBox2.Text = "";
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(1, 87);
            tabControl1.Margin = new Padding(1);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(799, 336);
            tabControl1.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Controls.Add(panel_Buttons);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            ForeColor = SystemColors.Control;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(680, 300);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Компилятор";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel_Buttons.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem правкаToolStripMenuItem;
        private ToolStripMenuItem текстToolStripMenuItem;
        private ToolStripMenuItem пускToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private Panel panel_Buttons;
        private Button btn_File;
        private Button btn_Folder;
        private Button btn_Save;
        private Button btn_Back;
        private Button btn_Forward;
        private Button btn_Copy;
        private Button btn_Cut;
        private Button btn_Info;
        private Button btn_Help;
        private Button btn_Start;
        private Button btn_Put;
        private System.Windows.Forms.Timer timer1;
        private ToolStripStatusLabel toolStripStatusLabelText;
        private ToolStripStatusLabel toolStripStatusLabelDate;
        private ToolStripStatusLabel toolStripStatusLabelTime;
        private Panel panel1;
        private ToolStripMenuItem создатьToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem отменитьToolStripMenuItem;
        private ToolStripMenuItem повторитьToolStripMenuItem;
        private ToolStripMenuItem вырезатьToolStripMenuItem;
        private ToolStripMenuItem копироватьToolStripMenuItem;
        private ToolStripMenuItem вставитьToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private ToolStripMenuItem выделитьВсеToolStripMenuItem;
        private ToolStripMenuItem постановкаЗадачиToolStripMenuItem;
        private ToolStripMenuItem грамматикаToolStripMenuItem;
        private ToolStripMenuItem классификацияГрамматикиToolStripMenuItem;
        private ToolStripMenuItem методАнализаToolStripMenuItem;
        private ToolStripMenuItem диагностикаИToolStripMenuItem;
        private ToolStripMenuItem тестовыйПримерToolStripMenuItem;
        private ToolStripMenuItem списокЛитературыToolStripMenuItem;
        private ToolStripMenuItem исходныйКодПрограммыToolStripMenuItem;
        private ToolStripMenuItem вызовСправкиToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
        private ToolStripMenuItem локализацияToolStripMenuItem;
        private ToolStripMenuItem русскийToolStripMenuItem;
        private ToolStripMenuItem английскийToolStripMenuItem;
        private TabPage tabPage1;
        private SplitContainer splitContainer1;
        private RichTextBox richTextBox1;
        private TabControl tabControl1;
        private RichTextBox richTextBox2;
        private ToolTip toolTip1;
    }
}
