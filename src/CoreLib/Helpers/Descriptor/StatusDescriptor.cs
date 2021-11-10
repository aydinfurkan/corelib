using System.Collections.Generic;
using System.Linq;

namespace CoreLib.Helpers.Descriptor
{
    public class StatusDescriptor : CustomDescriptor<string>
    {
        public StatusDescriptor(string key) : base(key)
        {
        }
    }
}