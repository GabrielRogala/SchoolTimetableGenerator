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

        public Lesson(Teacher t, Group g, Subject s)
        {
            teacher = t;
            group = g;
            subject = s;
            amount = 0;
        }

        public Lesson(Teacher t, Group g, Subject s, int amount) :this(t,g,s)
        {
            this.amount = amount;
        }

        public string ToString()
        {
            return group.ToString() + "/" + teacher.ToString() + "/" + subject.ToString()+ "["+amount+"]";
        }

        public Boolean Equals(Lesson lesson)
        {
            return this.subject.Equals(lesson.subject);
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
    }
}
