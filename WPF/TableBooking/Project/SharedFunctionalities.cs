using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class SharedFunctionalities
    {

        public static List<uint> accountNum = new List<uint>();
        public static List<uint> bookingIds = new List<uint>();


        static Random rand = new Random();
        public static uint GenerateAccountNum()
        {
            uint accountNumber;
            do
            {
                accountNumber = (uint)rand.Next(10000, 100000);

            } while (accountNum.Contains(accountNumber));

            accountNum.Add(accountNumber);

            return accountNumber;
        }
        public static uint GenerateBookingId()
        {
            uint bookingId;
            do
            {
                bookingId = (uint)rand.Next(10000, 100000);

            } while (bookingIds.Contains(bookingId));

            bookingIds.Add(bookingId);

            return bookingId;
        }

    }
}
