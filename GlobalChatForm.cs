using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

using System.Net;
using System.Net.Sockets;

using WindowsFormsApp1.Resources.Log;
using WindowsFormsApp1.Resources.Network;
using WindowsFormsApp1.Resources.ApplicationConfig;

using MetroFramework.Controls;

namespace WindowsFormsApp1
{
    public partial class GlobalChatForm : MetroFramework.Forms.MetroForm
    {
        public bool ShownForm = false;

        public GlobalChatForm()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.chatTextBox.Text += "\n";
            chatTextBox.SelectionStart = chatTextBox.Text.Length;
            chatTextBox.ScrollToCaret();
        }

        private void metroButton1_Click(object senderr, EventArgs e)
        {
            //Отправить сообщение
            this.chatTextBox.AppendText("ВЫ: " + MessageTextBox.Text);

            if (LocalMachines.ListLocalMachines.Count == 0)
            {
                LogApplication.WriteLog($"Сообщение не отправленно, т.к. клиентов нет");
            }

            UdpClient sender = new UdpClient(); // создаем UdpClient для отправки

            foreach (LocalMachine machine in LocalMachines.ListLocalMachines)
            {                
                IPEndPoint endPoint = new IPEndPoint(machine.RemoteIp, Config.GlobalChatUdpPort);
                try
                {
                    byte[] data = Config.Encoder.GetBytes(MessageTextBox.Text);
                    sender.Send(data, data.Length, endPoint); // отправка    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sender.Close();
                }
            }
            MessageTextBox.Text = "";



            

        }

        private void GlobalChatForm_Shown(object sender, EventArgs e)
        {
            ShownForm = true;
        }

        private void GlobalChatForm_FormClosing(object senderr, FormClosingEventArgs e)
        {
            NetworkModule.ChatForm = null;

            if (LocalMachines.ListLocalMachines.Count == 0)
            {
                LogApplication.WriteLog($"Сообщение не отправленно, т.к. клиентов нет");
            }

            UdpClient sender = new UdpClient(); // создаем UdpClient для отправки

            foreach (LocalMachine machine in LocalMachines.ListLocalMachines)
            {
                IPEndPoint endPoint = new IPEndPoint(machine.RemoteIp, Config.GlobalChatUdpPort);
                try
                {
                    byte[] data = Config.Encoder.GetBytes(Config.GlobalChatExitMessage);
                    sender.Send(data, data.Length, endPoint); // отправка    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sender.Close();
                }
            }
        }

        private void MessageTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (MessageTextBox.Text.Length <= 1) return;

            if(e.KeyData.ToString() == "Return")
            {
                metroButton1_Click(null, null);
            }
        }
    }
}
