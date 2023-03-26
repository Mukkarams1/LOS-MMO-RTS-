using System;

namespace TwitterSSO.DataModels.Errors
{
    [Serializable]
    public class Error
    {
        public int code;
        public string message;
    }
}