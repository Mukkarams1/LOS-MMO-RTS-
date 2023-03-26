using System;
using TwitterSSO.DataModels.Trends;

namespace TwitterSSO.DataModels.Responses
{
    [Serializable]
    public class TrendsPlaceResponse
    {
        public TrendsPlace[] items;
    }
}
