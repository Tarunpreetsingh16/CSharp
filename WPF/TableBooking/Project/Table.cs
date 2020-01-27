using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Table
    {
        private string tableNumber;
        private uint seats;
        private string tableInfo;
        private TimingsList timings;

        public string TableNumber { get => tableNumber; set => tableNumber = value; }
        public uint Seats { get => seats; set => seats = value; }
        public string TableInfo { get => tableInfo ; set => tableInfo = value; }
        public TimingsList Timings { get => timings; set => timings = value; }
        
        public Table() {
            timings = new TimingsList();
        }
        public Table(string TableNumber, uint Seats)
        {
            this.TableNumber = TableNumber;
            this.Seats = Seats;
        }

    }
}
