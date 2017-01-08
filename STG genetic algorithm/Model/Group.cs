using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class Group
    {
        public String name;
        public int id;
        public TimeTable timeTable;
        public int amount;

        public Group(int id, String name)
        {
            this.id = id;
            this.name = name;
        }

        public Group(int id, String name, int amount)
        {
            this.id = id;
            this.name = name;
            this.amount = amount;
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
            return id + ":" + name+"("+amount+")";
        }

        public override bool Equals(object obj)
        {
            return this.id.Equals(((Group)obj).id);
        }
    }
}
