using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STG_genetic_algorithm.Model;
using STG_genetic_algorithm.Config;

namespace STG_genetic_algorithm_Tests.Model
{
    [TestClass]
    public class DayTests
    {
        [TestMethod]
        public void shouldCreateCorrectSlotsCount()
        {
            Day day = new Day("a");
            int count = day.getSlots().Count;
            Assert.AreEqual(ConstVariable.NUMBER_OF_SLOTS_IN_DAY,count,0,"Create not correct number of slots in day");
        }
    }
}
