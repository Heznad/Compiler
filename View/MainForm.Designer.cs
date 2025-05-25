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
            tsmi_Light = new ToolStripMenuItem();
            tsmi_Start = new ToolStripMenuItem();
            tsmi_Reference = new ToolStripMenuItem();
            tsmi_Help = new ToolStripMenuItem();
            tsmi_Info = new ToolStripMenuItem();
            tsmi_View = new ToolStripMenuItem();
            tsmi_FontInput = new ToolStripMenuItem();
            tsmi_FontOutput = new ToolStripMenuItem();
            tsmi_ColorFont = new ToolStripMenuItem();
            tsmi_ColorKeywords = new ToolStripMenuItem();
            tsmi_Localization = new ToolStripMenuItem();
            tsmi_Russian = new ToolStripMenuItem();
            tsmi_English = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelText = new ToolStripStatusLabel();
            toolStripStatusLabelTime = new ToolStripStatusLabel();
            toolStripStatusLabelDate = new ToolStripStatusLabel();
            datelabel = new ToolStripStatusLabel();
            timelabel = new ToolStripStatusLabel();
            panel_Buttons = new Panel();
            btn_URLavto = new Button();
            btn_URL = new Button();
            btn_FIO = new Button();
            btn_Pochta = new Button();
            btn_Light = new Button();
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
            timer_1 = new System.Windows.Forms.Timer(components);
            tabControl = new TabControl();
            toolTip1 = new ToolTip(components);
            richTextBox2 = new RichTextBox();
            richTextBox1 = new RichTextBox();
            timer_2 = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            panel_Buttons.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { tsmi_File, tsmi_Correction, tsmi_Start, tsmi_Reference, tsmi_View, tsmi_Localization });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // tsmi_File
            // 
            tsmi_File.DropDownItems.AddRange(new ToolStripItem[] { tsmi_Create, tsmi_Open, tsmi_Save, tsmi_SaveAs, toolStripSeparator1, tsmi_Exit });
            tsmi_File.ForeColor = Color.Black;
            tsmi_File.Name = "tsmi_File";
            resources.ApplyResources(tsmi_File, "tsmi_File");
            // 
            // tsmi_Create
            // 
            tsmi_Create.Name = "tsmi_Create";
            resources.ApplyResources(tsmi_Create, "tsmi_Create");
            tsmi_Create.Click += btn_File_Click;
            // 
            // tsmi_Open
            // 
            tsmi_Open.Name = "tsmi_Open";
            resources.ApplyResources(tsmi_Open, "tsmi_Open");
            tsmi_Open.Click += btn_Folder_Click;
            // 
            // tsmi_Save
            // 
            tsmi_Save.Name = "tsmi_Save";
            resources.ApplyResources(tsmi_Save, "tsmi_Save");
            tsmi_Save.Click += btn_Save_Click;
            // 
            // tsmi_SaveAs
            // 
            tsmi_SaveAs.Name = "tsmi_SaveAs";
            resources.ApplyResources(tsmi_SaveAs, "tsmi_SaveAs");
            tsmi_SaveAs.Click += btn_SaveAs_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsmi_Exit
            // 
            tsmi_Exit.Name = "tsmi_Exit";
            resources.ApplyResources(tsmi_Exit, "tsmi_Exit");
            tsmi_Exit.Click += btn_Exit_Click;
            // 
            // tsmi_Correction
            // 
            tsmi_Correction.DropDownItems.AddRange(new ToolStripItem[] { tsmi_Undo, tsmi_Redo, tsmi_Cut, tsmi_Copy, tsmi_Paste, tsmi_Delete, tsmi_SelectAll, tsmi_Light });
            tsmi_Correction.ForeColor = Color.Black;
            tsmi_Correction.Name = "tsmi_Correction";
            resources.ApplyResources(tsmi_Correction, "tsmi_Correction");
            // 
            // tsmi_Undo
            // 
            tsmi_Undo.Name = "tsmi_Undo";
            resources.ApplyResources(tsmi_Undo, "tsmi_Undo");
            tsmi_Undo.Click += btn_Undo_Click;
            // 
            // tsmi_Redo
            // 
            tsmi_Redo.Name = "tsmi_Redo";
            resources.ApplyResources(tsmi_Redo, "tsmi_Redo");
            tsmi_Redo.Click += btn_Redo_Click;
            // 
            // tsmi_Cut
            // 
            tsmi_Cut.Name = "tsmi_Cut";
            resources.ApplyResources(tsmi_Cut, "tsmi_Cut");
            tsmi_Cut.Click += btn_Cut_Click;
            // 
            // tsmi_Copy
            // 
            tsmi_Copy.Name = "tsmi_Copy";
            resources.ApplyResources(tsmi_Copy, "tsmi_Copy");
            tsmi_Copy.Click += btn_Copy_Click;
            // 
            // tsmi_Paste
            // 
            tsmi_Paste.Name = "tsmi_Paste";
            resources.ApplyResources(tsmi_Paste, "tsmi_Paste");
            tsmi_Paste.Click += btn_Put_Click;
            // 
            // tsmi_Delete
            // 
            tsmi_Delete.Name = "tsmi_Delete";
            resources.ApplyResources(tsmi_Delete, "tsmi_Delete");
            tsmi_Delete.Click += tsmi_Delete_Click;
            // 
            // tsmi_SelectAll
            // 
            tsmi_SelectAll.Name = "tsmi_SelectAll";
            resources.ApplyResources(tsmi_SelectAll, "tsmi_SelectAll");
            tsmi_SelectAll.Click += tsmi_SelectAll_Click;
            // 
            // tsmi_Light
            // 
            tsmi_Light.Name = "tsmi_Light";
            resources.ApplyResources(tsmi_Light, "tsmi_Light");
            // 
            // tsmi_Start
            // 
            tsmi_Start.ForeColor = Color.Black;
            tsmi_Start.Name = "tsmi_Start";
            resources.ApplyResources(tsmi_Start, "tsmi_Start");
            tsmi_Start.Click += btn_Start_Click;
            // 
            // tsmi_Reference
            // 
            tsmi_Reference.DropDownItems.AddRange(new ToolStripItem[] { tsmi_Help, tsmi_Info });
            tsmi_Reference.ForeColor = Color.Black;
            tsmi_Reference.Name = "tsmi_Reference";
            resources.ApplyResources(tsmi_Reference, "tsmi_Reference");
            // 
            // tsmi_Help
            // 
            tsmi_Help.Name = "tsmi_Help";
            resources.ApplyResources(tsmi_Help, "tsmi_Help");
            tsmi_Help.Click += btn_Info_Click;
            // 
            // tsmi_Info
            // 
            tsmi_Info.Name = "tsmi_Info";
            resources.ApplyResources(tsmi_Info, "tsmi_Info");
            tsmi_Info.Click += btn_Info_Click;
            // 
            // tsmi_View
            // 
            tsmi_View.DropDownItems.AddRange(new ToolStripItem[] { tsmi_FontInput, tsmi_FontOutput, tsmi_ColorFont, tsmi_ColorKeywords });
            tsmi_View.ForeColor = Color.Black;
            tsmi_View.Name = "tsmi_View";
            resources.ApplyResources(tsmi_View, "tsmi_View");
            // 
            // tsmi_FontInput
            // 
            tsmi_FontInput.Name = "tsmi_FontInput";
            resources.ApplyResources(tsmi_FontInput, "tsmi_FontInput");
            tsmi_FontInput.Click += tsmi_Font_Click;
            // 
            // tsmi_FontOutput
            // 
            tsmi_FontOutput.Name = "tsmi_FontOutput";
            resources.ApplyResources(tsmi_FontOutput, "tsmi_FontOutput");
            tsmi_FontOutput.Click += tsmi_FontOutput_Click;
            // 
            // tsmi_ColorFont
            // 
            tsmi_ColorFont.Name = "tsmi_ColorFont";
            resources.ApplyResources(tsmi_ColorFont, "tsmi_ColorFont");
            tsmi_ColorFont.Click += tsmi_ColorFont_Click;
            // 
            // tsmi_ColorKeywords
            // 
            tsmi_ColorKeywords.Name = "tsmi_ColorKeywords";
            resources.ApplyResources(tsmi_ColorKeywords, "tsmi_ColorKeywords");
            tsmi_ColorKeywords.Click += tsmi_ColorKeywords_Click;
            // 
            // tsmi_Localization
            // 
            tsmi_Localization.DropDownItems.AddRange(new ToolStripItem[] { tsmi_Russian, tsmi_English });
            tsmi_Localization.ForeColor = Color.Black;
            tsmi_Localization.Name = "tsmi_Localization";
            resources.ApplyResources(tsmi_Localization, "tsmi_Localization");
            // 
            // tsmi_Russian
            // 
            tsmi_Russian.Name = "tsmi_Russian";
            resources.ApplyResources(tsmi_Russian, "tsmi_Russian");
            tsmi_Russian.Click += tsmi_Russian_Click;
            // 
            // tsmi_English
            // 
            tsmi_English.Name = "tsmi_English";
            resources.ApplyResources(tsmi_English, "tsmi_English");
            tsmi_English.Click += tsmi_English_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelText, toolStripStatusLabelTime, toolStripStatusLabelDate, datelabel, timelabel });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabelText
            // 
            toolStripStatusLabelText.ForeColor = Color.Black;
            toolStripStatusLabelText.Name = "toolStripStatusLabelText";
            resources.ApplyResources(toolStripStatusLabelText, "toolStripStatusLabelText");
            // 
            // toolStripStatusLabelTime
            // 
            toolStripStatusLabelTime.ForeColor = Color.Black;
            toolStripStatusLabelTime.Name = "toolStripStatusLabelTime";
            resources.ApplyResources(toolStripStatusLabelTime, "toolStripStatusLabelTime");
            // 
            // toolStripStatusLabelDate
            // 
            toolStripStatusLabelDate.ForeColor = Color.Black;
            toolStripStatusLabelDate.Name = "toolStripStatusLabelDate";
            resources.ApplyResources(toolStripStatusLabelDate, "toolStripStatusLabelDate");
            // 
            // datelabel
            // 
            datelabel.ForeColor = Color.Black;
            datelabel.Name = "datelabel";
            resources.ApplyResources(datelabel, "datelabel");
            // 
            // timelabel
            // 
            timelabel.ForeColor = Color.Black;
            timelabel.Name = "timelabel";
            resources.ApplyResources(timelabel, "timelabel");
            // 
            // panel_Buttons
            // 
            panel_Buttons.BackColor = SystemColors.Control;
            panel_Buttons.Controls.Add(btn_URLavto);
            panel_Buttons.Controls.Add(btn_URL);
            panel_Buttons.Controls.Add(btn_FIO);
            panel_Buttons.Controls.Add(btn_Pochta);
            panel_Buttons.Controls.Add(btn_Light);
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
            resources.ApplyResources(panel_Buttons, "panel_Buttons");
            panel_Buttons.Name = "panel_Buttons";
            // 
            // btn_URLavto
            // 
            resources.ApplyResources(btn_URLavto, "btn_URLavto");
            btn_URLavto.ForeColor = Color.Black;
            btn_URLavto.Name = "btn_URLavto";
            btn_URLavto.UseVisualStyleBackColor = true;
            btn_URLavto.Click += btn_URLavto_Click;
            // 
            // btn_URL
            // 
            resources.ApplyResources(btn_URL, "btn_URL");
            btn_URL.ForeColor = Color.Black;
            btn_URL.Name = "btn_URL";
            btn_URL.UseVisualStyleBackColor = true;
            btn_URL.Click += btn_URL_Click;
            // 
            // btn_FIO
            // 
            resources.ApplyResources(btn_FIO, "btn_FIO");
            btn_FIO.ForeColor = Color.Black;
            btn_FIO.Name = "btn_FIO";
            btn_FIO.UseVisualStyleBackColor = true;
            btn_FIO.Click += btn_FIO_Click;
            // 
            // btn_Pochta
            // 
            resources.ApplyResources(btn_Pochta, "btn_Pochta");
            btn_Pochta.ForeColor = Color.Black;
            btn_Pochta.Name = "btn_Pochta";
            btn_Pochta.UseVisualStyleBackColor = true;
            btn_Pochta.Click += btn_Pochta_Click;
            // 
            // btn_Light
            // 
            resources.ApplyResources(btn_Light, "btn_Light");
            btn_Light.ForeColor = Color.Gainsboro;
            btn_Light.Name = "btn_Light";
            toolTip1.SetToolTip(btn_Light, resources.GetString("btn_Light.ToolTip"));
            btn_Light.UseVisualStyleBackColor = true;
            btn_Light.Click += btn_Light_Click;
            // 
            // btn_Info
            // 
            resources.ApplyResources(btn_Info, "btn_Info");
            btn_Info.ForeColor = Color.Gainsboro;
            btn_Info.Image = Properties.Resources.info;
            btn_Info.Name = "btn_Info";
            toolTip1.SetToolTip(btn_Info, resources.GetString("btn_Info.ToolTip"));
            btn_Info.UseVisualStyleBackColor = true;
            btn_Info.Click += btn_Info_Click;
            // 
            // btn_Help
            // 
            resources.ApplyResources(btn_Help, "btn_Help");
            btn_Help.ForeColor = Color.Transparent;
            btn_Help.Image = Properties.Resources.help;
            btn_Help.Name = "btn_Help";
            toolTip1.SetToolTip(btn_Help, resources.GetString("btn_Help.ToolTip"));
            btn_Help.UseVisualStyleBackColor = true;
            btn_Help.Click += btn_Info_Click;
            // 
            // btn_Start
            // 
            resources.ApplyResources(btn_Start, "btn_Start");
            btn_Start.ForeColor = Color.Gainsboro;
            btn_Start.Image = Properties.Resources.start;
            btn_Start.Name = "btn_Start";
            toolTip1.SetToolTip(btn_Start, resources.GetString("btn_Start.ToolTip"));
            btn_Start.UseVisualStyleBackColor = true;
            btn_Start.Click += btn_Start_Click;
            // 
            // btn_Put
            // 
            resources.ApplyResources(btn_Put, "btn_Put");
            btn_Put.ForeColor = Color.Gainsboro;
            btn_Put.Image = Properties.Resources.put;
            btn_Put.Name = "btn_Put";
            toolTip1.SetToolTip(btn_Put, resources.GetString("btn_Put.ToolTip"));
            btn_Put.UseVisualStyleBackColor = true;
            btn_Put.Click += btn_Put_Click;
            // 
            // btn_Cut
            // 
            resources.ApplyResources(btn_Cut, "btn_Cut");
            btn_Cut.ForeColor = Color.Gainsboro;
            btn_Cut.Image = Properties.Resources.cut;
            btn_Cut.Name = "btn_Cut";
            toolTip1.SetToolTip(btn_Cut, resources.GetString("btn_Cut.ToolTip"));
            btn_Cut.UseVisualStyleBackColor = true;
            btn_Cut.Click += btn_Cut_Click;
            // 
            // btn_Copy
            // 
            resources.ApplyResources(btn_Copy, "btn_Copy");
            btn_Copy.ForeColor = Color.Gainsboro;
            btn_Copy.Image = Properties.Resources.copy;
            btn_Copy.Name = "btn_Copy";
            toolTip1.SetToolTip(btn_Copy, resources.GetString("btn_Copy.ToolTip"));
            btn_Copy.UseVisualStyleBackColor = true;
            btn_Copy.Click += btn_Copy_Click;
            // 
            // btn_Redo
            // 
            resources.ApplyResources(btn_Redo, "btn_Redo");
            btn_Redo.ForeColor = Color.Gainsboro;
            btn_Redo.Image = Properties.Resources.forward;
            btn_Redo.Name = "btn_Redo";
            toolTip1.SetToolTip(btn_Redo, resources.GetString("btn_Redo.ToolTip"));
            btn_Redo.UseVisualStyleBackColor = true;
            btn_Redo.Click += btn_Redo_Click;
            // 
            // btn_Undo
            // 
            resources.ApplyResources(btn_Undo, "btn_Undo");
            btn_Undo.ForeColor = Color.Gainsboro;
            btn_Undo.Image = Properties.Resources.back;
            btn_Undo.Name = "btn_Undo";
            toolTip1.SetToolTip(btn_Undo, resources.GetString("btn_Undo.ToolTip"));
            btn_Undo.UseVisualStyleBackColor = true;
            btn_Undo.Click += btn_Undo_Click;
            // 
            // btn_Save
            // 
            resources.ApplyResources(btn_Save, "btn_Save");
            btn_Save.ForeColor = Color.Gainsboro;
            btn_Save.Image = Properties.Resources.save;
            btn_Save.Name = "btn_Save";
            toolTip1.SetToolTip(btn_Save, resources.GetString("btn_Save.ToolTip"));
            btn_Save.UseVisualStyleBackColor = true;
            btn_Save.Click += btn_Save_Click;
            // 
            // btn_Open
            // 
            resources.ApplyResources(btn_Open, "btn_Open");
            btn_Open.ForeColor = Color.Gainsboro;
            btn_Open.Image = Properties.Resources.folder;
            btn_Open.Name = "btn_Open";
            toolTip1.SetToolTip(btn_Open, resources.GetString("btn_Open.ToolTip"));
            btn_Open.UseVisualStyleBackColor = true;
            btn_Open.Click += btn_Folder_Click;
            // 
            // btn_Create
            // 
            resources.ApplyResources(btn_Create, "btn_Create");
            btn_Create.ForeColor = Color.Gainsboro;
            btn_Create.Image = Properties.Resources.file;
            btn_Create.Name = "btn_Create";
            toolTip1.SetToolTip(btn_Create, resources.GetString("btn_Create.ToolTip"));
            btn_Create.UseVisualStyleBackColor = true;
            btn_Create.Click += btn_File_Click;
            // 
            // timer_1
            // 
            timer_1.Interval = 1000;
            timer_1.Tick += timer1_Tick;
            // 
            // tabControl
            // 
            resources.ApplyResources(tabControl, "tabControl");
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            tabControl.MouseClick += TabControl_MouseClick;
            // 
            // richTextBox2
            // 
            richTextBox2.BackColor = SystemColors.Window;
            resources.ApplyResources(richTextBox2, "richTextBox2");
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            // 
            // richTextBox1
            // 
            resources.ApplyResources(richTextBox1, "richTextBox1");
            richTextBox1.Name = "richTextBox1";
            // 
            // timer_2
            // 
            timer_2.Interval = 2000;
            timer_2.Tick += timer2_Tick;
            // 
            // MainForm
            // 
            AllowDrop = true;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl);
            Controls.Add(panel_Buttons);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            ForeColor = SystemColors.Control;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            FormClosing += MainForm_FormClosing;
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            KeyDown += MainForm_KeyDown;
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
        private ToolStripMenuItem tsmi_Start;
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
        private System.Windows.Forms.Timer timer_1;
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
        private ToolStripMenuItem tsmi_FontInput;
        private ToolStripMenuItem tsmi_FontOutput;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsmi_ColorFont;
        private ToolStripMenuItem tsmi_ColorKeywords;
        private System.Windows.Forms.Timer timer_2;
        private Button btn_Light;
        private ToolStripMenuItem tsmi_Light;
        private Button btn_Pochta;
        private Button btn_FIO;
        private Button btn_URL;
        private Button btn_URLavto;
    }
}
