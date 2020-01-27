using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class CustomersList
    {
        private List<Customer> customerList = null;

        public CustomersList()
        {
            customerList = new List<Customer>();
        }
        public List<Customer> GetList()
        {
            return customerList;
        }

        public void Add(Customer customer)
        {
            customerList.Add(customer);
        }

        public int Count
        {
            get { return customerList.Count; }
        }

        public Customer this[int i]
        {
            get { return customerList[i]; }
            set { customerList[i] = value; }
        }
        public void Clear()
        {
            customerList.Clear();
        }
        public void RemoveAt(int index)
        {
            customerList.RemoveAt(index);
        }
    }
}
