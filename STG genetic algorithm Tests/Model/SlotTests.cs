using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STG_genetic_algorithm.Model;
using STG_genetic_algorithm.Config;

namespace STG_genetic_algorithm_Tests.Model
{
    [TestClass]
    public class SlotTests
    {
        [TestMethod]
        public void shouldReturnFalseIfLessonListIsNotEmpty()
        {
            Slot slot = new Slot(0);
            Teacher t = new Teacher(0, "t");
            Group g = new Group(0, "g");
            Subject s = new Subject(1,"s",SubjectType.HUM);
            Lesson lesson = new Lesson(t,g,s);
            slot.addLesson(lesson);

            Assert.IsFalse(slot.isEmpty(),"List of lessons in this slot is empty");
        }

        [TestMethod]
        public void shouldReturnTrueIfRemoveAllLesson()
        {
            Slot slot = new Slot(0);
            Teacher t = new Teacher(0, "t");
            Group g = new Group(0, "g");
            Subject s = new Subject(1, "s", SubjectType.HUM);
            Lesson lesson1 = new Lesson(t, g, s);
            Lesson lesson2 = new Lesson(t, g, s);
            slot.addLesson(lesson1);
            slot.addLesson(lesson2);

            slot.removeLesson(lesson1);
            slot.removeLesson(lesson2);

            Assert.IsTrue(slot.isEmpty(), "not remove lesson");
        }
    }
}
