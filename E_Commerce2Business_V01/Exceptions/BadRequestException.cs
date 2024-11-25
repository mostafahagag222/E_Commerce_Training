namespace E_Commerce2Business_V01.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public int Id { get; set; }
        public string StringId { get; set; }
        public BadRequestException()
        {
        }
        public BadRequestException(string message) : base(message)
        {
        }
        public BadRequestException(string message , int id) : base(message)
        {
            Id = id;
        }
        public BadRequestException(string message , string id) : base(message)
        {
            StringId = id;
        }
        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public override string ToString()
        {
            var r = $"{base.ToString()} \n ID: {Id.ToString()} \n StringID: {StringId}";
            return r ;
        }
    }
}