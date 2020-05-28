using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace WindowsFormsApp1.Resources.Log
{
    static public class LogApplication
    {
        static private StreamWriter streamWriter;

        static public void Init()
        {
            streamWriter = new StreamWriter($"AppLog.log", false);
            streamWriter.WriteLine("Старт лога " + DateTime.Now.ToString());

            streamWriter.AutoFlush = true;
        }

        static public void WriteLog(string str)
        {
            streamWriter.WriteLine(str);
        }

        static public void StopLog()
        {
            streamWriter.Close();
        }
    }
}
