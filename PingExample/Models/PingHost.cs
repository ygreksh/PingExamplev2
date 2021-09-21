using System.Net;
using Newtonsoft.Json;

namespace PingExample
{
    public class PingHost
    {
        [JsonProperty("host")]
        public string Host { get; set; }
        [JsonProperty("period")]
        public int Period { get; set; }
        [JsonProperty("protocol")]
        public PingProtocol PingProtocol { get; set; }
        [JsonProperty("port")]
        public int Port { get; set;}
        [JsonProperty("httpstatus")]
        public HttpStatusCode StatusCode { get; set; }
    }
}