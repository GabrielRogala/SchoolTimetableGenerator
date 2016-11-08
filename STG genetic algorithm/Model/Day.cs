using STG_genetic_algorithm.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class Day
    {
        private String name;
        private List<Slot> slots;

        public Day(String name)
        {
            this.name = name;
            slots = new List<Slot>();

            for (int i = 0; i < ConstVariable.NUMBER_OF_SLOTS_IN_DAY ; i++)
            {
                slots.Add(new Slot(i));
            }
        }

        public List<Slot> getSlots()
        {
            return slots;
        }

        public String getName() {
            return name;
        }

    }
}
