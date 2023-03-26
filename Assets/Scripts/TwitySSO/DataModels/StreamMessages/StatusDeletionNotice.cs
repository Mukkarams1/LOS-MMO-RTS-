using System;

namespace TwitterSSO.DataModels.StreamMessages
{
    [Serializable]
    public class StatusDeletionNotice
    {
        public StatusDelete delete;
    }
}
