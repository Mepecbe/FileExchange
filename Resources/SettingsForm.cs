using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowsFormsApp1.Resources.ApplicationConfig;
using WindowsFormsApp1.Resources.Log;

using Tulpep.NotificationWindow;

namespace WindowsFormsApp1.Resources
{
    public partial class SettingsForm : MetroFramework.Forms.MetroForm
    {
        public SettingsForm()
        {
            InitializeComponent();
            this.metroTextBox1.Text = Config.nickname;
            this.metroCheckBox1.Checked = Config.enableSound;
        }


        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string buff = this.metroCheckBox1.Checked ? "1" : "0";
                LogApplication.WriteLog($"Запись новых настроек \n{this.metroTextBox1.Text}\n{buff}\nEND NEW SETTINGS");
                File.WriteAllText("conf.txt", $"{this.metroTextBox1.Text}\n{buff}");

                new PopupNotifier()
                {
                    TitleText = "Настройки",
                    ContentText = "Новые настройки были успешно сохранены"
                }.Popup();

                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка");
                LogApplication.WriteLog(ex.Message);
            }

        }
    }
}
