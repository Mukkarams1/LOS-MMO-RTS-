using System;

namespace TwitterSSO.DataModels.StreamMessages
{
    [Serializable]
    public class StatusDelete
    {
        public DeletedStatus status;
    }
}
