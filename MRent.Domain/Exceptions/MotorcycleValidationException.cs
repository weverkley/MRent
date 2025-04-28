namespace MRent.Domain.Exceptions
{
    public class MotorcycleValidationException : Exception
    {
        public MotorcycleValidationException()
        {
        }

        public MotorcycleValidationException(string message)
            : base(message)
        {
        }

        public MotorcycleValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
