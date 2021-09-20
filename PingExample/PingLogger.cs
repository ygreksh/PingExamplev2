using System;
using System.IO;

namespace PingExample
{
    public class PingLogger
    {
        //private string HostsFileName = "hosts.json";
        private string LogsFileName = "logs.txt";

        public void WriteLog(string logstring)
        {
            try
            {
                //bool fExists = File.Exists(distStorage);
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