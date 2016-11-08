using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STG_genetic_algorithm.Model;
using STG_genetic_algorithm.Config;
using System.Collections.Generic;

namespace STG_genetic_algorithm_Tests.Model
{
    /// <summary>
    /// Summary description for TimeTableTests
    /// </summary>
    [TestClass]
    public class TimeTableTests
    {
        TimeTable timeTable;

        [TestMethod]
        public void TestFitnessFreeSlotsMethod()
        {
            timeTable = dataProvide();

            List<TimeSlot> slot = new List<TimeSlot>();
            slot.Add(new TimeSlot(1,1));
            

        }

        private TimeTable dataProvide()
        {
            TimeTable tmp = new TimeTable();

            Slot slot = new Slot(0);
            Teacher t = new Teacher(0, "t");
            Group g = new Group(0, "g");
            Subject s1 = new Subject(1, "s1", SubjectType.HUM);
            Subject s2 = new Subject(1, "s2", SubjectType.SCI);
            Subject s3 = new Subject(1, "s3", SubjectType.SPE);

            Lesson lesson1 = new Lesson(t, g, s1);
            Lesson lesson2 = new Lesson(t, g, s2);
            Lesson lesson3 = new Lesson(t, g, s3);

            foreach (Day d in tmp.getDays()) {
                for (int i = 0; i<ConstVariable.NUMBER_OF_SLOTS_IN_DAY;i++) {
                    switch (i%3)
                    {
                        case 0: d.getSlots()[i].addLesson(lesson1);
                            break;
                        case 1: d.getSlots()[i].addLesson(lesson2);
                            break;
                        case 2: d.getSlots()[i].addLesson(lesson3);
                            break;
                        default:
                            break;
                    }
                }
            }

            return tmp;
        }

        [TestMethod]
        public void shouldReturnOnlyTheSameSlots() {
            Random rand = new Random();
            Group g = new Group(1, "g1");
            Teacher t = new Teacher(1, "t1");
            TimeTable gtt = new TimeTable(g);
            TimeTable ttt = new TimeTable(t);
            Lesson lesson = new Lesson(t, g, new Subject(1, "s1", SubjectType.INN));

            int day;
            int hour;

            for (int i=0; i<17; i++) {
                day = rand.Next(0,ConstVariable.NUMBER_OF_DAYS);
                hour = rand.Next(0, ConstVariable.NUMBER_OF_SLOTS_IN_DAY);
                gtt.addLesson(lesson, day, hour);

                day = rand.Next(0, ConstVariable.NUMBER_OF_DAYS);
                hour = rand.Next(0, ConstVariable.NUMBER_OF_SLOTS_IN_DAY);
                ttt.addLesson(lesson, day, hour);
            }

            List<TimeSlot> gts = gtt.getFreeTimeSlot();
            List<TimeSlot> tts = ttt.getFreeTimeSlot();
            List<TimeSlot> ts = new SchoolTimeTable().getTheSameSlots(gts,tts);

            gtt.printTimeTable();
            foreach(TimeSlot s in gts) {
                Console.Write(s.ToString()+" ");
            }
            Console.WriteLine("");
            ttt.printTimeTable();
            foreach (TimeSlot s in tts)
            {
                Console.Write(s.ToString() + " ");
            }
            Console.WriteLine("\n\n");
            foreach (TimeSlot s in ts)
            {
                Console.Write(s.ToString() + " ");
            }
            Console.WriteLine("");


            Boolean result = true;

            foreach (TimeSlot ti in ts) {
                if ( !(gtt.isEmpty(ti.day,ti.hour) && ttt.isEmpty(ti.day,ti.hour)) ) {
                    result = false;
                }
            }

            Assert.IsTrue(result,"Time slots is not free");

        }
    }
}
