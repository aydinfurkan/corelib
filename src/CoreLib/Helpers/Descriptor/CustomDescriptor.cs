namespace CoreLib.Helpers.Descriptor
{
    public abstract class CustomDescriptor<T>
    {
        public T Key { get; }

        protected CustomDescriptor(T key)
        {
            Key = key;
        }

        public override bool Equals(object? obj)
        {
            var item = obj as CustomDescriptor<T>;

            if (item == null)
                return false;
            
            return this.Key.Equals(item.Key);
        }

        public override int GetHashCode()
        {
            return (Key != null ? Key.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}