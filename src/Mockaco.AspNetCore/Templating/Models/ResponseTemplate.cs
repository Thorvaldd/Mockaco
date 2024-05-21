using System.Net;
using Mockaco.Common;
using Newtonsoft.Json.Linq;

namespace Mockaco.Templating.Models
{
    public class ResponseTemplate
    {
        public int? Delay { get; set; }

        public bool? Indented { get; set; }

        public HttpStatusCode Status { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public JToken Body { get; set; }

        public string File { get; set; }

        public ResponseTemplate()
        {
            Headers = new StringDictionary();
        }
    }
}