namespace E_Commerce2Business_V01
{
    [Serializable]
    public class ConflictException : Exception
    {
        public ConflictException()
        {
        }

        public ConflictException(string message) : base(message)
        {
        }

        public ConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}