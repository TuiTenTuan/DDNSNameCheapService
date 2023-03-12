using System;

namespace SystemUltility.Entities
{
    public class UpdateLogging : Profile
    {
        public DateTime Date { get; set; }

        public string Message { get; set; }

        public UpdateLogging(Profile profile, DateTime date, string message)
        {
            this.ID = Guid.NewGuid();
            this.Domain = profile.Domain;
            this.Host = profile.Host;
            this.Interval = profile.Interval;
            this.IP = profile.IP;
            this.DomainName = profile.DomainName;
            this.IsDomainName = profile.IsDomainName;
            this.Date = date;
            this.Message = message;
        }

        public UpdateLogging() : this(new Profile(), DateTime.Now, "") { }
    }
}
