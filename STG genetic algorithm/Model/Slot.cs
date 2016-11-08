using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class Slot
    {
        private int id;
        private List<Lesson> lessons;

        public bool isEmpty()
        {
            return lessons.Count == 0;
        }

        public Slot(int id)
        {
            this.id = id;
            lessons = new List<Lesson>();
        }

        public void addLesson(Lesson lesson)
        {
            lessons.Add(lesson);
        }

        public void removeLesson(Lesson lesson)
        {
            lessons.Remove(lesson);
        }

        public Lesson getLesson(int i)
        {
            return lessons[i];
        }

        public List<Lesson> getLessons()
        {
            return lessons;
        }
    }
}
