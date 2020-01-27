using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Project
{
    class PhoneNumberList
    {
        private BindingList<PhoneNumber> phoneNumbers = null;

        public BindingList<PhoneNumber> PhoneNumbers { get => phoneNumbers; set => phoneNumbers = value; }

        public PhoneNumberList()
        {
            PhoneNumbers = new BindingList<PhoneNumber>();
        }

        public void Add(PhoneNumber phone)
        {
            PhoneNumbers.Add(phone);
        }

        public int Count
        {
            get { return PhoneNumbers.Count; }
        }

        public PhoneNumber this[int i]
        {
            get { return PhoneNumbers[i]; }
            set { PhoneNumbers[i] = value; }
        }
        public void Clear()
        {
            PhoneNumbers.Clear();
        }

        public bool Contains(Customer customer)
        {
            for (int i=0;i<phoneNumbers.Count;i++)
            {
                if (phoneNumbers[i].Phone == customer.Phone)
                {
                    phoneNumbers[i].AddId(customer.Id);
                    return true;
                }
            }
            return false;
        }
    }
}
