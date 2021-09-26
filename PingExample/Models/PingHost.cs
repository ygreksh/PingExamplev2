using System;
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

        protected bool Equals(PingHost other)
        {
            return Host == other.Host && Period == other.Period && PingProtocol == other.PingProtocol && Port == other.Port && StatusCode == other.StatusCode;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PingHost)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Host, Period, (int)PingProtocol, Port, (int)StatusCode);
        }
    }
}