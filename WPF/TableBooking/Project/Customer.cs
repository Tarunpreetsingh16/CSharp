using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Customer
    {
        private uint id;
        private string name;
        private string phone;
        private string email;

        public uint Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }

        public Customer() { }
        public Customer(string Name, string Phone, string Email)
        {
            this.Name = Name;
            this.Phone = Phone;
            this.Email = Email;
        }
        public Customer(uint Id,string Name, string Phone, string Email)
        {
            this.Id = Id;
            this.Name = Name;
            this.Phone = Phone;
            this.Email = Email;
        }
    }
}
