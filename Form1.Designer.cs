namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            MetroFramework.Controls.MetroButton setStyleBtn;
            this.LocalComputersChat = new MetroFramework.Controls.MetroButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.попыткаУстановкиСоединенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.LABEL_START_WORK = new MetroFramework.Controls.MetroLabel();
            this.timerConnect = new System.Windows.Forms.Timer(this.components);
            setStyleBtn = new MetroFramework.Controls.MetroButton();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // setStyleBtn
            // 
            setStyleBtn.Location = new System.Drawing.Point(119, 5);
            setStyleBtn.Name = "setStyleBtn";
            setStyleBtn.Size = new System.Drawing.Size(75, 20);
            setStyleBtn.TabIndex = 10;
            setStyleBtn.Text = "стиль";
            setStyleBtn.Click += new System.EventHandler(this.metroButton1_Click_1);
            // 
            // LocalComputersChat
            // 
            this.LocalComputersChat.Location = new System.Drawing.Point(200, 5);
            this.LocalComputersChat.Name = "LocalComputersChat";
            this.LocalComputersChat.Size = new System.Drawing.Size(131, 20);
            this.LocalComputersChat.TabIndex = 1;
            this.LocalComputersChat.Text = "Чат локальной сети";
            this.LocalComputersChat.Click += new System.EventHandler(this.LocalComputersChat_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.попыткаУстановкиСоединенияToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(205, 70);
            // 
            // попыткаУстановкиСоединенияToolStripMenuItem
            // 
            this.попыткаУстановкиСоединенияToolStripMenuItem.Name = "попыткаУстановкиСоединенияToolStripMenuItem";
            this.попыткаУстановкиСоединенияToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.попыткаУстановкиСоединенияToolStripMenuItem.Text = "Установить соединение";
            this.попыткаУстановкиСоединенияToolStripMenuItem.Click += new System.EventHandler(this.попыткаУстановкиСоединенияToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // StyleManager
            // 
            this.StyleManager.Owner = this;
            // 
            // LABEL_START_WORK
            // 
            this.LABEL_START_WORK.AutoSize = true;
            this.LABEL_START_WORK.Location = new System.Drawing.Point(8, 148);
            this.LABEL_START_WORK.Name = "LABEL_START_WORK";
            this.LABEL_START_WORK.Size = new System.Drawing.Size(372, 19);
            this.LABEL_START_WORK.TabIndex = 11;
            this.LABEL_START_WORK.Text = "Для старта работы, необходимы клиенты в локальной сети";
            this.LABEL_START_WORK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timerConnect
            // 
            this.timerConnect.Enabled = true;
            this.timerConnect.Interval = 3000;
            this.timerConnect.Tick += new System.EventHandler(this.timerConnect_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 305);
            this.Controls.Add(this.LABEL_START_WORK);
            this.Controls.Add(setStyleBtn);
            this.Controls.Add(this.LocalComputersChat);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Файловый обмен";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Components.MetroStyleManager StyleManager;
        public MetroFramework.Controls.MetroLabel LABEL_START_WORK;
        public MetroFramework.Controls.MetroButton LocalComputersChat;
        public System.Windows.Forms.Timer timerConnect;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem попыткаУстановкиСоединенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
    }
}

