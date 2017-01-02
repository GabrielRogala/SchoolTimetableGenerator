using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STG_genetic_algorithm.Model;
using STG_genetic_algorithm.Config;
using System.Collections.Generic;

namespace STG_genetic_algorithm_Tests.Model
{
    [TestClass]
    public class PopulationTests
    {
        [TestMethod]
        public void TestGeneratingPopulation()
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

            Population population = new Population();
            population.generatePopulation(100,lessons,teachers,groups);
            //population.generateNewPopulation();
            population.start(500);

            SchoolTimeTable stt = population.getBestSchoolTimeTable();
            stt.printTimeTable();
        }


    }
}
