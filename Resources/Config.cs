using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
    }
}
