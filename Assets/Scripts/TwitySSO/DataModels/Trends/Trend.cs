using System;

namespace TwitterSSO.DataModels.Trends
{
    [Serializable]
    public class Trend
    {
        public string name;
		public string url;
		public string promoted_content; // fixme: I don't know a type of this property
		public string query;
		public int tweet_volume;
    }
}
