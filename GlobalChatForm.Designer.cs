namespace WindowsFormsApp1
{
    partial class GlobalChatForm
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
            this.MessageTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.chatTextBox = new System.Windows.Forms.RichTextBox();
            this.StyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Location = new System.Drawing.Point(13, 415);
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(669, 23);
            this.MessageTextBox.TabIndex = 0;
            this.MessageTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MessageTextBox_KeyUp);
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(702, 415);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(75, 23);
            this.metroButton1.TabIndex = 1;
            this.metroButton1.Text = "Отправить";
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // chatTextBox
            // 
            this.chatTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.chatTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chatTextBox.Location = new System.Drawing.Point(13, 63);
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.ReadOnly = true;
            this.chatTextBox.Size = new System.Drawing.Size(764, 346);
            this.chatTextBox.TabIndex = 2;
            this.chatTextBox.Text = "";
            this.chatTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // StyleManager
            // 
            this.StyleManager.Owner = this;
            // 
            // GlobalChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chatTextBox);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.MessageTextBox);
            this.Name = "GlobalChatForm";
            this.Text = "Глобальный чат";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GlobalChatForm_FormClosing);
            this.Shown += new System.EventHandler(this.GlobalChatForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton metroButton1;
        public System.Windows.Forms.RichTextBox chatTextBox;
        public MetroFramework.Components.MetroStyleManager StyleManager;
        public MetroFramework.Controls.MetroTextBox MessageTextBox;
    }
}