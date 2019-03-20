﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;

namespace Mockore
{
    public class ResponseTemplate
    {
        public IDictionary<string, string> Headers { get; set; }
        public string Delay { get; set; }        
        public HttpStatusCode Status { get; set; }
        public JContainer Body { get; set; }
    }
}