using System;

namespace TwitterSSO.DataModels.Core
{
    [Serializable]
    public class TweetObjectWithUser : TweetObject
    {
        public TweetUser user;
    }
}
