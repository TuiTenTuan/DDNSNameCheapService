using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SystemUltility
{
    public class Logging
    {
        private static Logging instance;

        public static Logging Instance
        {
            get { if (instance == null) instance = new Logging(); return instance; }
            private set { instance = value; }
        }

        private Logging()
        {
            if (!Directory.Exists(SystemVariable.Instance.SystemLogsPath()))
            {
                Directory.CreateDirectory(SystemVariable.Instance.SystemLogsPath());
            }

            if (!Directory.Exists(SystemVariable.Instance.ApplicationLogsPath()))
            {
                Directory.CreateDirectory(SystemVariable.Instance.ApplicationPath());
            }
        }

        public async Task WriteSystemLogs(string message)
        {
            StackTrace st = new StackTrace();

            string calledClassName = st.GetFrame(1).GetMethod().DeclaringType.Name;

            string calledName = st.GetFrame(1).GetMethod().Name;

            string currentTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:s");

            string logsFileName = Path.Combine(SystemVariable.Instance.SystemLogsPath(), "Logs.log");

            using (FileStream fs = new FileStream(logsFileName, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                while (!fs.CanWrite)
                {
                    await Task.Delay(100);
                }

                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                await sw.WriteLineAsync(currentTime + " - " + calledClassName + "." + calledName + " == " + message);

                sw.Close();
            }
        }

        public async Task WriteApplicationLogs(string message)
        {

        }
    }
}
