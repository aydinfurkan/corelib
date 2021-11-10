using CoreLib.Exceptions;

namespace CoreLib.Helpers.CommonErrors
{
    public class InvalidPaginationException : BadRequest
    {
        public InvalidPaginationException() : base(4003, message: "Invalid pagination query")
        {

        }
    }

    public class InvalidSortByException : BadRequest
    {
        public InvalidSortByException() : base(4004, message: "Invalid sortBy query")
        {

        }
    }

    public class EmptyFieldException : BadRequest
    {
        public EmptyFieldException(string fieldName) : base(4005, message: $"The field '{fieldName}' can not be null or empty.")
        {

        }
    }

    public class NegativeValueException : BadRequest
    {
        public NegativeValueException(string fieldName) : base(4006, message: $"The field '{fieldName}' can not be negative.")
        {

        }
    }

    public class NonPositiveValueException : BadRequest
    {
        public NonPositiveValueException(string fieldName) : base(4007, message: $"The field '{fieldName}' must be greater than 0.")
        {

        }
    }
}