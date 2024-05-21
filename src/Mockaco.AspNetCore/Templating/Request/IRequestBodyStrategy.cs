using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Mockaco.Templating.Request
{
    internal interface IRequestBodyStrategy
    {
        bool CanHandle(HttpRequest httpRequest);
        Task<JToken> ReadBodyAsJson(HttpRequest httpRequest);
    }
}