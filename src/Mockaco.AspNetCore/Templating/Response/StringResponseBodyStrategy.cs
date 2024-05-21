using System.Text;
using Mockaco.Templating.Models;

namespace Mockaco.Templating.Response
{
    internal abstract class StringResponseBodyStrategy : IResponseBodyStrategy
    {
        public abstract bool CanHandle(ResponseTemplate responseTemplate);

        public Task<byte[]> GetResponseBodyBytesFromTemplate(ResponseTemplate responseTemplate)
        {
            var responseBodyStringFromTemplate = GetResponseBodyStringFromTemplate(responseTemplate);

            return Task.FromResult(responseBodyStringFromTemplate == default ? default : Encoding.UTF8.GetBytes(responseBodyStringFromTemplate));
        }

        public abstract string GetResponseBodyStringFromTemplate(ResponseTemplate responseTemplate);
    }
}
