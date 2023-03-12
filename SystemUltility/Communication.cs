using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SystemUltility.Configs;
using SystemUltility.InternalEntities;

namespace SystemUltility
{
    public class Communication
    {
        public CommunationType Type { get; set; }

        private NamedPipeServerStream pipeSteamServer;

        private NamedPipeClientStream pipeStreamClient;

        private event EventHandler<MessageArgs> hasMessage;

        public event EventHandler<MessageArgs> HasMessage
        {
            add { hasMessage += value; }
            remove { hasMessage -= value; }
        }

        private void OnHasMessage(string message)
        {
            if (hasMessage != null)
            {
                hasMessage(this, new MessageArgs(message));
            }
        }

        public Communication(CommunationType type)
        {
            Type = type;

            ReContruct();
        }

        private void ReContruct()
        {
            if (Type == CommunationType.Server)
            {
                pipeSteamServer = new NamedPipeServerStream(SystemVariable.Instance.PipeNameServerRead(), PipeDirection.In);
            }
            else
            {
                pipeStreamClient = new NamedPipeClientStream(SystemVariable.Instance.PipeNameServerRead());
            }
        }

        public Task ServerStartListen()
        {
            Task t = new Task(async () =>
            {
                while (true)
                {
                    if (pipeSteamServer == null)
                    {
                        pipeSteamServer.WaitForConnection();

                        while (!pipeSteamServer.IsConnected)
                        {
                            await Task.Delay(100);
                        }

                        using (StreamReader sr = new StreamReader(pipeSteamServer, Encoding.UTF8))
                        {
                            string message = await sr.ReadLineAsync();
                            if (!string.IsNullOrEmpty(message))
                            {
                                OnHasMessage(message);
                            }

                            sr.Close();
                        }

                        pipeSteamServer.Close();
                        
                        ReContruct();
                    }
                    else
                    {
                        throw new Exception("Pipe name can't access");
                    }
                }
            });

            return t;
        }
    }
}
