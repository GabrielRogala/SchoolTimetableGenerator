using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STG_genetic_algorithm.Model;

namespace STG_genetic_algorithm_Tests.Model
{
    [TestClass]
    public class TeacherTests
    {
        [TestMethod]
        public void shouldReturnNullTimeTableVariable()
        {
            Teacher teacher = new Teacher(0,"test");
            Assert.IsNull(teacher.getTimeTable(), "TimeTable variable is not empty");
        }
    }
}
