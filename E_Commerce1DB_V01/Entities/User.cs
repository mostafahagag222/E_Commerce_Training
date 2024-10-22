using E_Commerce1DB_V01.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public Role Role { get; set; } = Role.User;

        //navigational
        public virtual List<Cart> Carts { get; set; } = new List<Cart>();
        public virtual List<Address> Addresses { get; set; } = new List<Address>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public virtual List<Payment> Payments { get; set; } = new List<Payment>();

    }
}
