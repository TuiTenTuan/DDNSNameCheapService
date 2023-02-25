using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SystemUltility;

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

        public async Task Storage(string path, string fileName, List<Profile> profiles)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (FileStream fs = new FileStream(Path.Combine(path, fileName + ".dat"), FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                while (!fs.CanWrite)
                {
                    await Task.Delay(100);
                }

                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                XmlSerializer serializer = new XmlSerializer(typeof(ProfileStorage));

                serializer.Serialize(sw, profiles);

                await Logging.Instance.WriteSystemLogs("Storage profile successfull (" + profiles.Count + " Profiles).");

                sw.Close();
            }
        }

        public async Task Storage(List<Profile> profiles)
        {
            if (!Directory.Exists(SystemVariable.Instance.ProfilesPath()))
            {
                Directory.CreateDirectory(SystemVariable.Instance.ProfilesPath());
            }

            using (FileStream fs = new FileStream(Path.Combine(SystemVariable.Instance.ProfilesPath(), SystemConfig.Instance.DefaultProfileNameFile + ".profile"), FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                while (!fs.CanWrite)
                {
                    await Task.Delay(100);
                }

                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                XmlSerializer serializer = new XmlSerializer(typeof(ProfileStorage));

                serializer.Serialize(sw, profiles);

                await Logging.Instance.WriteSystemLogs("Storage profile successfull (" + profiles.Count + " Profiles).");

                sw.Close();
            }
        }

        public async Task<List<Profile>> LoadProfile()
        {
            List<Profile> profiles = new List<Profile>();

            if (File.Exists(Path.Combine(SystemVariable.Instance.ProfilesPath(), SystemConfig.Instance.DefaultProfileNameFile + ".profile")))
            {
                using (FileStream fs = new FileStream(Path.Combine(SystemVariable.Instance.ProfilesPath(), SystemConfig.Instance.DefaultProfileNameFile + ".profile"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    while (!fs.CanRead)
                    {
                        await Task.Delay(100);
                    }

                    StreamReader sr = new StreamReader(fs, Encoding.UTF8);

                    XmlSerializer serializer = new XmlSerializer(typeof(ProfileStorage));

                    profiles = serializer.Deserialize(sr) as List<Profile>;

                    await Logging.Instance.WriteSystemLogs("Load profile successfull (" + profiles.Count + " Profiles).");

                    sr.Close();
                }
            }
            else
            {
                await Logging.Instance.WriteSystemLogs("Load profile faile " + Path.Combine(SystemVariable.Instance.ProfilesPath(), SystemConfig.Instance.DefaultProfileNameFile + ".profile") + " not found");
            }

            return profiles;
        }

        public async Task<List<Profile>> LoadProfile(string path, string fileName)
        {
            List<Profile> profiles = new List<Profile>();

            if (File.Exists(Path.Combine(path, fileName + ".profile")))
            {
                using (FileStream fs = new FileStream(Path.Combine(path, fileName + ".profile"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    while (!fs.CanRead)
                    {
                        await Task.Delay(100);
                    }

                    StreamReader sr = new StreamReader(fs, Encoding.UTF8);

                    XmlSerializer serializer = new XmlSerializer(typeof(ProfileStorage));

                    profiles = serializer.Deserialize(sr) as List<Profile>;

                    await Logging.Instance.WriteSystemLogs("Load profile successfull (" + profiles.Count + " Profiles).");

                    sr.Close();
                }
            }
            else
            {
                await Logging.Instance.WriteSystemLogs("Load profile faile " + Path.Combine(path, fileName + ".profile") + " not found");
            }

            return profiles;
        }

    }
}
