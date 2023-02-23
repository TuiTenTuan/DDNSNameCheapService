namespace DDNSNameCheapService
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

    }
}
