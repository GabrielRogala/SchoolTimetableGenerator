using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class FreeSlotsToLesson
    {
        public List<TimeSlot> slots;
        public int size;
        public Lesson lesson;

        public FreeSlotsToLesson(List<TimeSlot> slots, Lesson lesson)
        {
            this.slots = slots;
            this.size = slots.Count;
            this.lesson = lesson;
        }

        public override string ToString()
        {
            return lesson.ToString() + " : "+size;
        }
    }
}
