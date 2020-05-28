using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

using WindowsFormsApp1.Resources.Log;
using WindowsFormsApp1.Resources.Enums;
using WindowsFormsApp1.Resources.Network;
using WindowsFormsApp1.Resources.ApplicationConfig;

using System.IO;

using Tulpep.NotificationWindow;

namespace WindowsFormsApp1
{
    public partial class FileSender : MetroFramework.Forms.MetroForm
    {
        string[] pathFiles = new string[0];
        IPAddress ip;

        public FileSender(IPAddress ip)
        {
            InitializeComponent();
            this.Text += " с " + ip;
            this.ip = ip;
        }

        private void FileSender_Load(object sender, EventArgs e){ }

        private void добавитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFileDialog.ShowDialog();
            if (AddFileDialog.FileNames.Length == 0) return;

            int index = pathFiles.Length;
            Array.Resize(ref pathFiles, pathFiles.Length + AddFileDialog.FileNames.Length);
            
            foreach (string a in AddFileDialog.FileNames)
            {
                ListViewItem item = this.FilesList.Items.Add((this.FilesList.Items.Count + 1).ToString());
                item.SubItems.Add(a.Substring(a.LastIndexOf('\\') + 1));
                FileInfo fileInfo = new FileInfo(a);
                item.SubItems.Add((fileInfo.Length / 1000000.0).ToString() + " МБ");
                item.SubItems.Add("Готов к отправке");

                pathFiles[index++] = a;

                LogApplication.WriteLog("[SendFileForm] В список к отправке добавлен файл " + a);
            }

        }

        private void удалитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilesList.SelectedItems.Count == 0) return;
            
            this.pathFiles[FilesList.SelectedItems[0].Index] = "";

            LogApplication.WriteLog("[SendFileForm] Из списка к отправке удалён файл " + this.FilesList.SelectedItems[0].SubItems[1].Text);
            this.FilesList.SelectedItems[0].Remove();
        }

        private void SendFiles_Button_Click(object sender, EventArgs e)
        {
            SendFile_Progress.Maximum = FilesList.Items.Count;

            for(int a = 0; a < FilesList.Items.Count; a++)
            {
                if (File.Exists(pathFiles[a]))
                {
                    FilesList.Items[a].SubItems[3].Text = "Передача";
                    Label_State.Text = FilesList.Items[a].SubItems[1].Text;
                    Label_State.Update();

                    LogApplication.WriteLog($"[SendFileForm] Начало передачи файла {pathFiles[a]}");
                    LogApplication.WriteLog($"[SendFileForm] Отправка предупреждения о начале передачи");

                    byte[] sendBuff = new byte[2 + FilesList.Items[a].SubItems[1].Text.Length];
                    sendBuff[0] = (byte)PacketIdentification.TransferFileRequest;
                    sendBuff[1] = (byte)FilesList.Items[a].SubItems[1].Text.Length;
                    
                    Array.Copy(Config.Encoder.GetBytes(FilesList.Items[a].SubItems[1].Text), 0, 
                               sendBuff, 2, Config.Encoder.GetBytes(FilesList.Items[a].SubItems[1].Text).Length);

                    NetworkModule.UdpClient.SendTo(sendBuff, new IPEndPoint(ip, Config.LocalPort));
                    Thread.Sleep(30);

                    TcpClient tcpClient = new TcpClient();

                    
                    for (int count = 0; count < 9; count++)
                    {
                        tcpClient.Connect(ip, 2229);

                        if (!tcpClient.Connected) break;

                        Thread.Sleep(10);
                        Application.DoEvents();
                    }

                    if (!tcpClient.Connected)
                    {

                        NetworkModule.GlavnForm.Invoke((MethodInvoker)delegate
                        {
                            PopupNotifier popp = new PopupNotifier()
                            {
                                TitleText = "FileExchange",
                                ContentText = $"Соединение не установлено\n"
                            };

                            popp.Popup();
                        });

                        break;
                    }

                    BinaryReader SendFileReader = new BinaryReader(new FileStream(pathFiles[a], FileMode.Open));

                    {
                        PopupNotifier pop = new PopupNotifier()
                        {
                            TitleText = "FileExchange",
                            ContentText = $"Соединение установленно, начало передачи"
                        };
                        pop.Popup();
                    }

                    NetworkStream stream = tcpClient.GetStream();

                    while (true)
                    {
                        stream.Write(SendFileReader.ReadBytes(200), 0, 200);

                        if (SendFileReader.BaseStream.Position == SendFileReader.BaseStream.Length - 1)
                        {
                            //Конец передачи
                            break;
                        }
                    }

                    {
                        PopupNotifier pop = new PopupNotifier()
                        {
                            TitleText = "FileExchange",
                            ContentText = $"Соединение установленно, начало передачи"
                        };
                        pop.Popup();
                    }

                    tcpClient.Close();
                    SendFileReader.Close();

                }


                //Thread.Sleep(1000);
            }
        }

        
    }
}