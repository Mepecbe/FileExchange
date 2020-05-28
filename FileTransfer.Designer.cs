namespace WindowsFormsApp1
{
    partial class FileTransfer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FilesList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileList_contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Label_State = new MetroFramework.Controls.MetroLabel();
            this.StyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.TransferFile_Progress = new MetroFramework.Controls.MetroProgressBar();
            this.LocalChat = new System.Windows.Forms.RichTextBox();
            this.LocalChatText = new MetroFramework.Controls.MetroTextBox();
            this.FileList_contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // FilesList
            // 
            this.FilesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.FilesList.ContextMenuStrip = this.FileList_contextMenu;
            this.FilesList.FullRowSelect = true;
            this.FilesList.HideSelection = false;
            this.FilesList.Location = new System.Drawing.Point(23, 63);
            this.FilesList.Name = "FilesList";
            this.FilesList.Size = new System.Drawing.Size(828, 284);
            this.FilesList.TabIndex = 0;
            this.FilesList.UseCompatibleStateImageBehavior = false;
            this.FilesList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "№";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Имя файла";
            this.columnHeader2.Width = 531;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Размер";
            this.columnHeader3.Width = 143;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Статус";
            this.columnHeader4.Width = 89;
            // 
            // FileList_contextMenu
            // 
            this.FileList_contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьФайлToolStripMenuItem});
            this.FileList_contextMenu.Name = "FileList_contextMenu";
            this.FileList_contextMenu.Size = new System.Drawing.Size(158, 26);
            // 
            // добавитьФайлToolStripMenuItem
            // 
            this.добавитьФайлToolStripMenuItem.Name = "добавитьФайлToolStripMenuItem";
            this.добавитьФайлToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.добавитьФайлToolStripMenuItem.Text = "Передать файл";
            this.добавитьФайлToolStripMenuItem.Click += new System.EventHandler(this.передатьФайлToolStripMenuItem_Click);
            // 
            // AddFileDialog
            // 
            this.AddFileDialog.Multiselect = true;
            // 
            // Label_State
            // 
            this.Label_State.AutoSize = true;
            this.Label_State.Location = new System.Drawing.Point(23, 370);
            this.Label_State.Name = "Label_State";
            this.Label_State.Size = new System.Drawing.Size(133, 19);
            this.Label_State.TabIndex = 4;
            this.Label_State.Text = "Ожидание действия";
            // 
            // StyleManager
            // 
            this.StyleManager.Owner = this;
            // 
            // TransferFile_Progress
            // 
            this.TransferFile_Progress.Location = new System.Drawing.Point(23, 392);
            this.TransferFile_Progress.Name = "TransferFile_Progress";
            this.TransferFile_Progress.Size = new System.Drawing.Size(828, 35);
            this.TransferFile_Progress.TabIndex = 3;
            // 
            // LocalChat
            // 
            this.LocalChat.Location = new System.Drawing.Point(869, 63);
            this.LocalChat.Name = "LocalChat";
            this.LocalChat.ReadOnly = true;
            this.LocalChat.Size = new System.Drawing.Size(171, 254);
            this.LocalChat.TabIndex = 5;
            this.LocalChat.Text = "";
            this.LocalChat.TextChanged += new System.EventHandler(this.LocalChat_TextChanged);
            // 
            // LocalChatText
            // 
            this.LocalChatText.Location = new System.Drawing.Point(869, 324);
            this.LocalChatText.Name = "LocalChatText";
            this.LocalChatText.Size = new System.Drawing.Size(171, 23);
            this.LocalChatText.TabIndex = 6;
            this.LocalChatText.Text = "Сообщение";
            this.LocalChatText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LocalChatText_KeyDown);
            // 
            // FileTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 450);
            this.Controls.Add(this.LocalChatText);
            this.Controls.Add(this.LocalChat);
            this.Controls.Add(this.Label_State);
            this.Controls.Add(this.TransferFile_Progress);
            this.Controls.Add(this.FilesList);
            this.MaximizeBox = false;
            this.Name = "FileTransfer";
            this.Text = "Обмен файлами";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileTransfer_FormClosing);
            this.Load += new System.EventHandler(this.FileTransfer_Load);
            this.FileList_contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip FileList_contextMenu;
        private System.Windows.Forms.ToolStripMenuItem добавитьФайлToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog AddFileDialog;
        public MetroFramework.Controls.MetroLabel Label_State;
        public MetroFramework.Components.MetroStyleManager StyleManager;
        public System.Windows.Forms.ListView FilesList;
        private MetroFramework.Controls.MetroProgressBar TransferFile_Progress;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private MetroFramework.Controls.MetroTextBox LocalChatText;
        private System.Windows.Forms.RichTextBox LocalChat;
    }
}