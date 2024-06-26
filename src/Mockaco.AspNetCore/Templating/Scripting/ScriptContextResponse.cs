﻿using Mockaco.Common;
using Newtonsoft.Json.Linq;

namespace Mockaco.Templating.Scripting
{
    public class ScriptContextResponse
    {
        public IReadOnlyDictionary<string, string> Header { get; }

        public JToken Body { get; }

        public ScriptContextResponse(StringDictionary header, JToken body)
        {
            Header = header;
            Body = body;
        }
    }
}