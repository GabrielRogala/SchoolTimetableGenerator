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
        private int aountOfLessons;
        private static Random rand = new Random();

        public SchoolTimeTable()
        {
            groupTimeTables = new List<TimeTable>();
            teacherTimeTables = new List<TimeTable>();
            lessons = new List<Lesson>();
            aountOfLessons = 0;
        }

        public SchoolTimeTable(List<Lesson> lessons, List<Teacher> teachers, List<Group> groups) : this()
        {
            addLessons(lessons);
            generateTeacherTimeTables(teachers);
            generateGroupTimeTables(groups);
            aountOfLessons = lessons.Count();
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

        public void mutate()
        {

            //var fitnessOfTimaTable = new Dictionary<TimeTable, int>();

            //foreach (TimeTable tt in groupTimeTables)
            //{
            //    fitnessOfTimaTable[tt] = tt.fitness();
            //}

            //TimeTable timeTableToMutate = groupTimeTables[0];

            //foreach (TimeTable tt in groupTimeTables)
            //{
            //    if (fitnessOfTimaTable[tt] > fitnessOfTimaTable[timeTableToMutate])
            //    {
            //        timeTableToMutate = tt;
            //    }
            //}

            //List<TimeSlot> timeSlots = new List<TimeSlot>();

            //for (int d = 0; d < ConstVariable.NUMBER_OF_DAYS; d++)
            //{
            //    for (int h = 0; h < ConstVariable.NUMBER_OF_SLOTS_IN_DAY; h++)
            //    {
            //        if (!timeTableToMutate.isEmpty(d,h)) {
            //            timeSlots.Add(new TimeSlot(d, h));
            //        }
            //    }
            //}

            //double sizeOfTimeSlots = timeSlots.Count();
            //int amoutToChange = (int)( sizeOfTimeSlots * ConstVariable.SUBJECT_PERCENT_TO_MUTATE);

            //List<Lesson> lessonsToChange = new List<Lesson>();

            //for (int i = 0; i < amoutToChange; i++) {
            //    TimeSlot tmp_slot = timeSlots[rand.Next((int)sizeOfTimeSlots - 1)];
            //    timeSlots.Remove(tmp_slot);
            //    lessonsToChange.Add(timeTableToMutate.getDays()[tmp_slot.day].getSlots()[tmp_slot.hour].getLesson(0));
            //}

            //foreach (Lesson l in lessonsToChange) {
            //    Console.WriteLine(l.ToString());
            //}



            List<Lesson> lessonsToChange = new List<Lesson>();

            foreach (TimeTable tt in groupTimeTables) {
            
                List<TimeSlot> tmpTimeSlots = tt.getLessonsTimeSlot();

                int amoutToChange = (int)( tmpTimeSlots.Count() * ConstVariable.SUBJECT_PERCENT_TO_MUTATE);

                for (int i = 0; i < amoutToChange; i++) {
                    TimeSlot tmp_slot = tmpTimeSlots[rand.Next(tmpTimeSlots.Count() - 1)];
                    if (tt.getLesson(tmp_slot.day, tmp_slot.hour).Count == 1)
                    {
                        Lesson tmp_lesson = tt.getLesson(tmp_slot.day, tmp_slot.hour)[0];

                        lessonsToChange.Add(tmp_lesson);
                        tmpTimeSlots.Remove(tmp_slot);

                        tt.removeLesson(tmp_lesson, tmp_slot.day, tmp_slot.hour);
                        tmp_lesson.getTeacher().getTimeTable().removeLesson(tmp_lesson, tmp_slot.day, tmp_slot.hour);

                    } else {
                        tmpTimeSlots.Remove(tmp_slot);
                        --i;
                    }
                }
            }

            lessons = lessonsToChange;

            generateSchoolTimeTable();
        }

        public void sortLessonList(List<Lesson> lessons) {
            lessons.Sort(new Comparison<Lesson>(lessonComparator));
        }

        public Boolean removeLessonsAndFindNewPosition(Lesson lesson) {
            Boolean result = true;
            Boolean isGroupHaveMoreSlots = true;
            

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
                return bestTimeSlots[rand.Next(0, bestTimeSlots.Count - 1)];
            } else{
                return worstTimeSlots[rand.Next(0, worstTimeSlots.Count - 1)];
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

        public void crossSchoolTimeTables(SchoolTimeTable stt1, SchoolTimeTable stt2) {
                        
            foreach (TimeTable tt1 in stt1.groupTimeTables)
            {
                TimeTable tt2 = null;
                
                foreach (TimeTable tt2_tmp in stt2.groupTimeTables) {
                    if (tt1.group.Equals(tt2_tmp.group)) {
                        tt2 = tt2_tmp;
                        break;
                    }
                }
                
                if (tt2 != null)
                {
                    tt1.printTimeTable();
                    tt2.printTimeTable();

                    List<TimeSlot> slots = getTheSameTimeSlotInTheSameLesson(tt1, tt2);

                    foreach (TimeSlot slot in slots) {
                        Lesson lesson = tt1.getLesson(slot.day,slot.hour)[0];

                        if (lesson.getSize() == 1) {
                            TimeTable teacherTimeTable = null;
                            
                            foreach (TimeTable teacherTT in this.teacherTimeTables) {
                                if (teacherTT.Equals(lesson.getTeacher())) {
                                    teacherTimeTable = teacherTT;
                                    break;
                                }
                            }

                            TimeTable groupTimeTable = null;

                            foreach (TimeTable groupTT in this.groupTimeTables)
                            {
                                if (groupTT.Equals(lesson.getGroup()))
                                {
                                    groupTimeTable = groupTT;
                                    break;
                                }
                            }

                            teacherTimeTable.addLesson(lesson, slot.day, slot.hour);
                            groupTimeTable.addLesson(lesson, slot.day, slot.hour);

                        }
                    }

                }
            }

            lessons = getListOfLessonToInsert();
            this.generateSchoolTimeTable();

            this.printTimeTable();
            
        }

        public List<Lesson> getListOfLessonToInsert() {
            List<Lesson> lessons_tmp = new List<Lesson>();
            lessons_tmp.AddRange(this.lessons);

            List<Lesson> lessonToRemove = this.getListOfLessonsInTimeTables();
            //Console.WriteLine("--------------------");
            foreach (Lesson l in lessonToRemove) {
                foreach (Lesson ll in lessons_tmp) {
            //        Console.Write(ll.ToString()  + " <-> " +l.ToString());
                    if (l.Equals(ll) && ll.getSize() == 1)
                    {
                            lessons_tmp.Remove(ll);
            //            Console.Write(" X");
                        break;
                    }
            //        Console.WriteLine("");
                }
            //    Console.WriteLine("");
            }
            //Console.WriteLine("---------------------");
            return lessons_tmp;
        }

        private List<Lesson> getListOfLessonsInTimeTables()
        {
            List<Lesson> lessons_tmp = new List<Lesson>();

            foreach (TimeTable tt in this.groupTimeTables) {
                for (int d = 0; d < ConstVariable.NUMBER_OF_DAYS; d++){
                    for (int h = 0; h < ConstVariable.NUMBER_OF_SLOTS_IN_DAY; h++){
                        lessons_tmp.AddRange(tt.getLesson(d,h));
                    }
                }
            }

            return lessons_tmp;
        }

        public List<TimeSlot> getTheSameTimeSlotInTheSameLesson(TimeTable tt1, TimeTable tt2) {
            List<TimeSlot> slots = new List<TimeSlot>();

            for (int d = 0; d < ConstVariable.NUMBER_OF_DAYS; d++) {
                for (int h = 0; h < ConstVariable.NUMBER_OF_SLOTS_IN_DAY; h++)
                {

                    if ((tt1.getLesson(d, h).Count > 0) && (tt2.getLesson(d, h).Count > 0))
                    {
                        Console.Write("# : " + tt1.getLesson(d, h)[0].ToString() + "<->" + tt2.getLesson(d, h)[0].ToString());
                        if (tt1.getLesson(d, h)[0].getSubject().Equals(tt2.getLesson(d, h)[0].getSubject()))
                        {
                            Console.WriteLine(" < OK");
                            slots.Add(new TimeSlot(d, h));
                        }
                        else {
                            Console.WriteLine("");
                        }
                    }
                }
            }
            
            return slots;
        }

    }
}
