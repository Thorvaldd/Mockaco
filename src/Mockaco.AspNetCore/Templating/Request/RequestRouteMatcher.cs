﻿using Microsoft.AspNetCore.Http;
using Mockaco.Common;
using Mockaco.Templating.Models;

namespace Mockaco.Templating.Request
{
    internal class RequestRouteMatcher : IRequestMatcher
    {
        private const string DefaultRoute = "/";

        public Task<bool> IsMatch(HttpRequest httpRequest, Mock mock)
        {
            var routeMatcher = new RouteMatcher();

            if (string.IsNullOrWhiteSpace(mock?.Route))
            {
                return Task.FromResult(routeMatcher.IsMatch(DefaultRoute, httpRequest.Path));
            }

            return Task.FromResult(routeMatcher.IsMatch(mock.Route, httpRequest.Path));
        }
    }
}
