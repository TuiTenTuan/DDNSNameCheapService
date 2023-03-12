namespace SystemUltility.Configs
{
    public class SystemConfig
    {
        private static SystemConfig instance;

        public static SystemConfig Instance
        {
            get { if (instance == null) instance = new SystemConfig(); return instance; }
            private set { instance = value; }
        }

        private SystemConfig() { }

        public string AppName = "DDNSService";

        public string Logs = "Logs";

        public string Setting = "Settting";

        public string Profiles = "Profiles";

        public string SystemLogs = "System";

        public string ApplicationLogs = "Run";

        public string DefaultProfileNameFile = "Profile";

        public string CommunicationServerSend = "Send";

        public string CommunicationServerRead = "Read";
    }
}
