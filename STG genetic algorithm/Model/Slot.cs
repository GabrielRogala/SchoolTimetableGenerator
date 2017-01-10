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
        private bool blocked;

        public bool isEmpty()
        {
            return lessons.Count == 0;
        }

        public Slot(int id)
        {
            this.id = id;
            blocked = false;
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

        public void lockSlot() {
            blocked = true;
        }

        public void unlockSlot() {
            blocked = false;
        }

        public bool isBlocked() {
            return blocked;
        }
    }
}
