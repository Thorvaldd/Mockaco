using Mockaco.Templating.Models;

namespace Mockaco.Templating.Response
{
    internal interface IResponseBodyFactory
    {
        Task<byte[]> GetResponseBodyBytesFromTemplate(ResponseTemplate responseTemplate);
    }
}
