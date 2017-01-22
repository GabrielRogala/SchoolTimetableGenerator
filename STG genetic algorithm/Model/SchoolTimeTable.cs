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
        public List<TimeTable> teacherTimeTables;
        private List<TimeTable> roomTimeTables;
        public List<Lesson> lessons;
        private int aountOfLessons;
        private static Random rand = new Random();
        private int value;
        //ok
        public SchoolTimeTable()
        {
            groupTimeTables = new List<TimeTable>();
            teacherTimeTables = new List<TimeTable>();
            roomTimeTables = new List<TimeTable>();
            lessons = new List<Lesson>();
            aountOfLessons = 0;
            id = 0;
            value = 0;
        }
        //ok
        public SchoolTimeTable(List<Lesson> lessons, List<Teacher> teachers, List<Group> groups) : this()
        {
            addLessons(lessons);
            generateTeacherTimeTables(teachers);
            generateGroupTimeTables(groups);
            aountOfLessons = lessons.Count();
        }
        //ok
        public SchoolTimeTable(List<Lesson> lessons, List<Teacher> teachers, List<Group> groups, List<Room> rooms) : this()
        {
            addLessons(lessons);
            generateTeacherTimeTables(teachers);
            generateGroupTimeTables(groups);
            generateRoomTimeTables(rooms);
            aountOfLessons = lessons.Count();
        }
        //
        public SchoolTimeTable(List<Lesson> lessons, List<Teacher> teachers, List<Group> groups, SchoolTimeTable stt) : this(lessons,teachers,groups) {
            for (int i =0; i < stt.groupTimeTables.Count; ++i){

                TimeTable gtt = stt.groupTimeTables[i];

                for (int d = 0; d < ConstVariable.NUMBER_OF_DAYS; ++d) {
                    for (int h = 0; h < ConstVariable.NUMBER_OF_SLOTS_IN_DAY; ++h)
                    {
                        

                        if (gtt.getLesson(d, h).Count > 0)
                        {
                            Lesson tmp = gtt.getLesson(d, h)[0];
                            int index = this.lessons.IndexOf(tmp);
                            Lesson tmpT = this.lessons[index];
                            this.groupTimeTables[i].addLesson(tmpT, d, h);
                            this.teacherTimeTables[getIndexOfTeacher(tmpT.getTeacher())].addLesson(tmpT, d, h);

                        } 

                    }
                }

            }
        }

        public int getIndexOfTeacher(Teacher teacher) {
            int index = 0;
            
            foreach (TimeTable tt in teacherTimeTables) {
                if (tt.teacher.Equals(teacher)) {
                    break;
                }
                index++;
            }

            return index;
        }
        //ok
        public void setId(int id) {
            this.id = id;
        }
        //ok
        public int getId() {
            return id;
        }
        //ok
        public int getValue() {
            return value;
        }
        //ok
        public void addLessons(List<Lesson> lessons) {
            foreach (Lesson l in lessons)
            {
                this.lessons.Add(l);
            }
        }
        //ok
        public void generateTeacherTimeTables(List<Teacher> teachers)
        {
            foreach (Teacher t in teachers)
            {
                TimeTable tt = new TimeTable(t);
                t.setTimeTable(tt);
                this.teacherTimeTables.Add(tt);
            }
        }
        //ok
        public void generateGroupTimeTables(List<Group> groups)
        {
            foreach (Group g in groups)
            {
                TimeTable tt = new TimeTable(g);
                g.setTimeTable(tt);
                this.groupTimeTables.Add(new TimeTable(g));
            }
        }
        //ok
        public void generateRoomTimeTables(List<Room> rooms)
        {
            foreach (Room r in rooms)
            {
                TimeTable tt = new TimeTable(r);
                r.setTimeTable(tt);
                this.roomTimeTables.Add(tt);
            }
        }
        //ok
        public void generateSchoolTimeTable() {
            List<int> indexs = new List<int>();
            List<Lesson> positionedLessons = new List<Lesson>();

            sortLessonList(lessons);
            //>>>>>>>>>>>>>>>>>>>>
            foreach (Lesson l in lessons){ Console.WriteLine(l.ToString()); }
            //<<<<<<<<<<<<<<<<<<<<

            while (lessons.Count > 0) {
                indexs.Add(lessons.Count - 1);
                //znajduje lekcje do wstawienia
                indexs = findDifferentSubjectTheSameGroup(indexs);
                foreach (int i in indexs) {
                    positionedLessons.Add(lessons[i]);
                }
                //>>>>>>>>>>>>>>>>>>>>
                Console.WriteLine("------WYBRANO-------");
                foreach (Lesson l in positionedLessons) { Console.WriteLine(l.ToString()); }
                Console.WriteLine("--------------------");
                //<<<<<<<<<<<<<<<<<<<<

                //ustawia lekcje w sloty
                findAndSetBestPositionToLessons(positionedLessons);

                //usuwa wstawione lekcje
                foreach (Lesson l in positionedLessons) {
                    lessons.Remove(l);
                }

                indexs.Clear();
                positionedLessons.Clear();
            }
        }
        //
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
        //
        public int lessonComparator(Lesson l1, Lesson l2) {
            return sizeLessonComparator(l1,l2);
        }
        //
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
        //
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
        //
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
        //ok
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
                        //zwolnic sale
                        foreach (TimeTable roomTT in roomTimeTables) {
                            if (roomTT.getLesson(tmp_slot.day, tmp_slot.hour)[0] == tmp_lesson) {
                                roomTT.removeLesson(tmp_lesson, tmp_slot.day, tmp_slot.hour);
                            }
                        }


                    } else {
                        tmpTimeSlots.Remove(tmp_slot);
                        --i;
                    }
                }
            }

            lessons = lessonsToChange;

            generateSchoolTimeTable();
        }
        //ok
        public void sortLessonList(List<Lesson> lessons) {
            lessons.Sort(new Comparison<Lesson>(lessonComparator));
        }

        public Boolean removeLessonsAndFindNewPosition(Lesson lesson) {
            bool result = true;

            TimeTable groupTT = lesson.getGroup().getTimeTable();                               //plan zajęć dla grupy szukanej lekcji
            TimeTable teacherTT = lesson.getTeacher().getTimeTable();                           //plan zajec dla nauczyciela szukanej lekcji
            List<TimeSlot> groupSlots = groupTT.getFreeTimeSlot(lesson.getSize());              //wolne sloty grupy
            List<TimeSlot> teacherSlots = teacherTT.getFreeTimeSlot(lesson.getSize());          //wolne sloty nauczyciela
            List<FreeSlotsToLesson> freeSlotsToLesson = new List<FreeSlotsToLesson>();
            List<TimeSlot> slotsToChange = new List<TimeSlot>();
            TimeSlot bestSlot = null;
            Lesson lessonToMove = null;

            for (int d = 0; d<ConstVariable.NUMBER_OF_DAYS;d++) {
                for (int h = 0; h < ConstVariable.NUMBER_OF_SLOTS_IN_DAY; h++){
                    ////pobranie wolnych sal i grupy/nauczyciela
                    ////porównanie wolnych slotów nauczyciela, grupy i sal 
                    ////dodanie do listy
                    if (true) {
                        slotsToChange.Add(new TimeSlot(d, h));
                    }
                }
            }

            bestSlot = slotsToChange[rand.Next(slotsToChange.Count-1)];                         //wylosowanie z listy najlepszego slotu

            //wstawienie zmienianej lekcji
            lessonToMove = groupTT.getLesson(bestSlot.day, bestSlot.hour)[0];
            ////

            //usuniecie zmienianej lekcji z poprzedniej pozycji
            groupTT.removeLesson(lessonToMove, bestSlot.day, bestSlot.hour);
            teacherTT.removeLesson(lessonToMove, bestSlot.day, bestSlot.hour);
            
            ////room

            //wstawienie w wybrany slot lekcji do wstawienia
            groupTT.addLesson(lesson, bestSlot.day, bestSlot.hour);
            teacherTT.addLesson(lesson, bestSlot.day, bestSlot.hour);
            ////room


            return result;
            
            /*
            Boolean result = true;
            Boolean isGroupHaveMoreSlots = true;
            List<FreeSlotsToLesson> freeSlotsToLesson = new List<FreeSlotsToLesson>();

            TimeTable groupTT = lesson.getGroup().getTimeTable();                               //plan zajęć dla grupy szukanej lekcji
            TimeTable teacherTT = lesson.getTeacher().getTimeTable();                           //plan zajec dla nauczyciela szukanej lekcji
            List<TimeSlot> groupSlots = groupTT.getFreeTimeSlot(lesson.getSize());              //wolne sloty grupy
            List<TimeSlot> teacherSlots = teacherTT.getFreeTimeSlot(lesson.getSize());          //wolne sloty nauczyciela

            List<FreeSlotsInRoomToLesson> freeSlotsInRoomToLesson = new List<FreeSlotsInRoomToLesson>();    //lista wolnych slotów sal pasujących do lekcji i grupy
            foreach (TimeTable tt in roomTimeTables)
            {
                if (tt.room.type.Equals(lesson.getSubject().getRoomType()) && tt.room.amount >= lesson.getGroup().amount)
                {
                    freeSlotsInRoomToLesson.Add(new FreeSlotsInRoomToLesson(tt.getFreeTimeSlot(), tt.room));
                }
            }

            List<TimeSlot> theSameSlots = getTheSameSlots(groupSlots, teacherSlots);                                       //generuje liste wolnych slotów dla grupt i nauczyciela i sprawdza wolne sale
            freeSlotsToLesson.Add(new FreeSlotsToLesson(theSameSlots, lesson, freeSlotsInRoomToLesson));   //generuje lekcje wraz z listą wolnych slotów


            if (getTheSameSlots(groupSlots,teacherSlots).Count == 0) {
                TimeTable firstTT = groupTT;
                TimeTable secondTT = teacherTT;
                isGroupHaveMoreSlots = true;

                if (groupSlots.Count > teacherSlots.Count) {
                    firstTT = teacherTT;
                    secondTT = groupTT;
                    isGroupHaveMoreSlots = false;
                }
                
                //first - plan który posiada mniej wolnych slotów
                //second - plan który posiada więcej wolnych slotow
                //
                //w "second" szukamy lekcji mozliwych dozamiany w slotach  planu "first"
                

                List<TimeSlot> firstSlots = firstTT.getFreeTimeSlot(lesson.getSize());
                List<TimeSlot> secondSlots = secondTT.getFreeTimeSlot(lesson.getSize());
                 // do poprawienia
                 // dopisac uwzglednianie wiekszych lekcji
                 // zwalnianie slotow przy zamianie
                 // nie zmieniac wiekszych slotow
                 
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
                        //Console.WriteLine("find free slots");
                        break;
                    }


                    firstSlots.RemoveAt(slotsIndex);
                }
                if (firstSlots.Count == 0)
                {
                    //Console.WriteLine("NOT FOUND");

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

            */
        }

        public void findAndSetBestPositionToLessons(List<Lesson> positionedLessons)
        {
            
            TimeTable currentTimeTable = new TimeTable();
            List<TimeSlot> freeSlots = new List<TimeSlot>();
            List<TimeSlot> groupSlots;
            List<TimeSlot> teacherSlots;
            //List<TimeSlot> roomSlots;
            List<TimeSlot> theSameSlots;
            Group group;
            List<FreeSlotsToLesson> freeSlotsToLesson = new List<FreeSlotsToLesson>();

            if (positionedLessons.Count > 0)                                                                        //
            {
                group = positionedLessons[0].getGroup();                                                            //grupa jest taka sama w kazdjej lekcji

                foreach (Lesson lesson in positionedLessons)                                                        // idziemy po kazdej lekcji
                {
                    //>>>>>>>>>>>>>>>>>>>>>>>>
                    //Console.WriteLine("============PORÓWNANIE=============");
                    //group.timeTable.printTimeTable();
                    //lesson.getTeacher().getTimeTable().printTimeTable();
                    //<<<<<<<<<<<<<<<<<<<<<<<<

                    List<FreeSlotsInRoomToLesson> freeSlotsInRoomToLesson = new List<FreeSlotsInRoomToLesson>();
                    foreach (TimeTable tt in roomTimeTables) {
                        //Console.WriteLine(tt.room.ToString());
                        if (tt.room.type.Equals(lesson.getSubject().getRoomType()) && tt.room.amount >= group.amount) {
                            freeSlotsInRoomToLesson.Add(new FreeSlotsInRoomToLesson(tt.getFreeTimeSlot(),tt.room));
                            //>>>>>>>>>>>>>>>>>>>
                            //tt.printTimeTable();
                            //<<<<<<<<<<<<<<<<<<<
                        }
                    }
                    //>>>>>>>>>>>>>>>>>>>>>>>>
                    //Console.WriteLine("====================================");
                    //<<<<<<<<<<<<<<<<<<<<<<<<

                    groupSlots = group.getTimeTable().getFreeTimeSlot(lesson.getSize());                            //pobiera liste wolnych slotów
                    teacherSlots = lesson.getTeacher().getTimeTable().getFreeTimeSlot(lesson.getSize());            //pobiera liste wolnych slotów
                    theSameSlots = getTheSameSlots(groupSlots, teacherSlots);                                       //generuje liste wolnych slotów dla grupt i nauczyciela i sprawdza wolne sale

                    freeSlotsToLesson.Add(new FreeSlotsToLesson(theSameSlots, lesson, freeSlotsInRoomToLesson));   //generuje lekcje wraz z listą wolnych slotów

                    //dodanie list sal do 

                    foreach (TimeSlot ts in theSameSlots) {                                                         //
                        currentTimeTable.addLesson(lesson, ts.day, ts.hour);                                        //dodaje lekcje do globalnego planu w okreslonym slocie (cały plan z wszyskimi wolnymi slotami wszystkich lekcji)
                        if (!freeSlots.Contains(ts)) {                                                              //dodaje wolne sloty o ile jescze nie ma ich na liście (do szybszego wyszukiwania)
                            freeSlots.Add(ts);                                                                      //
                        }
                        //>>>>>>>>>>>>>>>>>>>>
                        //Console.WriteLine(ts.ToString());
                        //<<<<<<<<<<<<<<<<<<<<
                    }
                }

                freeSlotsToLesson.Sort(new Comparison<FreeSlotsToLesson>(BFSComparator));                           //sortuje wolne sloty pod wzgledem ilości lekcji w tym slocie
                //>>>>>>>>>>>>>>>>>>>>>
                //Console.WriteLine("----------wolne sloty dla lekcji----------");
                //foreach (FreeSlotsToLesson fstl in freeSlotsToLesson) { Console.WriteLine(fstl.ToString());}
                //Console.WriteLine("------------------------------------------");
                //<<<<<<<<<<<<<<<<<<<<<
                //-------------------------------------------------
                // posortowane lekcje

                foreach (FreeSlotsToLesson fstl in freeSlotsToLesson)
                {
                    if (fstl.slots.Count > 0)                                                                       //sprawdza czy oby na pewno jest w slocie jakaś lekcja
                    {
                        List<int> indexOfSlotsWithMaxCount = new List<int>();//lista indeksów lekcji o największej ilości wolnych slotów
                        int max = 0;
                        // find max
                        foreach (TimeSlot ts in fstl.slots) {
                            int currentTiteTableSlotCount = currentTimeTable.getDays()[ts.day].getSlots()[ts.hour].getLessons().Count;
                            if (max < currentTiteTableSlotCount) {
                                max = currentTiteTableSlotCount;
                            }
                        }
                        // wyszukanie wszystkich max
                        foreach (TimeSlot ts in fstl.slots) {
                            int currentTiteTableSlotCount = currentTimeTable.getDays()[ts.day].getSlots()[ts.hour].getLessons().Count;
                            if (max == currentTiteTableSlotCount) {
                                indexOfSlotsWithMaxCount.Add(fstl.slots.IndexOf(ts));
                            }
                        }

                        int bestRoomId = 0;//najlepsza sala
                        TimeSlot bestSlot = getBestTimeSlot(fstl.slots,fstl.roomSlots, ref bestRoomId);//najlepszy slot
                                                
                        for (int i = 0; i < fstl.lesson.getSize(); ++i)
                        {
                            if (fstl.lesson.getGroup().getTimeTable().getDays()[bestSlot.day].getSlots()[bestSlot.hour+i].isEmpty())
                            {
                                fstl.lesson.addTimeSlot(new TimeSlot(bestSlot.day, bestSlot.hour +i));
                                fstl.lesson.setRoom(roomTimeTables[bestRoomId].room);
                                fstl.lesson.getGroup().getTimeTable().addLesson(fstl.lesson, bestSlot.day, bestSlot.hour+i);
                                fstl.lesson.getTeacher().getTimeTable().addLesson(fstl.lesson, bestSlot.day, bestSlot.hour+i);
                                roomTimeTables[bestRoomId].addLesson(fstl.lesson, bestSlot.day, bestSlot.hour + i);

                                //>>>>>>>>>>>>>>>>>>>>>>>>
                                fstl.lesson.getGroup().getTimeTable().printTimeTable();
                                fstl.lesson.getTeacher().getTimeTable().printTimeTable();
                                roomTimeTables[bestRoomId].printTimeTable();
                                //<<<<<<<<<<<<<<<<<<<<<<<<
                            } else {
                                //Console.WriteLine("ERROR!!!!!!!!!!!!!!!!");
                            }
                        }
                        //usuwanie z listy ustawionych lekcji
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
                            //Console.WriteLine("ERROR! \t" + fstl.lesson.ToString() + "have not free slots");
                        
                    }
                }



            }
        }

        //wyiera najlepszy slot
        public TimeSlot getBestTimeSlot(List<TimeSlot> slots, List<FreeSlotsInRoomToLesson> roomSlots, ref int bestRoomId) {
            List<TimeSlot> bestTimeSlots = new List<TimeSlot>();//lista najlepszych slotów
            List<TimeSlot> worstTimeSlots = new List<TimeSlot>();//lista najgorszych slotów
            //podział slotów na 2 kategorie
            foreach (TimeSlot ts in slots) {
                if (ts.hour > ConstVariable.BOTTOM_BORDER_OF_BEST_SLOTS && ts.hour < ConstVariable.TOP_BORDER_OF_BEST_SLOTS){
                    bestTimeSlots.Add(ts);
                } else {
                    worstTimeSlots.Add(ts);
                }
            }
            TimeSlot slot = null;
            //zwraca losowy slot z listy najlepszych o ile taki istnieje
            if (bestTimeSlots.Count > 0) {
                slot = bestTimeSlots[rand.Next(0, bestTimeSlots.Count - 1)];
            } else{
                slot = worstTimeSlots[rand.Next(0, worstTimeSlots.Count - 1)];
            }

            List<Room> freeRooms = new List<Room>();
            foreach (FreeSlotsInRoomToLesson ro in roomSlots) {
                if (ro.slots.Contains(slot)) {
                    freeRooms.Add(ro.room);
                }
            }

            bestRoomId = roomTimeTables.IndexOf(freeRooms[rand.Next(freeRooms.Count)].getTimeTable());

            return slot;
        }

        //ok
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
        //ok
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
        //ok
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
        //ok
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
        //ok
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
            this.value = value;
            return value;
        }
        //ok
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

            foreach (TimeTable tt in roomTimeTables)
            {
                tt.printSimpleTimeTable();
            }
        }
        //ok
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

            foreach (TimeTable tt in roomTimeTables)
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
                    //tt1.printTimeTable();
                    //tt2.printTimeTable();

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

            //this.printTimeTable();
            
        }
        //ok
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
        //ok
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
        //ok
        public List<TimeSlot> getTheSameTimeSlotInTheSameLesson(TimeTable tt1, TimeTable tt2) {
            List<TimeSlot> slots = new List<TimeSlot>();

            for (int d = 0; d < ConstVariable.NUMBER_OF_DAYS; d++) {
                for (int h = 0; h < ConstVariable.NUMBER_OF_SLOTS_IN_DAY; h++)
                {

                    if ((tt1.getLesson(d, h).Count > 0) && (tt2.getLesson(d, h).Count > 0))
                    {
                        //Console.Write("# : " + tt1.getLesson(d, h)[0].ToString() + "<->" + tt2.getLesson(d, h)[0].ToString());
                        if (tt1.getLesson(d, h)[0].getSubject().Equals(tt2.getLesson(d, h)[0].getSubject()))
                        {
                            //Console.WriteLine(" < OK");
                            slots.Add(new TimeSlot(d, h));
                        }
                        else {
                            //Console.WriteLine("");
                        }
                    }
                }
            }
            
            return slots;
        }

    }
}
