using System;

namespace TwitterSSO.DataModels.StreamMessages
{
    [Serializable]
    public class Disconnect
    {
        public int code;
        public string stream_name;
        public string reason;
    }
}
