using CoreLib.Exceptions;

namespace CoreLib.Helpers.CommonErrors
{
    public class DocumentNotFound<T> : NotFound
    {
        public DocumentNotFound(string id) : base(4004, message: $"{nameof(T)} with id {id} was not found.")
        {
        }
    }
}