using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Mockaco.Templating.Request
{
    public interface IRequestBodyFactory
    {
        Task<JToken> ReadBodyAsJson(HttpRequest httpRequest);
    }
}
