using System;

namespace SystemUltility.InternalEntities
{
    public class MessageArgs : EventArgs
    {
        public string Message { get; set; }

        public MessageArgs(string message)
        {
            this.Message = message;
        }
    }
}
