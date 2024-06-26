﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Mockaco.Templating.Request
{
    internal class FormRequestBodyStrategy : IRequestBodyStrategy
    {
        public bool CanHandle(HttpRequest httpRequest)
        {
            return httpRequest.HasFormContentType;
        }

        public Task<JToken> ReadBodyAsJson(HttpRequest httpRequest)
        {
            return Task.FromResult(JToken.FromObject(httpRequest.Form.ToDictionary(f => f.Key, f => f.Value.ToString())));
        }
    }
}
