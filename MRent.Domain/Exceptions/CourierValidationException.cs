namespace MRent.Domain.Exceptions
{
    public class CourierValidationException : Exception
    {
        public CourierValidationException()
        {
        }

        public CourierValidationException(string message)
            : base(message)
        {
        }

        public CourierValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
