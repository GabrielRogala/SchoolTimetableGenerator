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

            sortLessonList(lessons);

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
            if (o1.lesson.getSize() > o2.lesson.getSize())
            {
                return -1;
            }
            else if (o1.lesson.getSize() < o2.lesson.getSize()) {
                return 1;
            }
            else
            {
                return o1.size.CompareTo(o2.size);
            }
        }

        public int lessonComparator(Lesson l1, Lesson l2) {
            return sizeLessonComparator(l1,l2);
        }

        public int sizeLessonComparator(Lesson l1, Lesson l2)
        {
            if (l1.getSize() > l2.getSize())
            {
                return 1;
            }
            else if (l1.getSize() < l2.getSize())
            {
                return -1;
            }
            else
            {
                return amountLessonComparator(l1, l2);
            }
        }

        public int typeLessonComparator(Lesson l1, Lesson l2)
        {
            if ((int)l1.getSubject().getSubjectType() < (int)l2.getSubject().getSubjectType())
            {
                return 1;
            }
            else if ((int)l1.getSubject().getSubjectType() > (int)l2.getSubject().getSubjectType())
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public int amountLessonComparator(Lesson l1, Lesson l2)
        {
            if (l1.getAmount() > l2.getAmount())
            {
                return 1;
            }
            else if (l1.getAmount() < l2.getAmount())
            {
                return -1;
            }
            else
            {
                return typeLessonComparator(l1, l2);
            }
        }

        public void mutate() {
            //-----------------------------------------
        }

        public void sortLessonList(List<Lesson> lessons) {
            lessons.Sort(new Comparison<Lesson>(lessonComparator));
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

                List<TimeSlot> firstSlots = firstTT.getFreeTimeSlot(lesson.getSize());
                List<TimeSlot> secondSlots = secondTT.getFreeTimeSlot(lesson.getSize());
                /**
                 * do poprawienia
                 * dopisac uwzglednianie wiekszych lekcji
                 * zwalnianie slotow przy zamianie
                 * nie zmieniac wiekszych slotow
                 */
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
                        Console.WriteLine("find free slots");
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

                foreach (Lesson lesson in positionedLessons)
                {
                    groupSlots = group.getTimeTable().getFreeTimeSlot(lesson.getSize());
                    teacherSlots = lesson.getTeacher().getTimeTable().getFreeTimeSlot(lesson.getSize());
                    theSameSlots = getTheSameSlots(groupSlots, teacherSlots);
                    freeSlotsToLesson.Add(new FreeSlotsToLesson(theSameSlots, lesson));

                    foreach (TimeSlot ts in theSameSlots) {
                        currentTimeTable.addLesson(lesson, ts.day, ts.hour);
                        if (!freeSlots.Contains(ts)) {
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
                    if (fstl.slots.Count > 0)
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

                        TimeSlot bestSlot = getBestTimeSlot(fstl.slots);
                        for (int i = 0; i < fstl.lesson.getSize(); ++i)
                        {
                            if (fstl.lesson.getGroup().getTimeTable().getDays()[bestSlot.day].getSlots()[bestSlot.hour+i].isEmpty())
                            {
                                fstl.lesson.addTimeSlot(new TimeSlot(bestSlot.day, bestSlot.hour +i));
                                fstl.lesson.getGroup().getTimeTable().addLesson(fstl.lesson, bestSlot.day, bestSlot.hour+i);
                                fstl.lesson.getTeacher().getTimeTable().addLesson(fstl.lesson, bestSlot.day, bestSlot.hour+i);
                            } else {
                                Console.WriteLine("ERROR!!!!!!!!!!!!!!!!");
                            }
                        }
                        
                        foreach (FreeSlotsToLesson tmp in freeSlotsToLesson) {
                            // Console.WriteLine("check " + tmpSlot.ToString());

                            for (int i = 0; i < fstl.lesson.getSize(); ++i)
                            {
                                TimeSlot tmpSlot = new TimeSlot(bestSlot.day, bestSlot.hour + i);

                                if (tmp.slots.Contains(tmpSlot)) {
                                    int timeSlotIndexToRemove = tmp.slots.IndexOf(tmpSlot);
                                    TimeSlot tsToRemove = tmp.slots[timeSlotIndexToRemove];
                                    tmp.slots.Remove(tsToRemove);
                                }
                            }
                        }
                        
                    }
                    else {
                        //if (!removeLessonsAndFindNewPosition(fstl.lesson))
                            Console.WriteLine("ERROR! \t" + fstl.lesson.ToString() + "have not free slots");
                        
                    }
                }



            }
        }

        public TimeSlot getBestTimeSlot(List<TimeSlot> slots) {
            List<TimeSlot> bestTimeSlots = new List<TimeSlot>();
            List<TimeSlot> worstTimeSlots = new List<TimeSlot>();

            foreach (TimeSlot ts in slots) {
                if (ts.hour > ConstVariable.BOTTOM_BORDER_OF_BEST_SLOTS && ts.hour < ConstVariable.TOP_BORDER_OF_BEST_SLOTS)
                {
                    bestTimeSlots.Add(ts);
                }
                else {
                    worstTimeSlots.Add(ts);
                }
            }

            if (bestTimeSlots.Count > 0) {
                return bestTimeSlots[new Random().Next(0, bestTimeSlots.Count - 1)];
            } else{
                return worstTimeSlots[new Random().Next(0, worstTimeSlots.Count - 1)];
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
            for (int i = lessons.Count - 1 ; i >= 0; i--)
            {
              //  Console.WriteLine("lesson : "+l.ToString());
                if (indexs.Count < ConstVariable.NUMBER_OF_LESSONS_TO_POSITIONING )
                {
                    if (lessons[i].getGroup().Equals(lessons[indexs[0]].getGroup()))
                    {
                //        Console.WriteLine(" # ");
                        if (!isContainInLessonsList(indexs, lessons[i]))
                        {
                  //          Console.WriteLine("add");
                            indexs.Add(lessons.IndexOf(lessons[i]));
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
