using System;
using System.Collections;

namespace TwitterSSO.DataModels.Trends {

	[Serializable]
	public class TrendsPlace
	{
		public Trend[] trends;
		public string as_of;
		public string created_at;
		public Woeid_location locaions;
	}
}
