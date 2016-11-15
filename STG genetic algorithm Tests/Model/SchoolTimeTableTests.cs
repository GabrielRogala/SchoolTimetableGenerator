using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STG_genetic_algorithm.Model;
using STG_genetic_algorithm.Config;
using System.Collections.Generic;

namespace STG_genetic_algorithm_Tests.Model
{
    [TestClass]
    public class SchoolTimeTableTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<Lesson> lessons = new List<Lesson>();
            Teacher t = new Teacher(1,"t");
            Group g = new Group(1, "g");
            Subject s = new Subject(1,"s",SubjectType.HUM);

            lessons.Add(new Lesson(t,g,s));
        }

        [TestMethod] 
        public void TestGenerating()
        {
            List<Lesson> lessons = new List<Lesson>();
            List<Teacher> teachers = new List<Teacher>();
            List<Group> groups = new List<Group>();
            List<Subject> subjects = new List<Subject>();

           // Console.WriteLine("create 6 teachers");
            for (int j = 0; j < 7; j++)
            {
                teachers.Add(new Teacher(j, "t" + j));
            }
            //Console.WriteLine("create 6 groups");
            for (int j = 0; j < 6; j++)
            {
                groups.Add(new Group(j, "g" + j));
            }
            //Console.WriteLine("create 10 subject");
            int i = 0;
            subjects.Add(new Subject(i++, "pol", SubjectType.HUM));
            subjects.Add(new Subject(i++, "ang", SubjectType.JEZ));
            subjects.Add(new Subject(i++, "mat", SubjectType.SCI));
            subjects.Add(new Subject(i++, "his", SubjectType.HUM));
            subjects.Add(new Subject(i++, "wos", SubjectType.HUM));
            subjects.Add(new Subject(i++, "fiz", SubjectType.SCI));
            subjects.Add(new Subject(i++, "bio", SubjectType.SCI));
            subjects.Add(new Subject(i++, "geo", SubjectType.SCI));
            subjects.Add(new Subject(i++, "w-f", SubjectType.SPO));
            subjects.Add(new Subject(i++, "rel", SubjectType.INN));
            subjects.Add(new Subject(i++, "inf", SubjectType.SPE));
            subjects.Add(new Subject(i++, "PRO", SubjectType.SPE));

            Console.WriteLine("create list of lesson");
            foreach (Group g in groups)
            {
                int tI = 0;
                int sI = 0;
                int amount = 0;
                //max = 45
                //----------pol----------
                tI = 0; sI = 0; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI],amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount, 2));
                //----------ang----------
                tI = 1; sI = 1; amount = 4;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------mat----------
                tI = 2; sI = 2; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------his----------
                tI = 3; sI = 3; amount = 1;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------wos----------
                tI = 3; sI = 4; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------fiz----------
                tI = 5; sI = 5; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------bio----------
                tI = 3; sI = 6; amount = 1;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------geo----------
                tI = 3; sI = 7; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------w-f----------
                tI = 4; sI = 8; amount = 3;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------rel----------
                tI = 5; sI = 9; amount = 3;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------inf----------
                tI = 4; sI = 10; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------pro----------
                tI = 6; sI = 11; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount, 2));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount, 3));
                
            }

            SchoolTimeTable stt = new SchoolTimeTable(lessons, teachers, groups);

            stt.generateSchoolTimeTable();
            stt.printTimeTable();
            stt.timeTableCheckSume();
        }

        [TestMethod]
        public void TestSorting()
        {
            List<Lesson> lessons = new List<Lesson>();
            List<Teacher> teachers = new List<Teacher>();
            List<Group> groups = new List<Group>();
            List<Subject> subjects = new List<Subject>();

            // Console.WriteLine("create 6 teachers");
            for (int j = 0; j < 7; j++)
            {
                teachers.Add(new Teacher(j, "t" + j));
            }
            //Console.WriteLine("create 6 groups");
            for (int j = 0; j < 6; j++)
            {
                groups.Add(new Group(j, "g" + j));
            }
            //Console.WriteLine("create 10 subject");
            int i = 0;
            subjects.Add(new Subject(i++, "pol", SubjectType.HUM));
            subjects.Add(new Subject(i++, "ang", SubjectType.JEZ));
            subjects.Add(new Subject(i++, "mat", SubjectType.SCI));
            subjects.Add(new Subject(i++, "his", SubjectType.HUM));
            subjects.Add(new Subject(i++, "wos", SubjectType.HUM));
            subjects.Add(new Subject(i++, "fiz", SubjectType.SCI));
            subjects.Add(new Subject(i++, "bio", SubjectType.SCI));
            subjects.Add(new Subject(i++, "geo", SubjectType.SCI));
            subjects.Add(new Subject(i++, "w-f", SubjectType.SPO));
            subjects.Add(new Subject(i++, "rel", SubjectType.INN));
            subjects.Add(new Subject(i++, "inf", SubjectType.SPE));
            subjects.Add(new Subject(i++, "PRO", SubjectType.SPE));

            Console.WriteLine("create list of lesson");
            foreach (Group g in groups)
            {
                int tI = 0;
                int sI = 0;
                int amount = 0;
                //max = 45
                //----------pol----------
                tI = 0; sI = 0; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount, 2));
                //----------ang----------
                tI = 1; sI = 1; amount = 4;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------mat----------
                tI = 2; sI = 2; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------his----------
                tI = 3; sI = 3; amount = 1;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------wos----------
                tI = 3; sI = 4; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------fiz----------
                tI = 5; sI = 5; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------bio----------
                tI = 3; sI = 6; amount = 1;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------geo----------
                tI = 3; sI = 7; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------w-f----------
                tI = 4; sI = 8; amount = 3;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------rel----------
                tI = 5; sI = 9; amount = 3;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------inf----------
                tI = 4; sI = 10; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------pro----------
                tI = 6; sI = 11; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount, 2));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount, 3));

            }

            SchoolTimeTable stt = new SchoolTimeTable(lessons, teachers, groups);

            stt.sortLessonList(lessons);

            foreach(Lesson lesson in lessons) {
                Console.WriteLine(lesson.ToString());
            }
        }

        [TestMethod]
        public void shouldReturnMoreThenOneDifferentSubjectTheSameGroup()
        {
            List<Lesson> lessons = new List<Lesson>();
            List<Teacher> teachers = new List<Teacher>();
            List<Group> groups = new List<Group>();
            List<Subject> subjects = new List<Subject>();

            for (int j = 0; j < 7; j++) {
                teachers.Add(new Teacher(j, "t"+j));
            }
            
            for (int j = 0; j < 6; j++)
            {
                groups.Add(new Group(j, "g" + j));
            }
            
            int i = 0;
            subjects.Add(new Subject(i++, "pol", SubjectType.HUM));
            subjects.Add(new Subject(i++, "ang", SubjectType.JEZ));
            subjects.Add(new Subject(i++, "mat", SubjectType.SCI));
            subjects.Add(new Subject(i++, "his", SubjectType.HUM));
            subjects.Add(new Subject(i++, "wos", SubjectType.HUM));
            subjects.Add(new Subject(i++, "fiz", SubjectType.SCI));
            subjects.Add(new Subject(i++, "bio", SubjectType.SCI));
            subjects.Add(new Subject(i++, "geo", SubjectType.SCI));
            subjects.Add(new Subject(i++, "w-f", SubjectType.SPO));
            subjects.Add(new Subject(i++, "rel", SubjectType.INN));
            subjects.Add(new Subject(i++, "inf", SubjectType.SPE));
            subjects.Add(new Subject(i++, "PRO", SubjectType.SPE));


            foreach (Group g in groups) {
                int tI = 0;
                int sI = 0;
                //max = 45
                //----------pol----------
                tI = 0; sI = 0;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------ang----------
                tI = 1; sI = 1;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------mat----------
                tI = 2; sI = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------his----------
                tI = 3; sI = 3;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------wos----------
                tI = 3; sI = 4;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------fiz----------
                tI = 5; sI = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------bio----------
                tI = 3; sI = 6;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------geo----------
                tI = 3; sI = 7;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------w-f----------
                tI = 4; sI = 8;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------rel----------
                tI = 5; sI = 9;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------inf----------
                tI = 4; sI = 10;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                //----------pro----------
                tI = 6; sI = 11;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI]));
            }

            SchoolTimeTable stt = new SchoolTimeTable(lessons, teachers, groups);
            List<int> indexs = new List<int>();
            indexs.Add(0);
            indexs = stt.findDifferentSubjectTheSameGroup(indexs);
            if (ConstVariable.NUMBER_OF_LESSONS_TO_POSITIONING > 1)
            {
                Assert.IsTrue((indexs.Count > 1), "not found more then one different subject the same group");
            } else {
                Assert.IsTrue(indexs.Count > 0, "not found any different subject");
            }
        }
    }
}
