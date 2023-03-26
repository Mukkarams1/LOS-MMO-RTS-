using System;
using TwitterSSO.DataModels.Core;

namespace TwitterSSO.DataModels.Responses
{
    [Serializable]
    public class StatusesHomeTimelineResponse
    {
        public Tweet[] items;
    }
}
