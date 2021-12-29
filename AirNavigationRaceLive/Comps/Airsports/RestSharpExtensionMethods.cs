using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirNavigationRaceLive.Comps.Airsports
{
    // from 
    // https://stackoverflow.com/questions/31516501/how-to-idiomatically-handle-http-error-codes-when-using-restsharp
    //
    public static class RestSharpExtensionMethods
    {
        public static bool IsSuccessful(this IRestResponse response)
        {
            return response.StatusCode.IsSuccessStatusCode()
                && response.ResponseStatus == ResponseStatus.Completed;
        }

        public static bool IsSuccessStatusCode(this HttpStatusCode responseCode)
        {
            int numericResponse = (int)responseCode;
            return numericResponse >= 200
                && numericResponse <= 399;
        }
    }
}
