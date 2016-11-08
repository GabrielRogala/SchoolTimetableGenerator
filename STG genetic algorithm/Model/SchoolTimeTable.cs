using STG_genetic_algorithm.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class SchoolTimeTable
    {

        private int id;
        private List<TimeTable> groupTimeTables;
        private List<TimeTable> teacherTimeTables;
        public List<Lesson> lessons;

        public SchoolTimeTable()
        {
            groupTimeTables = new List<TimeTable>();
            teacherTimeTables = new List<TimeTable>();
            lessons = new List<Lesson>();
        }

        public SchoolTimeTable(List<Lesson> lessons, List<Teacher> teachers, List<Group> groups) : this()
        {
            addLessons(lessons);
            generateTeacherTimeTables(teachers);
            generateGroupTimeTables(groups);
        }

        public void addLessons(List<Lesson> lessons) {
            foreach (Lesson l in lessons)
            {
                this.lessons.Add(l);
            }
        }

        public void generateTeacherTimeTables(List<Teacher> teachers)
        {
            foreach (Teacher t in teachers)
            {
                TimeTable tt = new TimeTable(t);
                t.setTimeTable(tt);
                this.teacherTimeTables.Add(tt);
            }
        }

        public void generateGroupTimeTables(List<Group> groups)
        {
            foreach (Group g in groups)
            {
                TimeTable tt = new TimeTable(g);
                g.setTimeTable(tt);
                this.groupTimeTables.Add(new TimeTable(g));
            }
        }

        public void generateSchoolTimeTable() {
            List<int> indexs = new List<int>();
            List<Lesson> positionedLessons = new List<Lesson>();

            lessons.Sort(new Comparison<Lesson>(lessonComparator));

            while (lessons.Count > 0) {
                indexs.Add(lessons.Count - 1);

                indexs = findDifferentSubjectTheSameGroup(indexs);

                foreach (int i in indexs) {
                    positionedLessons.Add(lessons[i]);
                }

                findAndSetBestPositionToLessons(positionedLessons);

                foreach (Lesson l in positionedLessons) {
                    lessons.Remove(l);
                }

                indexs.Clear();
                positionedLessons.Clear();
            }
        }

        public int BFSComparator(FreeSlotsToLesson o1, FreeSlotsToLesson o2)
        {
            return o1.size.CompareTo(o2.size);
        }

        public int lessonComparator(Lesson l1, Lesson l2) {
            return 0;
        }

        public void mutate() {
            //-----------------------------------------
        }

        public List<Lesson> sortLessonList(List<Lesson> lessons) {
            return lessons;
        }

        public Boolean removeLessonsAndFindNewPosition(Lesson lesson) {
            Boolean result = true;
            Boolean isGroupHaveMoreSlots = true;
            Random rand = new Random();

            TimeTable groupTT = lesson.getGroup().getTimeTable();
            TimeTable teacherTT = lesson.getTeacher().getTimeTable();
            List<TimeSlot> groupSlots = groupTT.getFreeTimeSlot();
            List<TimeSlot> teacherSlots = teacherTT.getFreeTimeSlot();

            if (getTheSameSlots(groupSlots,teacherSlots).Count == 0) {
                TimeTable firstTT = groupTT;
                TimeTable secondTT = teacherTT;
                isGroupHaveMoreSlots = true;

                if (groupSlots.Count > teacherSlots.Count) {
                    firstTT = teacherTT;
                    secondTT = groupTT;
                    isGroupHaveMoreSlots = false;
                }
                /*
                first - plan który posiada mniej wolnych slotów
                second - plan który posiada więcej wolnych slotow

                w "second" szukamy lekcji mozliwych dozamiany w slotach  planu "first"
                */

                List<TimeSlot> firstSlots = firstTT.getFreeTimeSlot();
                List<TimeSlot> secondSlots = secondTT.getFreeTimeSlot();

                while (firstSlots.Count > 0) {
                    int slotsIndex = rand.Next(0, firstSlots.Count);
                    TimeSlot timeSlotToClear = firstSlots[slotsIndex];
                    Lesson checkedLesson = secondTT.getDays()[timeSlotToClear.day].getSlots()[timeSlotToClear.hour].getLesson(0);

                    List<TimeSlot> slotsToCompare;
                    if (isGroupHaveMoreSlots) {
                        slotsToCompare = checkedLesson.getGroup().getTimeTable().getFreeTimeSlot();
                    } else {
                        slotsToCompare = checkedLesson.getTeacher().getTimeTable().getFreeTimeSlot();
                    }

                    List<TimeSlot> freeSlots = getTheSameSlots(slotsToCompare, secondSlots);
                    if (freeSlots.Count > 0 ) {
                        int newSlotIndex = rand.Next(0, freeSlots.Count);

                        // wstawaimy lekcje do przeniesienia
                        checkedLesson.getGroup().getTimeTable().addLesson(checkedLesson, freeSlots[newSlotIndex].day, freeSlots[newSlotIndex].hour);
                        checkedLesson.getTeacher().getTimeTable().addLesson(checkedLesson, freeSlots[newSlotIndex].day, freeSlots[newSlotIndex].hour);
                        // zwalniamy wolny slot
                        checkedLesson.getGroup().getTimeTable().removeLesson(checkedLesson, timeSlotToClear.day, timeSlotToClear.hour);
                        checkedLesson.getTeacher().getTimeTable().removeLesson(checkedLesson, timeSlotToClear.day, timeSlotToClear.hour);
                        // ustawiam nowa lekcje
                        lesson.getGroup().getTimeTable().addLesson(lesson, freeSlots[newSlotIndex].day, freeSlots[newSlotIndex].hour);
                        lesson.getTeacher().getTimeTable().addLesson(lesson, freeSlots[newSlotIndex].day, freeSlots[newSlotIndex].hour);

                        break;
                    }


                    firstSlots.RemoveAt(slotsIndex);
                }
                if (firstSlots.Count == 0)
                {
                    Console.WriteLine("NOT FOUND");

                    // szukanie wgłąb
                    firstSlots = firstTT.getFreeTimeSlot();
                    while (firstSlots.Count > 0)
                    {
                        int slotsIndex = rand.Next(0, firstSlots.Count);
                        TimeSlot timeSlotToClear = firstSlots[slotsIndex];
                        Lesson checkedLesson = secondTT.getDays()[timeSlotToClear.day].getSlots()[timeSlotToClear.hour].getLesson(0);

                        if (removeLessonsAndFindNewPosition(checkedLesson))
                        {
                            removeLessonsAndFindNewPosition(lesson);
                            break;
                        }

                        firstSlots.RemoveAt(slotsIndex);
                    }

                }

            }

            return result;
        }

        public void findAndSetBestPositionToLessons(List<Lesson> positionedLessons)
        {
            Random rand = new Random();
            TimeTable currentTimeTable = new TimeTable();
            List<TimeSlot> freeSlots = new List<TimeSlot>();
            List<TimeSlot> groupSlots;
            List<TimeSlot> teacherSlots;
            List<TimeSlot> theSameSlots;
            Group group;
            List<FreeSlotsToLesson> freeSlotsToLesson = new List<FreeSlotsToLesson>();

            if (positionedLessons.Count > 0)
            {
                group = positionedLessons[0].getGroup();
                groupSlots = group.getTimeTable().getFreeTimeSlot();

                foreach (Lesson lesson in positionedLessons)
                {
                    teacherSlots = lesson.getTeacher().getTimeTable().getFreeTimeSlot();
                    theSameSlots = getTheSameSlots(groupSlots, teacherSlots);
                    freeSlotsToLesson.Add(new FreeSlotsToLesson(theSameSlots, lesson));

                    foreach (TimeSlot ts in theSameSlots) {
                        currentTimeTable.addLesson(lesson, ts.day, ts.hour);
                        if (freeSlots.Contains(ts)) {
                            freeSlots.Add(ts);
                        }
                    }
                }

                freeSlotsToLesson.Sort(new Comparison<FreeSlotsToLesson>(BFSComparator));
                
                foreach (FreeSlotsToLesson fstl in freeSlotsToLesson) {
                    Console.WriteLine(fstl.ToString());
                }

                //-------------------------------------------------
                // posortowane lekcje

                foreach (FreeSlotsToLesson fstl in freeSlotsToLesson)
                {
                    if (fstl.size > 0)
                    {
                        List<int> indexOfSlotsWithMaxCount = new List<int>();
                        int max = 0;
                        // find max
                        foreach (TimeSlot ts in fstl.slots) {
                            int currentTiteTableSlotCount = currentTimeTable.getDays()[ts.day].getSlots()[ts.hour].getLessons().Count;
                            if (max < currentTiteTableSlotCount) {
                                max = currentTiteTableSlotCount;
                            }
                        }

                        foreach (TimeSlot ts in fstl.slots) {
                            int currentTiteTableSlotCount = currentTimeTable.getDays()[ts.day].getSlots()[ts.hour].getLessons().Count;
                            if (max == currentTiteTableSlotCount) {
                                indexOfSlotsWithMaxCount.Add(fstl.slots.IndexOf(ts));
                            }
                        }

                        int randIndex = rand.Next(0, indexOfSlotsWithMaxCount.Count);

                        if (fstl.lesson.getGroup().getTimeTable().getDays()[fstl.slots[randIndex].day].getSlots()[fstl.slots[randIndex].hour].isEmpty())
                        {
                            fstl.lesson.getGroup().getTimeTable().addLesson(fstl.lesson, fstl.slots[randIndex].day, fstl.slots[randIndex].hour);
                            fstl.lesson.getTeacher().getTimeTable().addLesson(fstl.lesson, fstl.slots[randIndex].day, fstl.slots[randIndex].hour);
                        } else {
                            Console.WriteLine("ERROR!!!!!!!!!!!!!!!!");
                        }

                        TimeSlot tmpSlot = fstl.slots[randIndex];
                        
                        foreach (FreeSlotsToLesson tmp in freeSlotsToLesson) {
                           // Console.WriteLine("check " + tmpSlot.ToString());
                            if (tmp.slots.Contains(tmpSlot))
                            {
                                int timeSlotIndexToRemove = tmp.slots.IndexOf(tmpSlot);
                                TimeSlot tsToRemove = tmp.slots[timeSlotIndexToRemove];
                                tmp.slots.Remove(tsToRemove);
                             //   Console.WriteLine("remove");
                            }
                            else {
                                //Console.WriteLine("not found "+ tmpSlot.ToString());
                            }
                        }
                        
                    }
                    else {
                        Console.WriteLine("ERROR! \t" + fstl.lesson.ToString() + "have not free slots");
                    }
                }



            }
        }

        public void timeTableCheckSume() {
            Boolean result = true;
            foreach (TimeTable tt in groupTimeTables)
            {
                result = (result && tt.checkSume(lessons));
            }

            foreach (TimeTable tt in teacherTimeTables)
            {
                result = (result && tt.checkSume(lessons));
            }

            Console.WriteLine(result);
        }

        public List<TimeSlot> getTheSameSlots(List<TimeSlot> freeSlotsGroup, List<TimeSlot> freeSlotsTeacher)
        {
            List<TimeSlot> freeSlot = new List<TimeSlot>();

            int indexTeacher = 0;

            for (int indexGroup = 0; indexGroup < freeSlotsGroup.Count() && indexTeacher < freeSlotsTeacher.Count();)
            {
                if (freeSlotsGroup[indexGroup].Equals(freeSlotsTeacher[indexTeacher]))
                {
                    freeSlot.Add(new TimeSlot(freeSlotsGroup[indexGroup].day, freeSlotsGroup[indexGroup].hour));
                    indexTeacher++;
                    indexGroup++;
                }
                else
                {
                    if (freeSlotsGroup[indexGroup].day > freeSlotsTeacher[indexTeacher].day)
                    {
                        indexTeacher++;
                    }
                    else if (freeSlotsGroup[indexGroup].day < freeSlotsTeacher[indexTeacher].day)
                    {
                        indexGroup++;
                    }
                    else
                    {
                        if (freeSlotsGroup[indexGroup].hour > freeSlotsTeacher[indexTeacher].hour)
                        {
                            indexTeacher++;
                        }
                        else
                        {
                            indexGroup++;
                        }
                    }
                }
            }

            return freeSlot;
        }

        public List<int> findDifferentSubjectTheSameGroup(List<int> indexList)
        {
            List<int> indexs = indexList;
            //Console.WriteLine("check : "+lessons[indexs[0]].ToString());
            foreach (Lesson l in lessons)
            {
              //  Console.WriteLine("lesson : "+l.ToString());
                if (indexs.Count < ConstVariable.NUMBER_OF_LESSONS_TO_POSITIONING )
                {
                    if (l.getGroup().Equals(lessons[indexs[0]].getGroup()))
                    {
                //        Console.WriteLine(" # ");
                        if (!isContainInLessonsList(indexs, l))
                        {
                  //          Console.WriteLine("add");
                            indexs.Add(lessons.IndexOf(l));
                        }
                    }
                }
                else
                {
                    break;
                }
            }
      
            return indexs;
        }

        public Boolean isContainInLessonsList(List<int> indexs, Lesson lesson)
        {
            foreach (int i in indexs)
            {
                if (!this.lessons[i].isDifferent(lesson))
                {
                    return true;
                }
            }
            return false;
        }

        public List<TimeSlot> findFreeSlots(TimeTable groupTimeTable, TimeTable teacherTimeTable)
        {
            List<TimeSlot> slots = new List<TimeSlot>();

            int day = 0;
            int hour = 0;
            foreach (Day d in groupTimeTable.getDays())
            {
                hour = 0;
                foreach (Slot h in d.getSlots())
                {
                    if (groupTimeTable.isEmpty(day, hour) && teacherTimeTable.isEmpty(day, hour))
                    {
                        slots.Add(new TimeSlot(day, hour));
                    }
                    hour++;
                }
                day++;
            }

            return slots;
        }

        public int fitness()
        {
            int value = 0;
            foreach (TimeTable tt in groupTimeTables)
            {
                value += tt.fitness();
            }
            foreach (TimeTable tt in teacherTimeTables)
            {
                value += tt.fitness();
            }
            return value;
        }

        public void printSimpleTimeTable()
        {
            foreach (TimeTable tt in groupTimeTables)
            {
                tt.printSimpleTimeTable();
            }

            foreach (TimeTable tt in teacherTimeTables)
            {
                tt.printSimpleTimeTable();
            }
        }

        public void printTimeTable()
        {
            foreach (TimeTable tt in groupTimeTables)
            {
                tt.printTimeTable();
            }

            foreach (TimeTable tt in teacherTimeTables)
            {
                tt.printTimeTable();
            }
        }

    }
}
