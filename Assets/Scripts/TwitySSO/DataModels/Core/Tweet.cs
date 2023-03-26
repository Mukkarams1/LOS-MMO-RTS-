using System;

namespace TwitterSSO.DataModels.Core
{
    [Serializable]
    public class Tweet : TweetObjectWithUser
    {
        public TweetObjectWithUser retweeted_status;
    }
}
