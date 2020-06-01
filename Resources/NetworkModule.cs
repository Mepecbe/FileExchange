using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;

using System.Diagnostics;
using System.Management;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Drawing;

using WindowsFormsApp1.Resources.ApplicationConfig;
using WindowsFormsApp1.Resources.Enums;
using WindowsFormsApp1.Resources.Log;

using Tulpep.NotificationWindow;

using static System.GC;

/**/
using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Drawing;
using MetroFramework.Controls;
using MetroFramework.Animation;
using MetroFramework.Components;
using MetroFramework.Fonts;
using MetroFramework.Interfaces;
using MetroFramework.Native;
/**/


namespace WindowsFormsApp1.Resources.Network
{
    public struct LocalMachine
    {
        public IPAddress RemoteIp; //Удалённый IP машины
        public string ComputerNickname; //Ник
        public bool trusted; //Доверенный ли
        public MetroTile MachineFormTile; //Его тиль на форме
    }

    static public class LocalMachines
    {
        static public List<LocalMachine> ListLocalMachines = new List<LocalMachine>();
        static public object locker = new object();

        /*================================================================================================*/
        static public bool Add(IPAddress ip, string nickname = "")
        {
            Monitor.Enter(locker);

            foreach (LocalMachine machine in ListLocalMachines)
            {
                //Проверка, есть ли такая машина у нас в листе
                if (ip.ToString() == machine.RemoteIp.ToString()) return false;
            }


            LocalMachine newMachine = new LocalMachine();
            {
                newMachine.RemoteIp = ip;
                newMachine.trusted = false;
                newMachine.ComputerNickname = nickname.Length < 3 ? "Неизвестный маслёнок" : nickname;
            }


            if (ListLocalMachines.Count == 0)
            {
                MetroTile metroTilee = new MetroTile();

                metroTilee.Location = new System.Drawing.Point(69, 69);
                metroTilee.Size = new System.Drawing.Size(175, 183);
                metroTilee.Name = newMachine.ComputerNickname;
                metroTilee.Text = newMachine.ComputerNickname;
                metroTilee.TabIndex = 23;
                metroTilee.ContextMenuStrip = NetworkModule.GlavnForm.contextMenuStrip1;
                metroTilee.MouseEnter += NetworkModule.GlavnForm.metroTile4_MouseEnter;
                metroTilee.TileImage = global::WindowsFormsApp1.Properties.Resources.computer_screen;
                metroTilee.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                metroTilee.UseTileImage = true;

                newMachine.MachineFormTile = metroTilee;

                NetworkModule.GlavnForm.Invoke((MethodInvoker)delegate
                {
                    NetworkModule.GlavnForm.Controls.Add(metroTilee);
                    NetworkModule.GlavnForm.LABEL_START_WORK.Visible = false;
                });
            }
            else
            {
                MetroTile metroTilee = new MetroTile();

                metroTilee.Location = new System.Drawing.Point(ListLocalMachines[ListLocalMachines.Count - 1].MachineFormTile.Location.X + 175 + 30, ListLocalMachines[ListLocalMachines.Count - 1].MachineFormTile.Location.Y);
                metroTilee.Size = new System.Drawing.Size(175, 183);
                metroTilee.Name = newMachine.ComputerNickname;
                metroTilee.Text = newMachine.ComputerNickname;
                metroTilee.TabIndex = 23;
                metroTilee.ContextMenuStrip = NetworkModule.GlavnForm.contextMenuStrip1;
                metroTilee.MouseEnter += NetworkModule.GlavnForm.metroTile4_MouseEnter;
                metroTilee.TileImage = global::WindowsFormsApp1.Properties.Resources.computer_screen;
                metroTilee.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                metroTilee.UseTileImage = true;

                newMachine.MachineFormTile = metroTilee;

                NetworkModule.GlavnForm.Invoke((MethodInvoker)delegate
                {
                    NetworkModule.GlavnForm.Controls.Add(metroTilee);
                    NetworkModule.GlavnForm.Size = new Size(NetworkModule.GlavnForm.Size.Width + 100, NetworkModule.GlavnForm.Size.Height);
                });
            }

            ListLocalMachines.Add(newMachine);
            LogApplication.WriteLog($"Добавлена машина в список {ip.ToString()} с никнеймом {newMachine.ComputerNickname}");

            if (Config.OnFoundNewComputer != null)
            {
                LogApplication.WriteLog("Воспроизведение звука OnFoundComputer");
                Config.OnFoundNewComputer.Play();
            }

            Monitor.Exit(locker);
            return true;
        }

        static public void RemoveMachine(IPAddress ip)
        {
            Monitor.Enter(locker);
            for (int a = 0; a < ListLocalMachines.Count; a++)
            {
                if (ListLocalMachines[a].RemoteIp.ToString() == ip.ToString())
                {
                    NetworkModule.GlavnForm.Controls.Remove(ListLocalMachines[a].MachineFormTile);
                    ListLocalMachines.RemoveAt(a);
                    break;
                }
            }

            RefreshTile();
            Monitor.Exit(locker);
        }

        static public void RemoveMachine(string nickname)
        {
            for (int a = 0; a < ListLocalMachines.Count; a++)
            {
                if (ListLocalMachines[a].ComputerNickname == nickname)
                {
                    NetworkModule.GlavnForm.Controls.Remove(ListLocalMachines[a].MachineFormTile);
                    ListLocalMachines.RemoveAt(a);
                    break;
                }
            }

            RefreshTile();
        }

        /*==========================================================*/


        static public void RefreshTile()
        {
            if (ListLocalMachines.Count == 0)
            {
                NetworkModule.GlavnForm.LABEL_START_WORK.Visible = true;
                NetworkModule.GlavnForm.Size = new Size(521, 305);
                return;
            }

            if (ListLocalMachines.Count != 0)
                ListLocalMachines[0].MachineFormTile.Location = new System.Drawing.Point(69, 69);

            if (ListLocalMachines.Count == 1)
            {
                NetworkModule.GlavnForm.Size = new Size(521, 305);
            }
            else if (ListLocalMachines.Count > 1)
            {
                for (int a = 1; a < ListLocalMachines.Count; a++)
                {
                    ListLocalMachines[a].MachineFormTile.Location =
                        new System.Drawing.Point(ListLocalMachines[a - 1].MachineFormTile.Location.X + 175 + 30,
                                                 ListLocalMachines[a - 1].MachineFormTile.Location.Y);

                    NetworkModule.GlavnForm.Update();
                    Application.DoEvents();
                }

                NetworkModule.GlavnForm.Size = new Size(ListLocalMachines[ListLocalMachines.Count - 1].MachineFormTile.Location.X + 200, NetworkModule.GlavnForm.Height);
            }

        }



        static public string GetNicknameByIP(IPAddress ip)
        {
            foreach (LocalMachine machine in ListLocalMachines)
            {
                //Проверка, есть ли такая машина у нас в листе
                if (ip.ToString() == machine.RemoteIp.ToString()) return machine.ComputerNickname;
            }

            throw new Exception("При запросе ника по айпи, машина с таким айпи не была найдена в листе");
        }







        static public IPAddress GetIPByNickname(string nick)
        {
            foreach (LocalMachine machine in ListLocalMachines)
            {
                //Проверка, есть ли такая машина у нас в листе
                if (nick == machine.ComputerNickname) return machine.RemoteIp;
            }

            throw new Exception("При запросе айпи по нику, машина с таким ником не была найдена в листе");
        }

    }

    static public class NetworkModule
    {
        static public Thread Sender = new Thread(senderr);                                  //Рассылает сообщения с запросом присутствия
        static public Thread Detector = new Thread(Detect);                                 //UDP принимает активность от устройств в сети, тем самым обнаруживая их

        static public Thread ReceiveConnectionThread = new Thread(WaitConnectionThread);    //Ожидает подключения по TCP с каким либо клиентом
        static public Thread GlobalChatReceiveThread = new Thread(GlobalChatReceive);       //Принимает сообщения глобального чата

        static public Thread PingThread = new Thread(CheckMachines);

        static public List<EndPoint> DestinationHosts = new List<EndPoint>(); //Удалённые хосты

        static public string LocalIp;
        static public List<string> ListIp = new List<string>();

        static public IPAddress MulticastGroup = IPAddress.Parse("235.5.5.11");

        static public Form1 GlavnForm;
        static public GlobalChatForm ChatForm;

        static public UdpClient MyUdpClient = new UdpClient(Config.UDP_ACTIVITY);
        static public UdpClient MyUdpClient1;
        static public UdpClient MyUdpSender = new UdpClient();

        static public TcpListener TcpReceiveListener;

        static void senderr()
        {
            LogApplication.WriteLog("[senderr] Поток рассылки активен");

            while (true)
            {
                try
                {
                    foreach (string bufff in ListIp)
                    {
                        string buff = bufff.Substring(0, bufff.LastIndexOf('.') + 1);

                        for (int a = 0; a < 256; a++)
                        {
                            MyUdpSender.Send(Config.Encoder.GetBytes(Config.nickname), Config.Encoder.GetBytes(Config.nickname).Length, new IPEndPoint(IPAddress.Parse(buff + a), Config.UDP_ACTIVITY));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogApplication.WriteLog("[senderr] Произошло исключение ->" + ex.Message);
                    LogApplication.WriteLog("[senderr] Сервис остановлен");
                    return;
                }
                Thread.Sleep(5000);
            }
        }

        static public void Init(Form1 ff)
        {
            GlavnForm = ff;
            List<string> s = new List<string>();

            string StringIp = "";


            {
                //Выделение локального IP
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (LocalIp == null)
                            LocalIp = ip.ToString();

                        StringIp += "\n" + ip.ToString();
                        ListIp.Add(ip.ToString());
                        LogApplication.WriteLog("local ip ->" + ip.ToString());
                    }
                }                
            }


            if (LocalIp == "127.0.0.1")
            {
                LogApplication.WriteLog("Нет доступных сетей кроме loopback, т.к. LocalIp == 127.0.0.1, создание задачи на попытку");

                GlavnForm.Invoke((MethodInvoker)delegate
                {
                    new PopupNotifier()
                    {
                        TitleText = "FileExchange",
                        ContentText = $"Не доступных подключений, кроме локальной петли, проверьте подключение по LAN или Wifi"
                    }.Popup();
                });

                return;
            }
            else
            {
                new Task(() =>
                {
                    Thread.Sleep(30000);
                    GlavnForm.Invoke((MethodInvoker)delegate
                    {
                        new PopupNotifier()
                        {
                            TitleText = "FileExchange",
                            ContentText = $"Локальные адреса: \n{StringIp}"
                        }.Popup();
                    });
                }).Start();
            }

            GlavnForm.timerConnect.Enabled = false;

            ReceiveConnectionThread.Start();
            Thread.Sleep(100);

            Sender.Start();
            Thread.Sleep(100);

            GlobalChatReceiveThread.Start();
            Thread.Sleep(100);

            Detector.Start();
            PingThread.Start();


            GlavnForm.Invoke((MethodInvoker)delegate
            {
                new PopupNotifier()
                {
                    TitleText = "FileExchange",
                    ContentText = $"Сервисы активны\n"
                }.Popup();
            });

        }

        static public void Stop()
        {
            GC.Collect();

            if (MyUdpClient1 != null)
                MyUdpClient1.Close();

            if (TcpReceiveListener != null)
                TcpReceiveListener.Stop();

            Sender.Abort();
            ReceiveConnectionThread.Abort();
            GlobalChatReceiveThread.Abort();
            Detector.Abort();
            PingThread.Abort();


            MyUdpClient.Close();

            GC.Collect();

            Log.LogApplication.WriteLog("[Stop] Всё остановлено ");
        }

        static public void Detect()
        {
            LogApplication.WriteLog("[Detect] Запущен");
            IPEndPoint remoteIp = null;
            string localAddress = LocalIp;

            try
            {
                while (true)
                {
                    byte[] data = MyUdpClient.Receive(ref remoteIp); // получаем данные

                    {
                        bool local = false;
                        foreach (string bufff in ListIp)
                        {
                            if (remoteIp.Address.ToString().Equals(bufff))
                            {
                                local = true;
                                break;
                            }
                        }

                        if (local) continue;
                    }

                    string message = Encoding.Unicode.GetString(data);
                    if (message.Length <= 1) continue;

                    if (LocalMachines.Add(IPAddress.Parse(remoteIp.ToString().Substring(0, remoteIp.ToString().IndexOf(':'))), message))
                    {
                        GlavnForm.Invoke((MethodInvoker)delegate
                        {
                            new PopupNotifier()
                            {
                                TitleText = "FileExchange",
                                ContentText = $"Новый компьютер в локальной сети {remoteIp.ToString().Substring(0, remoteIp.ToString().IndexOf(':'))}\n Никнейм {message}"
                            }.Popup();
                        });
                    }

                    Thread.Sleep(2);
                }
            }
            catch (Exception ex)
            {
                LogApplication.WriteLog($"DETECTOR    ИСКЛЮЧЕНИЕ IP:{remoteIp.ToString()}\n\nMESSAGE\n{ex.Message}\n\nSTACK\n" + ex.StackTrace + "\n\n\n");
            }
            finally
            {
                MyUdpClient.Close();
            }

            LogApplication.WriteLog("[Detector] Конец работы детектора");
        }



        static private void WaitConnectionThread()
        {
            //Ожидает и обрабатывает TCP подключения от компьютеров, которые нас обнаружили в сети
            LogApplication.WriteLog("[WaitConnectionThread] Ожидание подключения");
            IPAddress localAddr = IPAddress.Parse("0.0.0.0");
            TcpReceiveListener = new TcpListener(localAddr, Config.TCP_FILE_TRANSFER_PORT);
            TcpClient client = null;
            TcpReceiveListener.Start();
            Thread.Sleep(3000);

            try
            {
                while (true)
                {
                    if (client == null || !client.Connected)
                    {
                        GlavnForm.Invoke((MethodInvoker)delegate
                        {
                            new PopupNotifier()
                            {
                                TitleText = "FileExchange",
                                ContentText = $"Ожидание TCP соединения"
                            }.Popup();
                        });

                        LogApplication.WriteLog("[WaitTCPConnection] ожидание подключения");
                        client = TcpReceiveListener.AcceptTcpClient();
                        LogApplication.WriteLog("[WaitTCPConnection] подключение установлено");

                        GlavnForm.Invoke((MethodInvoker)delegate
                        {
                            new PopupNotifier()
                            {
                                TitleText = "FileExchange",
                                ContentText = $"Подключение установленно"
                            }.Popup();
                        });

                        FileTransfer fileTransfer = new FileTransfer(client, TcpReceiveListener, IPAddress.Parse(client.Client.RemoteEndPoint.ToString().Substring(0, client.Client.RemoteEndPoint.ToString().LastIndexOf(':'))), GlavnForm);

                        fileTransfer.StyleManager.Theme = GlavnForm.StyleManager.Theme;

                        if (fileTransfer.StyleManager.Theme == MetroThemeStyle.Dark)
                        {
                            fileTransfer.FilesList.BackColor = System.Drawing.SystemColors.ControlDarkDark;
                        }

                        LogApplication.WriteLog("[WaitTCPConnection] Открытие формы");
                        fileTransfer.ShowDialog();
                    }

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                LogApplication.WriteLog("[WaitTCPConnection] Произошла ошибка " + ex.Message);
            }
        }

        static private void GlobalChatReceive()
        {
            Log.LogApplication.WriteLog($"[UDP Reader Worker] Старт потока глобального чата на порту {Config.GlobalChatUdpPort}");

            /*=========*/
            MyUdpClient1 = new UdpClient(Config.GlobalChatUdpPort);
            MyUdpClient1.JoinMulticastGroup(MulticastGroup, 20);
            IPEndPoint remoteIp = null;
            string localAddress = LocalIp;

            try
            {
                while (true)
                {
                    byte[] data = MyUdpClient1.Receive(ref remoteIp); // получаем данные

                    if (remoteIp.Address.ToString().Equals(localAddress))
                        continue;

                    string message = Encoding.Unicode.GetString(data);
                    if (message.Length <= 1) continue;

                    if (ChatForm == null || !ChatForm.ShownForm) { LogApplication.WriteLog("[GlobalMessage] Форма не открыта, пропуск"); continue; }

                    LogApplication.WriteLog($"[GlobalMessage] размер буффера {message.Length}, буффер в строку -> {message}");

                    ChatForm.Invoke((MethodInvoker)delegate
                    {
                        ChatForm.chatTextBox.AppendText(LocalMachines.GetNicknameByIP(IPAddress.Parse(remoteIp.ToString().Substring(0, remoteIp.ToString().IndexOf(':')))) + ": " + message);
                    });

                    Thread.Sleep(2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                MyUdpClient.Close();
            }
            /*=========*/
        }


        static public bool TryConnect(IPAddress remoteIp, ref TcpClient client)
        {
            client = new TcpClient();

            var result = client.BeginConnect(remoteIp.ToString(), Config.TCP_FILE_TRANSFER_PORT, null, null);
            var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));

            if (!success)
            {
                return false;
            }

            return true;
        }



        static public void CheckMachines()
        {
            LogApplication.WriteLog("[PingCheck] работает, проверка существования клиентов в сети каждые 5 секунд");
            Ping ping = new Ping();

            while (true)
            {
                for (int a = 0; a < LocalMachines.ListLocalMachines.Count; a++)
                {
                    try
                    {
                        PingReply reply = ping.Send(LocalMachines.ListLocalMachines[a].RemoteIp);
                        if (reply.Status != IPStatus.Success)
                        {
                            bool result = false;

                            for (int aa = 0; aa < 3; aa++)
                            {
                                reply = ping.Send(LocalMachines.ListLocalMachines[a].RemoteIp);
                                if (reply.Status == IPStatus.Success)
                                {
                                    result = true;
                                    break;
                                }
                            }

                            if (!result)
                            {
                                LogApplication.WriteLog("[PingCheck] Обнаружена машина, которая недоступна, удаление");
                                LocalMachines.RemoveMachine(LocalMachines.ListLocalMachines[a].RemoteIp);
                                break;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        LogApplication.WriteLog($"[PingCheck] Исключение \n{ex.Message}\nSTACK\n{ex.StackTrace}\n[PingCheck] Удаляю машину {LocalMachines.ListLocalMachines[a].ComputerNickname}:{LocalMachines.ListLocalMachines[a].RemoteIp.ToString()}");
                    }
                }

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
    }
}