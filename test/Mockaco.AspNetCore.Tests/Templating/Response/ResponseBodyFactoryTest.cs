using FluentAssertions;
using Mockaco.Templating.Models;
using Mockaco.Templating.Response;
using Moq;

namespace Mockaco.AspNetCore.Tests.Templating.Response
{
    public class ResponseBodyFactoryTest
    {
        [Fact]
        public async Task Can_Handle_Null_Returned_From_Response_Body_Strategy()
        {
            var responseBodyStrategy = Moq.Mock.Of<IResponseBodyStrategy>(
                s => s.CanHandle(It.IsAny<ResponseTemplate>()) && s.GetResponseBodyBytesFromTemplate(It.IsAny<ResponseTemplate>()) == Task.FromResult<byte[]>(null));

            var factory = new ResponseBodyFactory(new[] { responseBodyStrategy });

            var responseTemplate = new ResponseTemplate { Body = null };

            var bodyBytes = await factory.GetResponseBodyBytesFromTemplate(responseTemplate);

            bodyBytes.Should()
                .BeNull();
        }
    }
}