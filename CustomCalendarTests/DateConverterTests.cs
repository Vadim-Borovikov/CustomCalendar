using System;
using CustomCalendar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomCalendarTests
{
    [TestClass]
    public class DateConverterTests
    {
        [TestMethod]
        public void ConvertStandardTest()
        {
            var dateStandard = new DateTime(1985, 2, 1);
            DateCustom dateCustom = DateConverter.ConvertStandard(dateStandard);
            Assert.IsTrue(dateCustom.Year == 11985);
            Assert.IsTrue(dateCustom.Month == 2);
            Assert.IsTrue(dateCustom.Day == 19);
            Assert.IsTrue(dateCustom.DayOfWeek == DayOfWeekCustom.Wednesday);

            dateStandard = new DateTime(1985, 10, 28);
            Console.WriteLine(DateConverter.ConvertStandard(dateStandard));
        }

        [TestMethod]
        public void ConvertCustomTest()
        {
            var dateCustom = new DateCustom(7, 2, 12016);
            DateTime dateStandard = DateConverter.ConvertCustom(dateCustom);
            Assert.IsTrue(dateStandard.Year == 2016);
            Assert.IsTrue(dateStandard.Month == 1);
            Assert.IsTrue(dateStandard.Day == 21);
            Assert.IsTrue(dateStandard.DayOfWeek == DayOfWeek.Thursday);

            dateCustom = new DateCustom(28, 10, 11985);
            Console.WriteLine(DateConverter.ConvertCustom(dateCustom));
        }
    }
}