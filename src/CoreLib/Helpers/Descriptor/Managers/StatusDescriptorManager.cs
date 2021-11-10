namespace CoreLib.Helpers.Descriptor.Managers
{
    public class StatusDescriptorManager : DescriptorManager<string>
    {
        public readonly StatusDescriptor Approved = new StatusDescriptor("approved");
        public readonly StatusDescriptor Pending = new StatusDescriptor("pending");
        public readonly StatusDescriptor Rejected = new StatusDescriptor("rejected");
        public readonly StatusDescriptor Closed = new StatusDescriptor("closed");

        protected override void AddAll()
        {
            _descriptors.Add(Approved);
            _descriptors.Add(Pending);
            _descriptors.Add(Rejected);
            _descriptors.Add(Closed);
        }
    }
}