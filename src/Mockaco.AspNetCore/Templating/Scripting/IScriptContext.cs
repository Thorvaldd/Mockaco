using Bogus;
using Microsoft.AspNetCore.Http;
using Mockaco.Templating.Models;

namespace Mockaco.Templating.Scripting
{
    public interface IScriptContext
    {
        IGlobalVariableStorage Global { get; }

        Faker Faker { get; }

        ScriptContextRequest Request { get; set; }

        ScriptContextResponse Response { get; set; }

        Task AttachRequest(HttpRequest httpRequest);

        Task AttachRouteParameters(HttpRequest httpRequest, Mock route);

        Task AttachResponse(HttpResponse httpResponse, ResponseTemplate responseTemplate);
    }
}