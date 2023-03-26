using System;

namespace TwitterSSO.DataModels.Oauth
{
	[Serializable]
	public class BearerToken
	{
		public string token_type;
		public string access_token;
	}
}
