using Mockaco.Templating.Models;

namespace Mockaco.Templating.Response
{
    internal interface IResponseBodyStrategy
    {
        bool CanHandle(ResponseTemplate responseTemplate);

        Task<byte[]> GetResponseBodyBytesFromTemplate(ResponseTemplate responseTemplate);
    }
}