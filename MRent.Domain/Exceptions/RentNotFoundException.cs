namespace MRent.Domain.Exceptions
{
    public class RentNotFoundException : Exception
    {
        public RentNotFoundException()
        {
        }

        public RentNotFoundException(string message)
            : base(message)
        {
        }

        public RentNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
