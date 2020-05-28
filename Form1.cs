using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Drawing;
using MetroFramework.Controls;
using MetroFramework.Animation;
using MetroFramework.Components;
using MetroFramework.Fonts;
using MetroFramework.Interfaces;
using MetroFramework.Native;

using WindowsFormsApp1.Resources.ApplicationConfig;
using WindowsFormsApp1.Resources.Network;
using WindowsFormsApp1.Resources.Log;
using WindowsFormsApp1.Resources;

using Tulpep.NotificationWindow;


namespace WindowsFormsApp1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LogApplication.Init();
                Config.Init();

                flag = true;
            }
            catch
            {
                MessageBox.Show("Один экземпляр уже запущен");
                this.Close();
            }
            if (DateTime.Now.Hour >= 19 || DateTime.Now.Hour < 8) StyleManager.Theme = MetroThemeStyle.Dark;            
        }

        private void LocalComputersChat_Click(object sender, EventArgs e)
        {
            GlobalChatForm globalChatForm = new GlobalChatForm();

            globalChatForm.StyleManager.Theme = this.StyleManager.Theme;

            if(globalChatForm.StyleManager.Theme == MetroThemeStyle.Dark)
            {
                globalChatForm.chatTextBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            }

            NetworkModule.ChatForm = globalChatForm;
            globalChatForm.ShowDialog();
        }

        string textBuff = ""; bool flag = false;
        private void попыткаУстановкиСоединенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Отправить запрос на открытие, если мы примем ответ с подключением, то запустим форму
            TcpClient client = new TcpClient();

            if(NetworkModule.TryConnect(LocalMachines.GetIPByNickname(textBuff), ref client))
            {
                FileTransfer s = new FileTransfer(client, null, LocalMachines.GetIPByNickname(textBuff), this); 
                s.StyleManager.Theme = StyleManager.Theme;

                if (s.StyleManager.Theme == MetroThemeStyle.Dark)
                {
                    s.FilesList.BackColor = System.Drawing.SystemColors.ControlDarkDark;
                }

                s.ShowDialog();
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    new PopupNotifier()
                    {
                        TitleText = "FileExchange",
                        ContentText = $"Не удалось подключится к {textBuff} находящегося по адресу {LocalMachines.GetIPByNickname(textBuff)}"
                    }.Popup();
                });
            }
        }


        private void metroButton1_Click_1(object sender, EventArgs e)
        {            
            if (StyleManager.Theme == MetroThemeStyle.Dark)
                StyleManager.Theme = MetroThemeStyle.Light;
            else
                StyleManager.Theme = MetroThemeStyle.Dark;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(flag)
                NetworkModule.Stop();
        }

        public void metroTile4_MouseEnter(object sender, EventArgs e)
        {
            this.textBuff = ((MetroTile)sender).Text;
        }

        private void timerConnect_Tick(object sender, EventArgs e)
        {
            NetworkModule.Init(this);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalMachines.RemoveMachine(LocalMachines.GetIPByNickname(this.textBuff));
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.metroStyleManager1.Theme = this.StyleManager.Theme;

            settings.ShowDialog();
        }
    }
}
