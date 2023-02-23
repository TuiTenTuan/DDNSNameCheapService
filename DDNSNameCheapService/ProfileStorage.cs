using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDNSNameCheapService
{
    public class ProfileStorage
    {
        private static ProfileStorage instance;

        public static ProfileStorage Instance
        {
            get { if (instance == null) instance = new ProfileStorage(); return instance; }
            private set { instance = value; }
        }

        private ProfileStorage() { }

        public Task Storage(string path, string fileName, List<Profile> profiles)
        {
            return new Task(() =>
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (StreamWriter sw = new StreamWriter(new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.Write, FileShare.Read)))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ProfileStorage));

                                       
                }
            });


        }

    }
}
