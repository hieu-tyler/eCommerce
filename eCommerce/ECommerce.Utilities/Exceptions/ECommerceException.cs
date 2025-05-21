namespace ECommerce.Utilities.Exceptions
{
    public class ECommerceException : Exception
    {
        public ECommerceException()
        {
        }

        public ECommerceException(string message)
            : base(message)
        {
        }

        public ECommerceException(string message, Exception inner)
            : base(message, inner)
        {   
        }
    }
}
