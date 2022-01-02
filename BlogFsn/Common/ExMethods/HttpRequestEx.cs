using System.Net;
using Microsoft.AspNetCore.Http;

namespace BlogFsn.Common.ExMethods
{
    public static class HttpRequestEx
    {
        public static string GetCurrentUrl(this HttpRequest request)
        {
            string url = request.Scheme + "://" + request.Host + request.Path;
            if (request.QueryString.HasValue)
                url += request.QueryString.Value;

            return url;
        }

        public static string GetCurrentUrlEncoded(this HttpRequest request)
        {
            string url = request.Scheme + "://" + request.Host + request.Path;
            if (request.QueryString.HasValue)
                url += request.QueryString.Value;

            return WebUtility.UrlEncode(url);
        }
    }
}