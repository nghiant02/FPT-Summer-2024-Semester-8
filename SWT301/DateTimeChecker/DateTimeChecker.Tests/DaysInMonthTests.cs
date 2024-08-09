using NUnit.Framework;

namespace DateTimeChecker.Tests
{
    [TestFixture]
    public class DaysInMonthTests
    {
        [Test]
        public void DaysInJanuary()
        {
            int result = DateTimeChecker.DaysInMonth(2024, 1);
            Assert.AreEqual(31, result);
        }

        [Test]
        public void DaysInFebruaryLeapYear()
        {
            int result = DateTimeChecker.DaysInMonth(2024, 2);
            Assert.AreEqual(29, result);
        }

        [Test]
        public void DaysInFebruaryNonLeapYear()
        {
            int result = DateTimeChecker.DaysInMonth(2023, 2);
            Assert.AreEqual(28, result);
        }

        [Test]
        public void DaysInApril()
        {
            int result = DateTimeChecker.DaysInMonth(2024, 4);
            Assert.AreEqual(30, result);
        }

        [Test]
        public void InvalidMonth()
        {
            int result = DateTimeChecker.DaysInMonth(2024, 13);
            Assert.AreEqual(0, result);
        }
    }
}
