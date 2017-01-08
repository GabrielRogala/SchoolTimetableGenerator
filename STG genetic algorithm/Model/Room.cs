using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class Room
    {
        public int id;
        public String name;
        public int amount;
        public String type;
        public TimeTable timeTable;

        public Room(int id, String name, int amount, String type) {
            this.id = id;
            this.name = name;
            this.amount = amount;
            this.type = type;
        }

        public void setTimeTable(TimeTable timeTable)
        {
            this.timeTable = timeTable;
        }

        public TimeTable getTimeTable()
        {
            return timeTable;
        }

        public string ToString()
        {
            return id + ":" + name+":"+amount+":"+type;
        }

        public override bool Equals(object obj)
        {
            return this.id.Equals(((Room)obj).id);
        }
    }
}
