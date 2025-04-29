namespace MRent.Domain.Exceptions
{
    public class RentValidationException : Exception
    {
        public RentValidationException()
        {
        }

        public RentValidationException(string message)
            : base(message)
        {
        }

        public RentValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
