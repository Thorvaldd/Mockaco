using Newtonsoft.Json.Linq;

namespace Mockaco.Templating.Models
{
    public class CallbackTemplate
    {
        public string Method { get; set; }

        public string Url { get; set; }

        public int? Delay { get; set; }

        public int? Timeout { get; set; }

        public bool? Indented { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public JToken Body { get; set; }
    }
}
