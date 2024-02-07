using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace EAU.ServiceLimits.Server.Manager
{
    public class ConfigurationRoot
    {
        [YamlMember(Alias = "domain")]
        public string Domain { get; set; }

        [YamlMember(Alias = "descriptors")]
        public List<Descriptor> Descriptors { get; set; }
    }

    public class Descriptor
    {
        [YamlMember(Alias = "key")]
        public string Key { get; set; }

        [YamlMember(Alias = "value")]
        public string Value { get; set; }

        [YamlMember(Alias = "rate_limit")]
        public RateLimit RateLimit { get; set; }

        [YamlMember(Alias = "descriptors")]
        public List<Descriptor> Descriptors { get; set; }
    }

    public class RateLimit
    {
        [YamlMember(Alias = "unit")]
        public string Unit { get; set; }

        [YamlMember(Alias = "requests_per_unit")]
        public int RequestPerUnit { get; set; }
    }
}
