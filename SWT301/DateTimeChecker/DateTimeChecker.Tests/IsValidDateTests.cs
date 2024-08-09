using NUnit.Framework;

namespace DateTimeChecker.Tests
{
    [TestFixture]
    public class IsValidDateTests
    {
        [Test]
        public void ValidDateInLeapYear()
        {
            bool result = DateTimeChecker.IsValidDate(2024, 2, 29);
            Assert.IsTrue(result);
        }

        [Test]
        public void InvalidDateInNonLeapYear()
        {
            bool result = DateTimeChecker.IsValidDate(2023, 2, 29);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidDateEndOfMonth()
        {
            bool result = DateTimeChecker.IsValidDate(2024, 12, 31);
            Assert.IsTrue(result);
        }

        [Test]
        public void InvalidDateInApril()
        {
            bool result = DateTimeChecker.IsValidDate(2024, 4, 31);
            Assert.IsFalse(result);
        }

        [Test]
        public void InvalidMonth()
        {
            bool result = DateTimeChecker.IsValidDate(2024, 13, 1);
            Assert.IsFalse(result);
        }

        [Test]
        public void InvalidDay()
        {
            bool result = DateTimeChecker.IsValidDate(2024, 2, 0);
            Assert.IsFalse(result);
        }
    }
}
