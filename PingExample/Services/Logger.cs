using System;
using System.IO;

namespace PingExample
{
    public class Logger : Interfaces.ILogger
    {
        //private string HostsFileName = "hosts.json";
        private string LogsFileName;

        public Logger(string logsFileName)
        {
            LogsFileName = logsFileName;
        }

        public void WriteLog(string logstring)
        {
            try
            {
                
                using (StreamWriter sw = new StreamWriter(LogsFileName, true, System.Text.Encoding.Default))
                {
                    sw.WriteLineAsync(logstring);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Ошибка записи логов", e);
            }
        }
    }
}