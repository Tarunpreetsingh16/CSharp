using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class BookingsList
    {
        private List<Booking> bookingsList = null;


        public BookingsList()
        {
            bookingsList = new List<Booking>();
        }
        public List<Booking> GetList()
        {
            return bookingsList;
        }

        public void Add(Booking booking)
        {
            bookingsList.Add(booking);
        }

        public int Count
        {
            get { return bookingsList.Count; }
        }

        public Booking this[int i]
        {
            get { return bookingsList[i]; }
            set { bookingsList[i] = value; }
        }
        public void Clear()
        {
            bookingsList.Clear();
        }
        public  void RemoveAt(int index)
        {
            bookingsList.RemoveAt(index);
        }
    }
}
