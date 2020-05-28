using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Resources.Enums
{
    public enum PacketIdentification : byte
    {
        GlobalChatMessage = 0,    //Сообщение глобального чата
        OpenLocalChatRequest = 1, //Сообщение запроса открытия локального чата

        TransferFileRequest = 3, //Передача файла
            
        LocalChatMessage = 4,     //Пакет с сообщением личного чата
        ClientActive = 5          //Пакет активности
    }

    public enum TransferType : byte
    {
        UploadFile = 1,
        ReceiveFile = 2
    }
}
