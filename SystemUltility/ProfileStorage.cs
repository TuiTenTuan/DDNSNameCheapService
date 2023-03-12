using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System;
using SystemUltility.Entities;
using SystemUltility.Configs;

namespace SystemUltility
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

                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ProfileStorage));

                    try
                    {
                        serializer.Serialize(sw, profiles);

                        Logging.Instance.WriteSystemLogs("Storage profile successfull (" + profiles.Count + " Profiles).");
                    }
                    catch (Exception e)
                    {
                        Logging.Instance.WriteSystemLogs("Storage profile fail (" + profiles.Count + " Profiles). " + e.Message);
                    }

                    sw.Close();
                }

                fs.Close();
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

                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ProfileStorage));

                    try
                    {
                        serializer.Serialize(sw, profiles);

                        Logging.Instance.WriteSystemLogs("Storage profile successfull (" + profiles.Count + " Profiles).");
                    }
                    catch (Exception e)
                    {
                        Logging.Instance.WriteSystemLogs("Storage profile fail (" + profiles.Count + " Profiles). " + e.Message);
                    }

                    sw.Close();
                }

                fs.Close();
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

                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ProfileStorage));

                        try
                        {
                            profiles = serializer.Deserialize(sr) as List<Profile>;

                            Logging.Instance.WriteSystemLogs("Load profile successfull (" + profiles.Count + " Profiles).");
                        }
                        catch (Exception e)
                        {
                            Logging.Instance.WriteSystemLogs("Load profile successfull (" + profiles.Count + " Profiles). " + e.Message);
                        }

                        sr.Close();
                    }

                    fs.Close();
                }
            }
            else
            {
                Logging.Instance.WriteSystemLogs("Load profile faile " + Path.Combine(SystemVariable.Instance.ProfilesPath(), SystemConfig.Instance.DefaultProfileNameFile + ".profile") + " not found");
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

                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ProfileStorage));

                        try
                        {
                            profiles = serializer.Deserialize(sr) as List<Profile>;

                            Logging.Instance.WriteSystemLogs("Load profile successfull (" + profiles.Count + " Profiles).");
                        }
                        catch (Exception e)
                        {
                            Logging.Instance.WriteSystemLogs("Load profile successfull (" + profiles.Count + " Profiles). " + e.Message);
                        }

                        sr.Close();
                    }

                    fs.Close();
                }
            }
            else
            {
                Logging.Instance.WriteSystemLogs("Load profile faile " + Path.Combine(path, fileName + ".profile") + " not found");
            }

            return profiles;
        }
    }
}
