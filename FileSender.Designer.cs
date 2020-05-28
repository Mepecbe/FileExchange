namespace WindowsFormsApp1
{
    partial class FileSender
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
            this.удалитьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Label_State = new MetroFramework.Controls.MetroLabel();
            this.StyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.SendFiles_Button = new MetroFramework.Controls.MetroButton();
            this.SendFile_Progress = new MetroFramework.Controls.MetroProgressBar();
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
            this.FilesList.Size = new System.Drawing.Size(754, 284);
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
            this.columnHeader2.Width = 567;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Размер";
            this.columnHeader3.Width = 122;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Статус";
            this.columnHeader4.Width = 86;
            // 
            // FileList_contextMenu
            // 
            this.FileList_contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьФайлToolStripMenuItem,
            this.удалитьФайлToolStripMenuItem});
            this.FileList_contextMenu.Name = "FileList_contextMenu";
            this.FileList_contextMenu.Size = new System.Drawing.Size(159, 48);
            // 
            // добавитьФайлToolStripMenuItem
            // 
            this.добавитьФайлToolStripMenuItem.Name = "добавитьФайлToolStripMenuItem";
            this.добавитьФайлToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.добавитьФайлToolStripMenuItem.Text = "Добавить файл";
            this.добавитьФайлToolStripMenuItem.Click += new System.EventHandler(this.добавитьФайлToolStripMenuItem_Click);
            // 
            // удалитьФайлToolStripMenuItem
            // 
            this.удалитьФайлToolStripMenuItem.Name = "удалитьФайлToolStripMenuItem";
            this.удалитьФайлToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.удалитьФайлToolStripMenuItem.Text = "Удалить файл";
            this.удалитьФайлToolStripMenuItem.Click += new System.EventHandler(this.удалитьФайлToolStripMenuItem_Click);
            // 
            // AddFileDialog
            // 
            this.AddFileDialog.Multiselect = true;
            // 
            // Label_State
            // 
            this.Label_State.AutoSize = true;
            this.Label_State.Location = new System.Drawing.Point(23, 359);
            this.Label_State.Name = "Label_State";
            this.Label_State.Size = new System.Drawing.Size(135, 19);
            this.Label_State.TabIndex = 4;
            this.Label_State.Text = "Ожидание отправки";
            // 
            // StyleManager
            // 
            this.StyleManager.Owner = this;
            // 
            // SendFiles_Button
            // 
            this.SendFiles_Button.Location = new System.Drawing.Point(665, 392);
            this.SendFiles_Button.Name = "SendFiles_Button";
            this.SendFiles_Button.Size = new System.Drawing.Size(112, 35);
            this.SendFiles_Button.TabIndex = 1;
            this.SendFiles_Button.Text = "Начать передачу";
            this.SendFiles_Button.Click += new System.EventHandler(this.SendFiles_Button_Click);
            // 
            // SendFile_Progress
            // 
            this.SendFile_Progress.Location = new System.Drawing.Point(23, 392);
            this.SendFile_Progress.Name = "SendFile_Progress";
            this.SendFile_Progress.Size = new System.Drawing.Size(617, 35);
            this.SendFile_Progress.TabIndex = 3;
            // 
            // FileSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Label_State);
            this.Controls.Add(this.SendFile_Progress);
            this.Controls.Add(this.SendFiles_Button);
            this.Controls.Add(this.FilesList);
            this.MaximizeBox = false;
            this.Name = "FileSender";
            this.Text = "Обмен файлами";
            this.Load += new System.EventHandler(this.FileSender_Load);
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
        private System.Windows.Forms.ToolStripMenuItem удалитьФайлToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog AddFileDialog;
        public MetroFramework.Controls.MetroLabel Label_State;
        public MetroFramework.Components.MetroStyleManager StyleManager;
        public System.Windows.Forms.ListView FilesList;
        private MetroFramework.Controls.MetroProgressBar SendFile_Progress;
        private MetroFramework.Controls.MetroButton SendFiles_Button;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}