namespace MRent.Domain.Exceptions
{
    public class MotorcycleNotFoundException : Exception
    {
        public MotorcycleNotFoundException()
        {
        }

        public MotorcycleNotFoundException(string message)
            : base(message)
        {
        }

        public MotorcycleNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
