using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mockaco.Middlewares;
using Mockaco.Options;
using Mockaco.Templating.Models;
using Mockaco.Templating.Response;
using Mockaco.Templating.Scripting;
using Moq;
using Mock = Moq.Mock;

namespace Mockaco.AspNetCore.Tests.Middlewares
{
    public class ResponseMockingMiddlewareTest
    {
        private readonly ResponseMockingMiddleware _middleware;

        public ResponseMockingMiddlewareTest()
        {
            var next = Moq.Mock.Of<RequestDelegate>();
            _middleware = new ResponseMockingMiddleware(next);
        }

        [Fact]
        public async Task Produces_Response_With_Default_Http_Status_When_Ommited_In_Template()
        {
            var defaultHttpStatusCode = HttpStatusCode.OK;
            var actualHttpStatusCode = default(int);

            var httpResponse = new Mock<HttpResponse>();            
            httpResponse.SetupSet(r => r.StatusCode = It.IsAny<int>()).Callback<int>(value => actualHttpStatusCode = value);
            httpResponse.Setup(r => r.Body).Returns(Moq.Mock.Of<Stream>());
            httpResponse.Setup(r => r.Headers).Returns(Mock.Of<IHeaderDictionary>());
            var httpContext = new Mock<HttpContext>();

             // var httpContext = Moq.Mock.Of<HttpContext>(c => c.Response == httpResponse.Object);
             httpContext.SetupGet(s => s.Response).Returns(httpResponse.Object);
            

            var templateWithOmmitedStatus = Moq.Mock.Of<Template>(t => t.Response == Moq.Mock.Of<ResponseTemplate>());
            var mockacoContext = Moq.Mock.Of<IMockacoContext>(c => c.TransformedTemplate == templateWithOmmitedStatus);

            var scriptContext = Moq.Mock.Of<IScriptContext>();
            var responseBodyFactory = Moq.Mock.Of<IResponseBodyFactory>();

            var optionsWithDefaulHttpStatusCode = Moq.Mock.Of<MockacoOptions>(o => o.DefaultHttpStatusCode == defaultHttpStatusCode);
            var mockacoOptionsSnapshot = Moq.Mock.Of<IOptionsSnapshot<MockacoOptions>>(s => s.Value == optionsWithDefaulHttpStatusCode);

            await _middleware.Invoke(httpContext.Object, mockacoContext, scriptContext, responseBodyFactory, mockacoOptionsSnapshot);

            actualHttpStatusCode.Should()
                .Be((int)defaultHttpStatusCode);
        }
    }
}
