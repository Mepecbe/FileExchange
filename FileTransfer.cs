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

//using System.Diagnostics;
using System.Management;

using WindowsFormsApp1.Resources.Log;
using WindowsFormsApp1.Resources.Enums;
using WindowsFormsApp1.Resources.Network;
using WindowsFormsApp1.Resources.ApplicationConfig;

using System.IO;

using Tulpep.NotificationWindow;

namespace WindowsFormsApp1
{
    enum packetType
    {
        endUpload = '0', //При приёме, закрываем файловый стрим
        startUpload = '1',   //При отправке, клиент на другой стороне создаёт файловый стрим, куда будет писать всё, что примет по TCP
        chatMessage = '2',  //Сообщение чата
        remoteCloseConn = '3' //Удалённый комп закрыл соединение
    }

    public partial class FileTransfer : MetroFramework.Forms.MetroForm
    {
        public string ReceiveFileNameBuffer = ""; //Имя файла, которое принимали

        public bool UserCloseForm = false;
        public bool RemoteUserClose = false;

        public TcpClient tcpClient; //Соединение с удалёнными компьютером
        public TcpListener tcpServer;
        public UdpClient client = new UdpClient();
        public UdpClient MyUdpClient = new UdpClient();

        public Thread UdpMessageReceiverHandler;
        public Thread TcpFileBytesReceiver;
        public Form1 GlavnForm;

        public IPAddress RemoteIp;

        public BinaryWriter FileWriter;

        public FileTransfer(TcpClient client, TcpListener listener, IPAddress ip, Form1 form)
        {
            InitializeComponent();
            UdpMessageReceiverHandler = new Thread(UdpConnMessageHandler);
            TcpFileBytesReceiver = new Thread(TcpBytesReaderFunc);

            this.GlavnForm = form;

            this.tcpClient = client;
            this.tcpServer = listener;

            this.RemoteIp = ip;

            UdpMessageReceiverHandler.Start();
            TcpFileBytesReceiver.Start();

            this.Focus();
        }

        public void ClickPopupOnReceiveEnd(object sender, EventArgs args)
        {
            System.Diagnostics.Process.Start("Files\\" + ReceiveFileNameBuffer);
        }

        private void UdpConnMessageHandler()
        {
            LogApplication.WriteLog("[UdpConnMessageHandler] Хандлер приёма имени файла по UDP активен");

            IPEndPoint remoteIp = null;
            MyUdpClient = new UdpClient(Config.UDP_FILE_NAME_RECEIVE);

            try
            {
                while (true)
                {
                    byte[] data = MyUdpClient.Receive(ref remoteIp); // получаем данные

                    string fileName = Encoding.Unicode.GetString(data);
                    if (fileName.Length <= 2) continue;

                    if (fileName[0] == '1')
                    {
                        fileName = fileName.Substring(1, fileName.Length - 1);
                        LogApplication.WriteLog($"[UdpConnMessageHandler] ->{fileName}<- открываю на запись");

                        GlavnForm.Invoke((MethodInvoker)delegate
                        {
                            new PopupNotifier()
                            {
                                TitleText = "FileExchange",
                                ContentText = $"Приём файла {fileName}"
                            }.Popup();
                        });

                        FileWriter = new BinaryWriter(new FileStream("Files\\" + fileName, FileMode.Create));
                        ReceiveFileNameBuffer = fileName;

                        ListViewItem item = this.FilesList.Items.Add((this.FilesList.Items.Count + 1).ToString());
                        item.SubItems.Add(fileName);
                        item.SubItems.Add(228.ToString());
                        item.SubItems.Add("Принимаю");
                    }
                    else if (fileName[0] == '0')
                    {
                        LogApplication.WriteLog($"[UdpConnMessageHandler] Закрываю запись");
                        FileWriter.Close();

                        setReceiveEnd(ReceiveFileNameBuffer);
                        
                        GlavnForm.Invoke((MethodInvoker)delegate
                        {
                            PopupNotifier popup = new PopupNotifier()
                            {
                                TitleText = "FileExchange",
                                ContentText = $"Завершен приём файла {ReceiveFileNameBuffer}"
                            };

                            popup.Click += ClickPopupOnReceiveEnd;
                            popup.Popup();
                        });
                    }
                    else if (fileName[0] == '2')
                    {
                        LogApplication.WriteLog("[UdpConnMessageHandler] Принял сообщение локального чата -> " + fileName);
                        this.LocalChat.AppendText(fileName.Substring(1, fileName.Length - 1));
                    }
                    else if (fileName[0] == '3')
                    {
                        LogApplication.WriteLog("[UdpConnMessageHandler] Удалённый компьютер прислал сообщение о том, что пользователь закрывает форму");
                        this.RemoteUserClose = true;
                    }
                    else
                    {
                        LogApplication.WriteLog($"[WARNING] -------> Принял неизвестный пакет ->{fileName}<-");
                    }

                    Thread.Sleep(2);
                }
            }
            catch (Exception ex)
            {
                LogApplication.WriteLog("[UdpConnMessageHandler] " + ex.Message);
            }
            finally
            {
                MyUdpClient.Close();
            }

            LogApplication.WriteLog("[UdpConnMessageHandler] Конец работы");
        }

        public void TcpBytesReaderFunc()
        {
            LogApplication.WriteLog("[TcpBytesReader] Активен и ждёт пакеты по TCP");
            long ReceiveBytes = 0;
            try
            {
                while (true)
                {
                    using (var stream = tcpClient.GetStream())
                    {
                        LogApplication.WriteLog("[TcpBytesReader] Клиент подключен. Начинаю приём байтов файла");

                        // чтение 1 килобайта
                        var buffer = new byte[1024];
                        int bytesRead = 0;

                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            try
                            {
                                LogApplication.WriteLog($"[TcpBytesReader] read > 0, -> {bytesRead}, write to stream....");
                                FileWriter.Write(buffer, 0, bytesRead);
                                LogApplication.WriteLog($"[TcpBytesReader] write {bytesRead} bytes to stream success");

                                //ReceiveBytes += bytesRead;
                                //LogApplication.WriteLog($"[TcpBytesReader] Receive bytes {bytesRead}/ ALL {ReceiveBytes}");
                            }
                            catch (Exception ex) 
                            {
                                LogApplication.WriteLog("[TcpBytesReader] reading exception " + ex.Message); 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogApplication.WriteLog($"[TcpBytesReader] Исключение \n{ex.Message} \n {ex.StackTrace} \n**************************");
                GlavnForm.Invoke((MethodInvoker)delegate
                {
                    if (!UserCloseForm)
                    {
                        new PopupNotifier()
                        {
                            TitleText = "FileExchange",
                            ContentText = $"[TcpBytesReader] Возникла ошибка, связь была потерянна",
                            Delay = 10
                        }.Popup();
                    }
                });

                if (!UserCloseForm)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Close();
                    });
                }
            }
        }

        #region events
        private void FileTransfer_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserCloseForm = true;
            client.Send(Config.Encoder.GetBytes("33aaaf"), Config.Encoder.GetBytes("33aaaf").Length, new IPEndPoint(RemoteIp, Config.UDP_FILE_NAME_RECEIVE));
            Thread.Sleep(10);

            LogApplication.WriteLog("[Закрытие формы передачи файлов] Закрытие клиентов UPD и TCP");

            {
                //Закрытие соединений
                client.Close();
                MyUdpClient.Close();
                tcpClient.Close();
            }

            LogApplication.WriteLog("[Закрытие формы передачи файлов] Закрытие потоков");
            {
                //Закрытие потоков
                if(UdpMessageReceiverHandler.ThreadState == ThreadState.Running)
                    UdpMessageReceiverHandler.Abort();
                
                if(TcpFileBytesReceiver.ThreadState == ThreadState.Running)
                    TcpFileBytesReceiver.Abort();
            }

            if (!RemoteUserClose && !UserCloseForm)
                GlavnForm.Invoke((MethodInvoker)delegate
                {
                    LogApplication.WriteLog("[Закрытие формы передачи файлов] Закрыто по неизвестной причине");
                    new PopupNotifier()
                    {
                        TitleText = "FileExchange",
                        ContentText = $"Соединение закрыто по неизвестной причине"
                    }.Popup();
                });
            else if(RemoteUserClose)
                GlavnForm.Invoke((MethodInvoker)delegate
                {
                    LogApplication.WriteLog("[Закрытие формы передачи файлов] Удалённый комп сам закрыл соединение");
                    new PopupNotifier()
                    {
                        TitleText = "FileExchange",
                        ContentText = $"Удалённый компьютер закрыл соединение с вами"
                    }.Popup();
                });
            else if (UserCloseForm)
            {
                LogApplication.WriteLog("[Закрытие формы передачи файлов] Мы сами закрыли соединение");
                GlavnForm.Invoke((MethodInvoker)delegate
                {
                    new PopupNotifier()
                    {
                        TitleText = "FileExchange",
                        ContentText = $"Закрыл соединение с удалённым компом"
                    }.Popup();
                });
            }
        }

        private void передатьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogApplication.WriteLog("\n\n[Передача файла EVENT] Открытие диалога выбора файла");
            this.TransferFile_Progress.Value = 0;

            GlavnForm.Invoke((MethodInvoker)delegate
            {
                AddFileDialog.ShowDialog();
            });
            
            LogApplication.WriteLog("[Передача файла EVENT] Проверка количества выбранных файлов");

            if (AddFileDialog.FileNames.Length == 0)
            {
                LogApplication.WriteLog("[Передача файла EVENT] Файлы не были выбранны");
                return;
            }
            
            LogApplication.WriteLog("[Передача файла EVENT] Создание item'a в таблице");

            ListViewItem item = this.FilesList.Items.Add((this.FilesList.Items.Count + 1).ToString());
            item.SubItems.Add(AddFileDialog.FileNames[0].Substring(AddFileDialog.FileNames[0].LastIndexOf('\\') + 1));
            FileInfo fileInfo = new FileInfo(AddFileDialog.FileNames[0]);
            item.SubItems.Add("228"); //item.SubItems.Add((fileInfo.Length / 1000000.0).ToString() + " МБ");
            item.SubItems.Add("Отправляется");

            
            LogApplication.WriteLog("[Передача файла EVENT] ApplicationDoEvents");
            Application.DoEvents();

            client = new UdpClient();
            string fileName = "1" + AddFileDialog.FileNames[0].Substring(AddFileDialog.FileNames[0].LastIndexOf('\\') + 1, AddFileDialog.FileNames[0].Length - AddFileDialog.FileNames[0].LastIndexOf('\\') - 1);

            LogApplication.WriteLog($"[Передача файла EVENT] Передача клиенту названия файла {fileName}");
            client.Send(Config.Encoder.GetBytes(fileName), Config.Encoder.GetBytes(fileName).Length, RemoteIp.ToString(), Config.UDP_FILE_NAME_RECEIVE);

            //tcpClient.GetStream().Write(Config.Encoder.GetBytes("1" + AddFileDialog.FileNames[0]), 0, Config.Encoder.GetBytes("1" + AddFileDialog.FileNames[0]).Length);

            LogApplication.WriteLog("[Передача файла EVENT] Передано, начинаю передачу по TCP -> " + AddFileDialog.FileNames[0] + "\n\n\n");
            BinaryReader reader = new BinaryReader(new FileStream(AddFileDialog.FileNames[0], FileMode.Open));

            this.TransferFile_Progress.Maximum = (int)reader.BaseStream.Length;

            long sendedBytes = 0;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                try
                {
                    byte[] ReadBytes = reader.ReadBytes(1024);
                    tcpClient.GetStream().Write(ReadBytes, 0, ReadBytes.Length);

                    sendedBytes += ReadBytes.Length;
                    this.TransferFile_Progress.Value += ReadBytes.Length;
                    LogApplication.WriteLog($"SEND bytes {ReadBytes.Length.ToString()}/{sendedBytes}");
                    LogApplication.WriteLog($"Stream position {reader.BaseStream.Position}/{reader.BaseStream.Length}\n");
                }
                catch (Exception Ex)
                {
                    LogApplication.WriteLog("[SendBytes] exception \n" + Ex.Message);
                }

                Application.DoEvents();
            }

            Thread.Sleep(300);
            LogApplication.WriteLog("[Передача файла EVENT] Типа передано, отправляю сигнал о закрытии потока");
            client.Send(Config.Encoder.GetBytes("00zdkf"), Config.Encoder.GetBytes("00zdkf").Length, RemoteIp.ToString(), Config.UDP_FILE_NAME_RECEIVE);

            LogApplication.WriteLog("[Передача файла EVENT] Конец");

            item.SubItems[3].Text = "Передано";
        }
        #endregion


        private void setReceiveEnd(string fileName)
        {
            foreach (ListViewItem item in this.FilesList.Items)
            {
                if (item.SubItems[1].Text == fileName)
                {
                    item.SubItems[3].Text = "Принято";
                }
            }
        }


        private void FileTransfer_Load(object sender, EventArgs e)
        {

        }

        private void LocalChat_TextChanged(object sender, EventArgs e)
        {
            ((RichTextBox)sender).Text += "\n";
        }

        private void LocalChatText_KeyDown(object sender, KeyEventArgs e)
        {
            if (LocalChatText.Text.Length <= 1) return;

            if (e.KeyData.ToString() == "Return")
            {
                LogApplication.WriteLog($"[Передача файла EVENT] Передача клиенту сообщения {LocalChatText.Text} машине {RemoteIp.ToString()}");
                string sendBuff = $"2{Config.nickname}:{LocalChatText.Text}";
                client.Send(Config.Encoder.GetBytes(sendBuff), Config.Encoder.GetBytes(sendBuff).Length, RemoteIp.ToString(), Config.UDP_FILE_NAME_RECEIVE);
                this.LocalChat.AppendText("ВЫ: " + LocalChatText.Text);
                this.LocalChatText.Text = "";
            }
        }
    }
}