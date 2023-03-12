using System;
using System.IO;

namespace SystemUltility.Configs
{
    public class SystemVariable
    {
        private static SystemVariable instance;

        public static SystemVariable Instance
        {
            get { if (instance == null) instance = new SystemVariable(); return instance; }
            private set { instance = value; }
        }
        private SystemVariable() { }

        public string LocalAppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public string ApplicationPath()
        {
            return Path.Combine(LocalAppPath, SystemConfig.Instance.AppName);
        }

        public string LogsPath()
        {
            return Path.Combine(ApplicationPath(), SystemConfig.Instance.Logs);
        }

        public string SystemLogsPath()
        {
            return Path.Combine(LogsPath(), SystemConfig.Instance.SystemLogs);
        }

        public string ApplicationLogsPath()
        {
            return Path.Combine(LogsPath(), SystemConfig.Instance.ApplicationLogs);
        }

        public string SettingPath()
        {
            return Path.Combine(ApplicationPath(), SystemConfig.Instance.Setting);
        }

        public string ProfilesPath()
        {
            return Path.Combine(ApplicationPath(), SystemConfig.Instance.Profiles);
        }

        public string PipeNameServerSent()
        {
            return SystemConfig.Instance.AppName + "_" + SystemConfig.Instance.CommunicationServerSend;
        }

        public string PipeNameServerRead()
        {
            return SystemConfig.Instance.AppName + "_" + SystemConfig.Instance.CommunicationServerRead;
        }
    }
}
