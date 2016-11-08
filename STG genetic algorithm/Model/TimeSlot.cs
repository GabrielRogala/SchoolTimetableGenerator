using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class TimeSlot
    {
        public int day;
        public int hour;

        public TimeSlot(int day, int hour)
        {
            this.day = day;
            this.hour = hour;
        }

        public override bool Equals(object obj)
        {
            return (this.day == ((TimeSlot)obj).day) && (this.hour == ((TimeSlot)obj).hour);
        }

        public override String ToString()
        {
            return "(" + day + "/" + hour + ")";
        }
    }
}
