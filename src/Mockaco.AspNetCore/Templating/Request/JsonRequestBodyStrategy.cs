using Microsoft.AspNetCore.Http;
using Mockaco.Extensions;
using Newtonsoft.Json.Linq;

namespace Mockaco.Templating.Request
{
    internal class JsonRequestBodyStrategy: IRequestBodyStrategy
    {
        public bool CanHandle(HttpRequest httpRequest)
        {
            return httpRequest.HasJsonContentType();
        }

        public async Task<JToken> ReadBodyAsJson(HttpRequest httpRequest)
        {
            var body = await httpRequest.ReadBodyStream();

            if (string.IsNullOrWhiteSpace(body))
            {
                return new JObject();
            }

            return JToken.Parse(body);
        }
    }
}
