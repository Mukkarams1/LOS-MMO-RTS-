﻿using System;

namespace TwitterSSO.DataModels.StreamMessages
{
    [Serializable]
    public class Warning
    {
        public string code;
        public string message;
        public int percent_full;
        public int user_id;
    }
}
