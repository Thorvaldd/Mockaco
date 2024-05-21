using Microsoft.AspNetCore.Http;
using Mockaco.Templating.Models;

namespace Mockaco.Templating.Request
{
    internal interface IRequestMatcher
    {
        Task<bool> IsMatch(HttpRequest httpRequest, Mock mock);
    }
}