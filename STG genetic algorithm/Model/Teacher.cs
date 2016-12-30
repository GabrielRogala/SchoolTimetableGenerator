using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class Teacher
    {
        public String name;
        public int id;
        public TimeTable timeTable;

        public Teacher(int id, String name)
        {
            this.id = id;
            this.name = name;
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
            return id + ":" + name;
        }

        public override bool Equals(object obj)
        {
            return this.id.Equals(((Teacher)obj).id);
        }
    }
}
