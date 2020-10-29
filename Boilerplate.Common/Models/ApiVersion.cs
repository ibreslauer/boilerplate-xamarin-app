using System.Collections.Generic;

namespace Boilerplate.Common.Models
{
    public class ApiVersion
    {
        public string AssemblyName { get; set; }
        public string Version { get; set; }
        public List<string> References { get; set; }
    }
}
