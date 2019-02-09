using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DM.ResponseModels
{
    public class CacheableHttpActionResult<T>: IHttpActionResult
    {
        private readonly T _content;
        private readonly int _cacheTimeInMin;
        private readonly HttpRequestMessage _request;

        public CacheableHttpActionResult(HttpRequestMessage request, T content, int cachetime)
        {
            this._content = content;
            this._request = request;
            this._cacheTimeInMin = cachetime;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse<T>(HttpStatusCode.OK, this._content);
            response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(this._cacheTimeInMin),
                // other customizations
            };

            return Task.FromResult(response);
        }
    }
}
