﻿using Microsoft.AspNetCore.Http;
using Mockaco.Templating.Models;

namespace Mockaco.Templating.Request
{
    internal class RequestMethodMatcher : IRequestMatcher
    {
        public Task<bool> IsMatch(HttpRequest httpRequest, Mock mock)
        {
            if (string.IsNullOrWhiteSpace(mock?.Method))
            {
                return Task.FromResult(httpRequest.Method == HttpMethods.Get);
            }

            var isMatch = httpRequest.Method.Equals(mock.Method, StringComparison.InvariantCultureIgnoreCase);

            return Task.FromResult(isMatch);
        }
    }
}
