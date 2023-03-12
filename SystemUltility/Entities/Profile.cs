using System;

namespace SystemUltility.Entities
{
    public class Profile
    {
        public Guid ID { get; set; }

        public string Domain { get; set; }

        public string Host { get; set; }

        public string Key { get; set; }

        public int Interval { get; set; }

        public string IP { get; set; }

        public string DomainName { get; set; }

        public bool IsDomainName { get; set; }

        public string GetHost { get { return Host + "." + Domain; } }

        public Profile(Guid iD, string domain, string host, string key, int interval, string iP, string domainName, bool isDomainName)
        {
            ID = iD;
            Domain = domain;
            Host = host;
            Key = key;
            Interval = interval;
            IP = iP;
            DomainName = domainName;
            IsDomainName = isDomainName;
        }

        public Profile(string domain, string host, string key, int interval, string ip) : this(Guid.NewGuid(), domain, host, key, interval, ip, string.Empty, false) { }

        public Profile(string domain, string host, string domainName, string key, int interval) : this(Guid.NewGuid(), domain, host, key, interval, string.Empty, domainName, true) { }

        public Profile() : this(Guid.NewGuid(), string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, false) { }
    }
}
