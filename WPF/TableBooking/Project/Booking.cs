using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Booking
    {
        private uint id;
        private string tableNumber;
        private uint customerId;
        private Timing timing;
        private string bookingForList;

        public uint Id { get => id; set => id = value; }
        public string TableNumber { get => tableNumber; set => tableNumber= value; }
        public uint CustomerId{ get => customerId; set => customerId = value; }
        public Timing Timing { get => timing; set => timing = value; }
        public string BookingForList { get => bookingForList; set => bookingForList= value; }

        public Booking() { }
        public Booking(string TableNumber, uint CustomerId, Timing Timing)
        {
            this.TableNumber = TableNumber;
            this.CustomerId = CustomerId;
            this.Timing = Timing;
            BookingForList = "Table - " + this.TableNumber + " | Time - " + this.Timing.Duration;
        }
        public Booking(uint  Id, string TableNumber, uint CustomerId, Timing Timing)
        {
            this.Id = Id;
            this.TableNumber = TableNumber;
            this.CustomerId = CustomerId;
            this.Timing = Timing;
            BookingForList = "Table - " + this.TableNumber + " | Time - " + this.Timing.Duration;
        }
    }
    public class Timing
    {

        private uint startTime;
        private uint endTime;
        private string duration;

        public uint StartTime { get => startTime; set => startTime = value; }
        public uint EndTime { get => endTime; set => endTime = value; }
        public string Duration { get => duration; set => duration = value; }

        public Timing() { }
        public Timing(uint StartTime, uint EndTime)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            Duration = this.StartTime.ToString() + " pm - " + this.EndTime.ToString() + " pm";
        }
    }
}
