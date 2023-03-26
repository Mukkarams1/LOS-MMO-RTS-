using System;

namespace TwitterSSO.DataModels.StreamMessages
{
    [Serializable]
    public class LocationDeletionNotice
    {
        public ScrubGeo scrub_geo;
    }
}
