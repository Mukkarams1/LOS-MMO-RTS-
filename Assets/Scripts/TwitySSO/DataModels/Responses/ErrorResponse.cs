using System;
using TwitterSSO.DataModels.Errors;

namespace TwitterSSO.DataModels.Responses
{
    [Serializable]
    public class ErrorResponse
    {
        public Error[] errors;
    }
}