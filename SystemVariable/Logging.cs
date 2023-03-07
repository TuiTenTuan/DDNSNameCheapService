using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SystemVariable;

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

        public async Task WriteSystemLogsAsync(string message)
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

        public void WriteSystemLogs(string message)
        {
            Task t = WriteSystemLogsAsync(message);
            t.Start();
        }

        public async Task WriteApplicationLogsAsync(UpdateLogging logging)
        {
            string logsFileName = Path.Combine(SystemVariable.Instance.ApplicationLogsPath(), DateTime.Now.ToString("ddMMyyyy") + ".logs");

            List<UpdateLogging> loggings = new List<UpdateLogging>();

            if (File.Exists(logsFileName))
            {
                using (FileStream fs = new FileStream(logsFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    while (!fs.CanRead)
                    {
                        await Task.Delay(100);
                    }

                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<UpdateLogging>));

                        try
                        {
                            loggings = serializer.Deserialize(sr) as List<UpdateLogging>;
                            WriteSystemLogs("Read application log success!");
                        }
                        catch (Exception ex)
                        {
                            WriteSystemLogs("Read application log fail. " + ex.Message);
                        }

                        sr.Close();
                    }

                    fs.Close();
                }
            }

            loggings.Add(logging);

            using (FileStream fs = new FileStream(logsFileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                while (!fs.CanWrite)
                {
                    await Task.Delay(100);
                }

                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<UpdateLogging>));

                    try
                    {
                        serializer.Serialize(sw, loggings);
                        WriteSystemLogs("File Logs Update " + logging.ID + " success");
                    }
                    catch (Exception ex)
                    {
                        WriteSystemLogs("File Logs Update " + logging.ID + " fail. " + ex.Message);
                    }

                    sw.Close();
                }

                fs.Close();
            }
        }

        public void WriteApplicationLogs(UpdateLogging logging)
        {
            Task t = WriteApplicationLogsAsync(logging);
            t.Start();
        }

        public async Task<List<UpdateLogging>> ReadLogs()
        {
            string logsFileName = Path.Combine(SystemVariable.Instance.ApplicationLogsPath(), DateTime.Now.ToString("ddMMyyyy") + ".logs");

            List<UpdateLogging> logs = new List<UpdateLogging>();

            using (FileStream fs = new FileStream(logsFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                while (!fs.CanRead)
                {
                    await Task.Delay(100);
                }

                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<UpdateLogging>));

                    try
                    {
                        logs = serializer.Deserialize(sr) as List<UpdateLogging>;
                        WriteSystemLogs("Read logs successfull");
                    }
                    catch(Exception e)
                    {
                        WriteSystemLogs("Read logs fail. " + e.Message);
                    }

                    sr.Close();
                }

                fs.Close();
            }

            return logs;
        }
    }
}
