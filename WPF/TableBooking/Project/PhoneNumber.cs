using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class PhoneNumber
    {
        private string phone;
        private List<uint> ids;
        public string Phone { get => phone; set => phone = value; }
        public List<uint> Ids { get => ids; set => ids = value; }

        public PhoneNumber() {
            Ids = new List<uint>();
        }

        public PhoneNumber(string Phone, uint Id)
        {
            this.Phone = Phone;
            Ids.Add(Id);
        }

        public void AddId(uint Id)
        {
            Ids.Add(Id);
        }
    }
}
