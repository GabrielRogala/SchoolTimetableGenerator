using STG_genetic_algorithm.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class TimeTable
    {
        public Group group;
        public Teacher teacher;
        private List<Day> days;

        public TimeTable()
        {
            days = new List<Day>();
            for (int i = 0; i < ConstVariable.NUMBER_OF_DAYS; i++)
            {
                days.Add(new Day("DAY:" + (i + 1)));
            }
        }

        public TimeTable(Group gr) : this()
        {
            gr.setTimeTable(this);
            this.group = gr;
        }

        public TimeTable(Teacher te) : this()
        {
            te.setTimeTable(this);
            this.teacher = te;
        }

        public List<Day> getDays()
        {
            return days;
        }

        public bool isEmpty(int day, int hour)
        {
            return (days[day].getSlots()[hour].getLessons().Count == 0);
        }

        public void addLesson(Lesson lesson, int day, int hour)
        {
            days[day].getSlots()[hour].addLesson(lesson);
        }

        public void removeLesson(Lesson lesson, int day, int hour)
        {
            days[day].getSlots()[hour].removeLesson(lesson);
        }

        public void printTimeTable()
        {
            if (teacher != null)
            {
                Console.WriteLine("============== " + teacher.ToString() + " ===============");
            }
            else if (group != null)
            {
                Console.WriteLine("============== " + group.ToString() + " ===============");
            }
            else
            {
                Console.WriteLine("======================================================");
            }
            Console.Write("H |      ");
            foreach (Day d in days)
            {
                Console.Write(d.getName() + "               \t:      ");
            }
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------");
            for (int i = 0; i < ConstVariable.NUMBER_OF_SLOTS_IN_DAY; i++)
            {
                Console.Write(i + " |");
                foreach (Day d in days)
                {
                    if (d.getSlots()[i].getLessons().Count > 0)
                    {
                        foreach (Lesson lesson in d.getSlots()[i].getLessons()) {
                            Console.Write(" " + lesson.ToString() + "\t");
                        }
                    }
                    else
                    {
                        Console.Write(" -----/-----/-------------\t");
                    }
                    Console.Write("|");

                }
                Console.WriteLine("");
            }
            Console.WriteLine("---------------------------------------------------");
        }

        public void printSimpleTimeTable()
        {
            if (teacher != null)
            {
                Console.WriteLine("============== " + teacher.ToString() + " ===============");
            }
            else if (group != null)
            {
                Console.WriteLine("============== " + group.ToString() + " ===============");
            }
            else
            {
                Console.WriteLine("======================================================");
            }
            Console.WriteLine("---------------------------------------------------");
            for (int i = 0; i < ConstVariable.NUMBER_OF_SLOTS_IN_DAY; i++)
            {
                foreach (Day d in days)
                {
                    if (d.getSlots()[i].getLessons().Count > 0)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.Write("|");

                }
                Console.WriteLine("");
            }
            Console.WriteLine("---------------------------------------------------");
        }

        public List<TimeSlot> getFreeTimeSlot()
        {
            return getFreeTimeSlot(1);
        }

        public List<TimeSlot> getFreeTimeSlot(int size)
        {
            List<TimeSlot> freeSlot = new List<TimeSlot>();

            for (int d = 0; d < ConstVariable.NUMBER_OF_DAYS; ++d)
            {
                for (int h = 0; h < ConstVariable.NUMBER_OF_SLOTS_IN_DAY-size; ++h)
                {
                    bool result = true;

                    for (int i=0;i<size;++i) {
                        result = result && isEmpty(d, h + i);
                    }

                    if (result)
                    {
                        freeSlot.Add(new TimeSlot(d, h));
                    }
                }
            }

            return freeSlot;
        }

        public List<TimeSlot> getLessonsTimeSlot()
        {
            List<TimeSlot> freeSlot = new List<TimeSlot>();

            for (int d = 0; d < ConstVariable.NUMBER_OF_DAYS; ++d)
            {
                for (int h = 0; h < ConstVariable.NUMBER_OF_SLOTS_IN_DAY; ++h)
                {
                    if (!isEmpty(d, h))
                    {
                        freeSlot.Add(new TimeSlot(d, h));
                    }
                }
            }

            return freeSlot;
        }

        public List<Lesson> getLessons()
        {
            List<Lesson> lessons = new List<Lesson>();

            for (int d = 0; d < ConstVariable.NUMBER_OF_DAYS; ++d)
            {
                for (int h = 0; h < ConstVariable.NUMBER_OF_SLOTS_IN_DAY; ++h)
                {
                    if (!isEmpty(d, h))
                    {
                        lessons.AddRange(getLesson(d,h));
                    }
                }
            }

            return lessons;
        }

        public List<Lesson> getLesson(int d, int h) {
            return days[d].getSlots()[h].getLessons();
        }

        public int fitness()
        {
            int value = 0;
            value += fitnessFreeSlots();

            if (group != null) {
                value += fitnessType();
            }

            return value;
        }

        public int fitnessFreeSlots() {
            int value = 0;
            foreach (Day d in days)
            {
                int lastIndex = -1;
                for (int i = 0; i < d.getSlots().Count; i++)
                {
                    if (!d.getSlots()[i].isEmpty())
                    {
                        value += (int)Math.Pow((i - lastIndex - 1), 3);
                        lastIndex = i;
                    }

                }
            }

            return value;
        }

        public int fitnessType()
        {
            int sizeOfSubjectType = Enum.GetNames(typeof(SubjectType)).Length;
            int value = 0;
            if (this.group != null)
            {
                foreach (Day d in days)
                {
                    int[] numberOfType = new int[sizeOfSubjectType];
                    for (int i = 0; i < sizeOfSubjectType; i++) {
                        numberOfType[i] = 0;
                    }
                    int count = 0;

                    for (int i = 0; i < d.getSlots().Count; i++)
                    {
                        if (!d.getSlots()[i].isEmpty())
                        {
                            count++;
                            
                            numberOfType[(int)d.getSlots()[i].getLesson(0).getSubject().getSubjectType()]++;
                        }

                    }

                    count = count / sizeOfSubjectType;
                    for (int i = 0; i < sizeOfSubjectType; i++)
                    {
                        numberOfType[i] = numberOfType[i] - count;
                        if (numberOfType[i] > 0) {
                            value += (int)(Math.Pow(numberOfType[i], 2));
                        }
                    }
                }
            }
            return value;
        }

        public Boolean checkSume(List<Lesson> lessons)
        {
            Boolean result = true;
            List<Lesson> listOfLessonInThisTimeTable = new List<Lesson>();

            foreach (Day d in days) {
                foreach (Slot s in d.getSlots()) {
                    if (!s.isEmpty()) {
                        listOfLessonInThisTimeTable.Add(s.getLesson(0));
                    }
                }
            }

            foreach (Lesson lesson in lessons) {
                if (!listOfLessonInThisTimeTable.Contains(lesson)) {
                    result = false;
                    Console.WriteLine("ERROR! "+lesson.ToString() +" not exist in TimeTable ("+this.group.ToString() + this.teacher.ToString() +")");
                }
            }

            return result;
        }
    }
}
