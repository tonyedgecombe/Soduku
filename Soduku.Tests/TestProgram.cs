using System.Linq;
using NUnit.Framework;
using Sodukuk;

namespace Soduku.Tests
{
    [TestFixture]
    public class TestProgram
    {
        [Test]
        public void TestIsSequenceValid()
        {
            Assert.That(Program.IsSequenceValid(new []{0, 0, 0, 0, 0, 0, 0, 0, 0}), Is.True);
            Assert.That(Program.IsSequenceValid(new []{1, 2, 3, 4, 5, 6, 7, 8, 9}), Is.True);
            Assert.That(Program.IsSequenceValid(new []{1, 2, 3, 4, 5, 6, 7, 9, 9}), Is.False);
        }

        [Test]
        public void TestGetRow()
        {
            var arr = Enumerable.Range(0, 81).ToArray();

            Assert.That(Program.GetRow(arr, 0), Is.EqualTo(Enumerable.Range(0, 9).ToArray()));
            Assert.That(Program.GetRow(arr, 1), Is.EqualTo(Enumerable.Range(9, 9).ToArray()));
            Assert.That(Program.GetRow(arr, 8), Is.EqualTo(Enumerable.Range(72, 9).ToArray()));
        }

        [Test]
        public void TestGetColumn()
        {
            var arr = Enumerable.Range(0, 81).ToArray();
            arr[0] = 1;
            Assert.That(Program.GetColumn(arr, 0), Is.EqualTo(new []{1, 9, 18, 27, 36, 45, 54, 63, 72}));
        }

        [Test]
        public void TestAreRowsValid()
        {
            var arr = new int[81];

            Assert.That(Program.AreRowsValid(arr), Is.True);
            Enumerable.Range(0, 9).ToArray().CopyTo(arr, 0);

            Assert.That(Program.AreRowsValid(arr), Is.True);

            arr[0] = 1;
            Assert.That(Program.AreRowsValid(arr), Is.False);
        }

        [Test]
        public void TestIsRowValid()
        {
            var arr = new int[81];

            Assert.That(Program.IsRowValid(arr, 0), Is.True);
            Enumerable.Range(0, 9).ToArray().CopyTo(arr, 0);

            arr[0] = 1;
            Assert.That(Program.IsRowValid(arr, 0), Is.False);
        }

        [Test]
        public void TestIsColumnValid()
        {
            var arr = Enumerable.Range(0, 81).ToArray();
            Assert.That(Program.IsColumnValid(arr, 0), Is.True);

            arr[0] = 9;
            Assert.That(Program.IsColumnValid(arr, 0), Is.False);
        }
    }
}
