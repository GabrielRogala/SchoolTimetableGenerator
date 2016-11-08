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

        public Lesson(Teacher t, Group g, Subject s)
        {
            teacher = t;
            group = g;
            subject = s;
        }

        public string ToString()
        {
            return group.ToString() + "/" + teacher.ToString() + "/" + subject.ToString();
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

        internal bool isDifferent(Lesson lesson)
        {
            return !this.subject.Equals(lesson.getSubject());
        }
    }
}
