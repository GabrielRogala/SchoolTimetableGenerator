using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class Lesson
    {
        private Teacher teacher;
        private Group group;
        private Subject subject;
        private int amount;
        private int size;
        private List<TimeSlot> timeSlots;
        private Room room;

        public Lesson(Teacher t, Group g, Subject s)
        {
            teacher = t;
            group = g;
            subject = s;
            amount = 1;
            size = 1;
            timeSlots = new List<TimeSlot>();
            room = null;
        }

        public Lesson(Teacher t, Group g, Subject s, int amount) :this(t,g,s)
        {
            this.amount = amount;
            this.size = 1;
        }

        public Lesson(Teacher t, Group g, Subject s, int amount, int size) : this(t, g, s, amount)
        {
            this.size = size;
        }

        public void setRoom(Room room) {
            this.room = room;
        }

        public Room getRoom() {
            return room;
        }

        public string ToString()
        {
            String roomNo = "";
            if (room != null) {
                roomNo = room.name;
            }
            return group.ToString() + "/" + teacher.ToString() + "/" + subject.ToString()+ "["+size+":"+amount+"]"+ roomNo;
        }

        public Boolean Equals(Lesson lesson)
        {
            return this.subject.Equals(lesson.subject) && this.group.Equals(lesson.group) && this.teacher.Equals(lesson.teacher) && this.amount.Equals(lesson.amount) && this.size.Equals(lesson.size);
        }

        public Subject getSubject()
        {
            return subject;
        }

        public Teacher getTeacher() {
            return teacher;
        }

        public Group getGroup() {
            return group;
        }

        public bool isDifferent(Lesson lesson)
        {
            return !this.subject.Equals(lesson.getSubject());
        }

        public int getSize() {
            return size;
        }

        public void increaseAmount()
        {
            if (amount < 0)
            {
                amount = 0;
            }
            amount++;
        }

        public void decreaseAmount()
        {
            if (amount > 0)
            {
                amount--;
            }
        }

        public void setAmount(int amount)
        {
            this.amount = amount;
        }

        public int getAmount()
        {
            return amount;
        }

        public void setTimeSlots(List<TimeSlot> list) {
            this.timeSlots = list;
        }

        public void addTimeSlot(TimeSlot slot) {
            timeSlots.Add(slot);
        }

        public void clearTimeSlots() {
            timeSlots.Clear();
        }

        public void removeTimeSlot(TimeSlot slot) {
            timeSlots.Remove(slot);
        }
    }
}
