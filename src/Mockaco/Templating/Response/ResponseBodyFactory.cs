﻿using System.Collections.Generic;
using System.Linq;

namespace Mockaco
{
    public class ResponseBodyStrategyFactory : IResponseBodyStrategyFactory
    {
        private readonly IEnumerable<IResponseBodyStrategy> _strategies;

        public ResponseBodyStrategyFactory(IEnumerable<IResponseBodyStrategy> strategies)
        {
            _strategies = strategies;
        }

        public string GetResponseBody(ResponseTemplate responseTemplate)
        {
            var selectedStrategy = _strategies.FirstOrDefault(_ => _.CanHandle(responseTemplate));

            return selectedStrategy.GetResponse(responseTemplate);
        }
    }
}
