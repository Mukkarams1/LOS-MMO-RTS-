using System;

namespace TwitterSSO.DataModels.StreamMessages
{
    [Serializable]
    public class WithheldContentNotice
    {
        public StatusWithheld status_withheld;
        public UserWithheld user_withheld;
    }
}
