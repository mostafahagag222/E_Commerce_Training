using E_Commerce1DB_V01.Repositories;

namespace E_Commerce1DB_V01
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int UserId { get; set;}

        //navigational
        public virtual User User { get; set; }
    }
}