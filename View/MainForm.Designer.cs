namespace Compiler
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            tsmi_File = new ToolStripMenuItem();
            tsmi_Create = new ToolStripMenuItem();
            tsmi_Open = new ToolStripMenuItem();
            tsmi_Save = new ToolStripMenuItem();
            tsmi_SaveAs = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            tsmi_Exit = new ToolStripMenuItem();
            tsmi_Correction = new ToolStripMenuItem();
            tsmi_Undo = new ToolStripMenuItem();
            tsmi_Redo = new ToolStripMenuItem();
            tsmi_Cut = new ToolStripMenuItem();
            tsmi_Copy = new ToolStripMenuItem();
            tsmi_Paste = new ToolStripMenuItem();
            tsmi_Delete = new ToolStripMenuItem();
            tsmi_SelectAll = new ToolStripMenuItem();
            tsmi_Text = new ToolStripMenuItem();
            постановкаЗадачиToolStripMenuItem = new ToolStripMenuItem();
            грамматикаToolStripMenuItem = new ToolStripMenuItem();
            классификацияГрамматикиToolStripMenuItem = new ToolStripMenuItem();
            методАнализаToolStripMenuItem = new ToolStripMenuItem();
            диагностикаИToolStripMenuItem = new ToolStripMenuItem();
            тестовыйПримерToolStripMenuItem = new ToolStripMenuItem();
            списокЛитературыToolStripMenuItem = new ToolStripMenuItem();
            исходныйКодПрограммыToolStripMenuItem = new ToolStripMenuItem();
            пускToolStripMenuItem = new ToolStripMenuItem();
            tsmi_Reference = new ToolStripMenuItem();
            tsmi_Help = new ToolStripMenuItem();
            tsmi_Info = new ToolStripMenuItem();
            tsmi_Localization = new ToolStripMenuItem();
            tsmi_Russian = new ToolStripMenuItem();
            tsmi_English = new ToolStripMenuItem();
            tsmi_View = new ToolStripMenuItem();
            tsmi_Font = new ToolStripMenuItem();
            tsmi_ColorFont = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelText = new ToolStripStatusLabel();
            toolStripStatusLabelTime = new ToolStripStatusLabel();
            toolStripStatusLabelDate = new ToolStripStatusLabel();
            datelabel = new ToolStripStatusLabel();
            timelabel = new ToolStripStatusLabel();
            panel_Buttons = new Panel();
            btn_Info = new Button();
            btn_Help = new Button();
            btn_Start = new Button();
            btn_Put = new Button();
            btn_Cut = new Button();
            btn_Copy = new Button();
            btn_Redo = new Button();
            btn_Undo = new Button();
            btn_Save = new Button();
            btn_Open = new Button();
            btn_Create = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            tabControl = new TabControl();
            toolTip1 = new ToolTip(components);
            richTextBox2 = new RichTextBox();
            richTextBox1 = new RichTextBox();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            panel_Buttons.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { tsmi_File, tsmi_Correction, tsmi_Text, пускToolStripMenuItem, tsmi_Reference, tsmi_Localization, tsmi_View });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // tsmi_File
            // 
            tsmi_File.DropDownItems.AddRange(new ToolStripItem[] { tsmi_Create, tsmi_Open, tsmi_Save, tsmi_SaveAs, toolStripSeparator1, tsmi_Exit });
            tsmi_File.ForeColor = Color.Black;
            tsmi_File.Name = "tsmi_File";
            tsmi_File.Size = new Size(59, 24);
            tsmi_File.Text = "Файл";
            // 
            // tsmi_Create
            // 
            tsmi_Create.Name = "tsmi_Create";
            tsmi_Create.Size = new Size(224, 26);
            tsmi_Create.Text = "Создать";
            tsmi_Create.Click += btn_File_Click;
            // 
            // tsmi_Open
            // 
            tsmi_Open.Name = "tsmi_Open";
            tsmi_Open.Size = new Size(224, 26);
            tsmi_Open.Text = "Открыть";
            tsmi_Open.Click += btn_Folder_Click;
            // 
            // tsmi_Save
            // 
            tsmi_Save.Name = "tsmi_Save";
            tsmi_Save.Size = new Size(224, 26);
            tsmi_Save.Text = "Сохранить";
            tsmi_Save.Click += btn_Save_Click;
            // 
            // tsmi_SaveAs
            // 
            tsmi_SaveAs.Name = "tsmi_SaveAs";
            tsmi_SaveAs.Size = new Size(224, 26);
            tsmi_SaveAs.Text = "Сохранить как";
            tsmi_SaveAs.Click += btn_SaveAs_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(221, 6);
            // 
            // tsmi_Exit
            // 
            tsmi_Exit.Name = "tsmi_Exit";
            tsmi_Exit.Size = new Size(224, 26);
            tsmi_Exit.Text = "Выход";
            tsmi_Exit.Click += btn_Exit_Click;
            // 
            // tsmi_Correction
            // 
            tsmi_Correction.DropDownItems.AddRange(new ToolStripItem[] { tsmi_Undo, tsmi_Redo, tsmi_Cut, tsmi_Copy, tsmi_Paste, tsmi_Delete, tsmi_SelectAll });
            tsmi_Correction.ForeColor = Color.Black;
            tsmi_Correction.Name = "tsmi_Correction";
            tsmi_Correction.Size = new Size(74, 24);
            tsmi_Correction.Text = "Правка";
            // 
            // tsmi_Undo
            // 
            tsmi_Undo.Name = "tsmi_Undo";
            tsmi_Undo.Size = new Size(186, 26);
            tsmi_Undo.Text = "Отменить";
            tsmi_Undo.Click += btn_Undo_Click;
            // 
            // tsmi_Redo
            // 
            tsmi_Redo.Name = "tsmi_Redo";
            tsmi_Redo.Size = new Size(186, 26);
            tsmi_Redo.Text = "Повторить";
            tsmi_Redo.Click += btn_Redo_Click;
            // 
            // tsmi_Cut
            // 
            tsmi_Cut.Name = "tsmi_Cut";
            tsmi_Cut.Size = new Size(186, 26);
            tsmi_Cut.Text = "Вырезать";
            tsmi_Cut.Click += btn_Cut_Click;
            // 
            // tsmi_Copy
            // 
            tsmi_Copy.Name = "tsmi_Copy";
            tsmi_Copy.Size = new Size(186, 26);
            tsmi_Copy.Text = "Копировать";
            tsmi_Copy.Click += btn_Copy_Click;
            // 
            // tsmi_Paste
            // 
            tsmi_Paste.Name = "tsmi_Paste";
            tsmi_Paste.Size = new Size(186, 26);
            tsmi_Paste.Text = "Вставить";
            tsmi_Paste.Click += btn_Put_Click;
            // 
            // tsmi_Delete
            // 
            tsmi_Delete.Name = "tsmi_Delete";
            tsmi_Delete.Size = new Size(186, 26);
            tsmi_Delete.Text = "Удалить";
            tsmi_Delete.Click += tsmi_Delete_Click;
            // 
            // tsmi_SelectAll
            // 
            tsmi_SelectAll.Name = "tsmi_SelectAll";
            tsmi_SelectAll.Size = new Size(186, 26);
            tsmi_SelectAll.Text = "Выделить все";
            tsmi_SelectAll.Click += tsmi_SelectAll_Click;
            // 
            // tsmi_Text
            // 
            tsmi_Text.DropDownItems.AddRange(new ToolStripItem[] { постановкаЗадачиToolStripMenuItem, грамматикаToolStripMenuItem, классификацияГрамматикиToolStripMenuItem, методАнализаToolStripMenuItem, диагностикаИToolStripMenuItem, тестовыйПримерToolStripMenuItem, списокЛитературыToolStripMenuItem, исходныйКодПрограммыToolStripMenuItem });
            tsmi_Text.ForeColor = Color.Black;
            tsmi_Text.Name = "tsmi_Text";
            tsmi_Text.Size = new Size(59, 24);
            tsmi_Text.Text = "Текст";
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
            // tsmi_Reference
            // 
            tsmi_Reference.DropDownItems.AddRange(new ToolStripItem[] { tsmi_Help, tsmi_Info });
            tsmi_Reference.ForeColor = Color.Black;
            tsmi_Reference.Name = "tsmi_Reference";
            tsmi_Reference.Size = new Size(81, 24);
            tsmi_Reference.Text = "Справка";
            // 
            // tsmi_Help
            // 
            tsmi_Help.Name = "tsmi_Help";
            tsmi_Help.Size = new Size(197, 26);
            tsmi_Help.Text = "Вызов справки";
            tsmi_Help.Click += btn_Info_Click;
            // 
            // tsmi_Info
            // 
            tsmi_Info.Name = "tsmi_Info";
            tsmi_Info.Size = new Size(197, 26);
            tsmi_Info.Text = "О программе";
            tsmi_Info.Click += btn_Info_Click;
            // 
            // tsmi_Localization
            // 
            tsmi_Localization.DropDownItems.AddRange(new ToolStripItem[] { tsmi_Russian, tsmi_English });
            tsmi_Localization.ForeColor = Color.Black;
            tsmi_Localization.Name = "tsmi_Localization";
            tsmi_Localization.Size = new Size(115, 24);
            tsmi_Localization.Text = "Локализация";
            // 
            // tsmi_Russian
            // 
            tsmi_Russian.Name = "tsmi_Russian";
            tsmi_Russian.Size = new Size(146, 26);
            tsmi_Russian.Text = "Русский";
            // 
            // tsmi_English
            // 
            tsmi_English.Name = "tsmi_English";
            tsmi_English.Size = new Size(146, 26);
            tsmi_English.Text = "English";
            // 
            // tsmi_View
            // 
            tsmi_View.DropDownItems.AddRange(new ToolStripItem[] { tsmi_Font, tsmi_ColorFont });
            tsmi_View.ForeColor = Color.Black;
            tsmi_View.Name = "tsmi_View";
            tsmi_View.Size = new Size(49, 24);
            tsmi_View.Text = "Вид";
            // 
            // tsmi_Font
            // 
            tsmi_Font.Name = "tsmi_Font";
            tsmi_Font.Size = new Size(183, 26);
            tsmi_Font.Text = "Шрифт";
            tsmi_Font.Click += tsmi_Font_Click;
            // 
            // tsmi_ColorFont
            // 
            tsmi_ColorFont.Name = "tsmi_ColorFont";
            tsmi_ColorFont.Size = new Size(183, 26);
            tsmi_ColorFont.Text = "Цвет шрифта";
            tsmi_ColorFont.Click += tsmi_ColorFont_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelText, toolStripStatusLabelTime, toolStripStatusLabelDate, datelabel, timelabel });
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
            toolStripStatusLabelTime.Size = new Size(0, 20);
            // 
            // toolStripStatusLabelDate
            // 
            toolStripStatusLabelDate.ForeColor = Color.Black;
            toolStripStatusLabelDate.Name = "toolStripStatusLabelDate";
            toolStripStatusLabelDate.Size = new Size(0, 20);
            // 
            // datelabel
            // 
            datelabel.ForeColor = Color.Black;
            datelabel.Name = "datelabel";
            datelabel.Size = new Size(0, 20);
            // 
            // timelabel
            // 
            timelabel.ForeColor = Color.Black;
            timelabel.Name = "timelabel";
            timelabel.Size = new Size(0, 20);
            // 
            // panel_Buttons
            // 
            panel_Buttons.BackColor = SystemColors.Control;
            panel_Buttons.Controls.Add(btn_Info);
            panel_Buttons.Controls.Add(btn_Help);
            panel_Buttons.Controls.Add(btn_Start);
            panel_Buttons.Controls.Add(btn_Put);
            panel_Buttons.Controls.Add(btn_Cut);
            panel_Buttons.Controls.Add(btn_Copy);
            panel_Buttons.Controls.Add(btn_Redo);
            panel_Buttons.Controls.Add(btn_Undo);
            panel_Buttons.Controls.Add(btn_Save);
            panel_Buttons.Controls.Add(btn_Open);
            panel_Buttons.Controls.Add(btn_Create);
            panel_Buttons.ForeColor = Color.Silver;
            panel_Buttons.Location = new Point(0, 28);
            panel_Buttons.Margin = new Padding(0, 0, 0, 5);
            panel_Buttons.Name = "panel_Buttons";
            panel_Buttons.Padding = new Padding(5);
            panel_Buttons.Size = new Size(800, 58);
            panel_Buttons.TabIndex = 2;
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
            btn_Info.Click += btn_Info_Click;
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
            btn_Help.Click += btn_Info_Click;
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
            btn_Put.Click += btn_Put_Click;
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
            btn_Cut.Click += btn_Cut_Click;
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
            btn_Copy.Click += btn_Copy_Click;
            // 
            // btn_Redo
            // 
            btn_Redo.FlatStyle = FlatStyle.Flat;
            btn_Redo.ForeColor = Color.Gainsboro;
            btn_Redo.Image = (Image)resources.GetObject("btn_Redo.Image");
            btn_Redo.Location = new Point(258, 5);
            btn_Redo.Margin = new Padding(5);
            btn_Redo.Name = "btn_Redo";
            btn_Redo.Size = new Size(48, 48);
            btn_Redo.TabIndex = 4;
            toolTip1.SetToolTip(btn_Redo, "Повторить");
            btn_Redo.UseVisualStyleBackColor = true;
            btn_Redo.Click += btn_Redo_Click;
            // 
            // btn_Undo
            // 
            btn_Undo.FlatStyle = FlatStyle.Flat;
            btn_Undo.ForeColor = Color.Gainsboro;
            btn_Undo.Image = (Image)resources.GetObject("btn_Undo.Image");
            btn_Undo.Location = new Point(200, 5);
            btn_Undo.Margin = new Padding(5);
            btn_Undo.Name = "btn_Undo";
            btn_Undo.Size = new Size(48, 48);
            btn_Undo.TabIndex = 3;
            toolTip1.SetToolTip(btn_Undo, "Отменить");
            btn_Undo.UseVisualStyleBackColor = true;
            btn_Undo.Click += btn_Undo_Click;
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
            btn_Save.Click += btn_Save_Click;
            // 
            // btn_Open
            // 
            btn_Open.FlatStyle = FlatStyle.Flat;
            btn_Open.ForeColor = Color.Gainsboro;
            btn_Open.Image = (Image)resources.GetObject("btn_Open.Image");
            btn_Open.Location = new Point(68, 5);
            btn_Open.Margin = new Padding(5);
            btn_Open.Name = "btn_Open";
            btn_Open.Size = new Size(48, 48);
            btn_Open.TabIndex = 1;
            toolTip1.SetToolTip(btn_Open, "Открыть");
            btn_Open.UseVisualStyleBackColor = true;
            btn_Open.Click += btn_Folder_Click;
            // 
            // btn_Create
            // 
            btn_Create.FlatStyle = FlatStyle.Flat;
            btn_Create.ForeColor = Color.Gainsboro;
            btn_Create.Image = (Image)resources.GetObject("btn_Create.Image");
            btn_Create.Location = new Point(10, 5);
            btn_Create.Margin = new Padding(5);
            btn_Create.Name = "btn_Create";
            btn_Create.Size = new Size(48, 48);
            btn_Create.TabIndex = 0;
            toolTip1.SetToolTip(btn_Create, "Создать");
            btn_Create.UseVisualStyleBackColor = true;
            btn_Create.Click += btn_File_Click;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Location = new Point(0, 88);
            tabControl.Margin = new Padding(1);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(800, 336);
            tabControl.TabIndex = 3;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            tabControl.MouseClick += TabControl_MouseClick;
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
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl);
            Controls.Add(panel_Buttons);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            ForeColor = SystemColors.Control;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(680, 400);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Компилятор";
            FormClosed += MainForm_FormClosed;
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel_Buttons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem tsmi_File;
        private ToolStripMenuItem tsmi_Correction;
        private ToolStripMenuItem tsmi_Text;
        private ToolStripMenuItem пускToolStripMenuItem;
        private ToolStripMenuItem tsmi_Reference;
        private Panel panel_Buttons;
        private Button btn_Create;
        private Button btn_Open;
        private Button btn_Save;
        private Button btn_Undo;
        private Button btn_Redo;
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
        private ToolStripMenuItem tsmi_Create;
        private ToolStripMenuItem tsmi_Open;
        private ToolStripMenuItem tsmi_Save;
        private ToolStripMenuItem tsmi_SaveAs;
        private ToolStripMenuItem tsmi_Exit;
        private ToolStripMenuItem tsmi_Undo;
        private ToolStripMenuItem tsmi_Redo;
        private ToolStripMenuItem tsmi_Cut;
        private ToolStripMenuItem tsmi_Copy;
        private ToolStripMenuItem tsmi_Paste;
        private ToolStripMenuItem tsmi_Delete;
        private ToolStripMenuItem tsmi_SelectAll;
        private ToolStripMenuItem постановкаЗадачиToolStripMenuItem;
        private ToolStripMenuItem грамматикаToolStripMenuItem;
        private ToolStripMenuItem классификацияГрамматикиToolStripMenuItem;
        private ToolStripMenuItem методАнализаToolStripMenuItem;
        private ToolStripMenuItem диагностикаИToolStripMenuItem;
        private ToolStripMenuItem тестовыйПримерToolStripMenuItem;
        private ToolStripMenuItem списокЛитературыToolStripMenuItem;
        private ToolStripMenuItem исходныйКодПрограммыToolStripMenuItem;
        private ToolStripMenuItem tsmi_Help;
        private ToolStripMenuItem tsmi_Info;
        private ToolStripMenuItem tsmi_Localization;
        private ToolStripMenuItem tsmi_Russian;
        private ToolStripMenuItem tsmi_English;
        private TabControl tabControl;
        private ToolTip toolTip1;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox1;
        private ToolStripStatusLabel datelabel;
        private ToolStripStatusLabel timelabel;
        private ToolStripMenuItem tsmi_View;
        private ToolStripMenuItem tsmi_Font;
        private ToolStripMenuItem tsmi_ColorFont;
        private ToolStripSeparator toolStripSeparator1;
    }
}
