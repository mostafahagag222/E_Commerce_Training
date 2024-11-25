namespace E_Commerce1DB_V01.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public Role Role { get; set; } = Role.User;
        public bool IsVerified { get; set; }

        //navigational
        public virtual List<Basket> Baskets { get; set; } = new List<Basket>();
        public virtual List<Address> Addresses { get; set; } = new List<Address>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public virtual List<Payment> Payments { get; set; } = new List<Payment>();
        public virtual List<UserToken> Tokens { get; set; } = new List<UserToken>();
    }
}