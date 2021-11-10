using System.Collections.Generic;
using System.Linq;

namespace CoreLib.Helpers.Descriptor.Managers
{
    public abstract class DescriptorManager<T>
    {
        protected List<CustomDescriptor<T>> _descriptors = new List<CustomDescriptor<T>>();

        protected DescriptorManager()
        {
            AddAll();
        }

        public bool Any(T key)
        {
            return _descriptors.Any(x => x.Key.Equals(key));
        }

        public CustomDescriptor<T> Find(T key)
        {
            return _descriptors.FirstOrDefault(x => x.Key.Equals(key));
        }

        protected abstract void AddAll();

    }
}