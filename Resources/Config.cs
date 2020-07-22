using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Media;

using WindowsFormsApp1.Resources.Log;

namespace WindowsFormsApp1.Resources.ApplicationConfig
{
    public static class Config
    {
        public const int UDP_ACTIVITY = 2228;   //Порт для обнаружения друг друга в локалке
        public const int UDP_FILE_NAME_RECEIVE = 2229;  //Порт для приёма имени файлов
        public const int TCP_FILE_TRANSFER_PORT = 2230; //Порт для пересылки файлов по TCP
        public const int GlobalChatUdpPort = 2231; //ПОРТ ДЛЯ ГЛОБАЛЬНЫХ СООБЩЕНИЙ

        public const int bufferSize = 256; //Для UDP

        static public string FilesDir = Environment.CurrentDirectory + "\\Files\\";
        static public Encoding Encoder = Encoding.Unicode;

        public const string GlobalChatExitMessage = "Вышел из чата и был справедливо попущен";
        public static bool enableSound = true;

        public static string nickname = "";


        /*Для воспроизведения звуков*/
        public static SoundPlayer OnFoundNewComputer;
        public static SoundPlayer OnReceiveFile;
        public static SoundPlayer OnOpenConnect;
        public static SoundPlayer OnCloseConnect;

        public static void Init()
        {
            LogApplication.WriteLog("***InitConfig***");

            {
                LogApplication.WriteLog(" Load file");

                if (!File.Exists("conf.txt"))
                {
                    LogApplication.WriteLog(" Create config file");
                    File.Create("conf.txt"); 
                    File.WriteAllText("conf.txt", "Default\n1"); 
                }
                StreamReader reader = new StreamReader("conf.txt");
                nickname = reader.ReadLine();
                LogApplication.WriteLog($" Load nickname -> {nickname}");
                enableSound = reader.ReadLine() == "1" ? true : false;
                LogApplication.WriteLog($" Sound -> {enableSound}");
                reader.Close();
            }


            if (enableSound)
            {
                OnFoundNewComputer = null;
                OnCloseConnect = null;
                OnReceiveFile = null;
                OnOpenConnect = null;
                LogApplication.WriteLog("***End init players, sound off***");
            }
            else
            {
                if (File.Exists("Others/OnFoundComputer.wav"))
                {
                    OnFoundNewComputer = new SoundPlayer("Others/OnFoundComputer.wav");
                    LogApplication.WriteLog("  Loaded OnFoundComputer.wav");
                }

                if (File.Exists("Others/OnReceiveFile.wav"))
                {
                    OnReceiveFile = new SoundPlayer("Others/OnReceiveFile.wav");
                    LogApplication.WriteLog("  Loaded OnReceiveFile.wav");
                }

                if (File.Exists("Others/OnOpenConnect.wav"))
                {
                    OnOpenConnect = new SoundPlayer("Others/OnOpenConnect.wav");
                    LogApplication.WriteLog("  Loaded OnOpenConnect.wav");
                }

                if (File.Exists("Others/OnCloseConnect.wav"))
                {
                    OnCloseConnect = new SoundPlayer("Others/OnCloseConnect.wav");
                    LogApplication.WriteLog("  Loaded OnCloseConnect.wav");
                }
            }

            LogApplication.WriteLog("***EndInitConfig***");
        }



    }
}
