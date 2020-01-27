using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class TimingsList
    {
        private List<Timing> timings = null;

        public List<Timing> Timings { get => timings; set => timings = value; }

        public TimingsList()
        {
            timings = new List<Timing>();
        }

        public void Add(Timing timing)
        {
            timings.Add(timing);
        }

        public int Count
        {
            get { return timings.Count; }
        }

        public Timing this[int i]
        {
            get { return timings[i]; }
            set { timings[i] = value; }
        }
        public void Clear()
        {
            timings.Clear();
        }

        public bool Contains(Timing timing)
        {
            for(int i = 0; i < timings.Count; i++)
            {
                if(timings[i].Duration == timing.Duration)
                {
                    return true;
                }
            }
            return false;
        }
        public void RemoveAt(int i)
        {
            timings.RemoveAt(i);
        }
    }
}
