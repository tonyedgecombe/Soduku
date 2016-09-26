using System.Linq;
using NUnit.Framework;

namespace Sudoku.Tests
{
    [TestFixture]
    public class TestProgram
    {
        [Test]
        public void TestIsSequenceValid()
        {
            Assert.That(new Program().IsSequenceValid(new []{0, 0, 0, 0, 0, 0, 0, 0, 0}), Is.True);
            Assert.That(new Program().IsSequenceValid(new []{1, 2, 3, 4, 5, 6, 7, 8, 9}), Is.True);
            Assert.That(new Program().IsSequenceValid(new []{1, 2, 3, 4, 5, 6, 7, 9, 9}), Is.False);
        }

        [Test]
        public void TestGetRow()
        {
            var arr = Enumerable.Range(0, 81).ToArray();

            Assert.That(new Program().GetRow(arr, 0), Is.EqualTo(Enumerable.Range(0, 9).ToArray()));
            Assert.That(new Program().GetRow(arr, 1), Is.EqualTo(Enumerable.Range(9, 9).ToArray()));
            Assert.That(new Program().GetRow(arr, 8), Is.EqualTo(Enumerable.Range(72, 9).ToArray()));
        }

        [Test]
        public void TestGetColumn()
        {
            var arr = Enumerable.Range(0, 81).ToArray();
            arr[0] = 1;
            Assert.That(new Program().GetColumn(arr, 0), Is.EqualTo(new []{1, 9, 18, 27, 36, 45, 54, 63, 72}));
        }

        [Test]
        public void TestAreRowsValid()
        {
            var arr = new int[81];

            Assert.That(new Program().AreRowsValid(arr), Is.True);
            Enumerable.Range(0, 9).ToArray().CopyTo(arr, 0);

            Assert.That(new Program().AreRowsValid(arr), Is.True);

            arr[0] = 1;
            Assert.That(new Program().AreRowsValid(arr), Is.False);
        }

        [Test]
        public void TestIsRowValid()
        {
            var arr = new int[81];

            Assert.That(new Program().IsRowValid(arr, 0), Is.True);
            Enumerable.Range(0, 9).ToArray().CopyTo(arr, 0);

            arr[0] = 1;
            Assert.That(new Program().IsRowValid(arr, 0), Is.False);
        }

        [Test]
        public void TestIsColumnValid()
        {
            var arr = Enumerable.Range(0, 81).ToArray();
            Assert.That(new Program().IsColumnValid(arr, 0), Is.True);

            arr[0] = 9;
            Assert.That(new Program().IsColumnValid(arr, 0), Is.False);
        }

        [Test]
        public void TestAreColumnsValid()
        {
            var arr = Enumerable.Range(0, 81).ToArray();

            Assert.That(new Program().AreColumnsValid(arr), Is.True);

            arr[0] = 9;
            Assert.That(new Program().AreColumnsValid(arr), Is.False);
        }

        [Test]
        public void TestGetNextTestCell()
        {
            var arr = new int[81];
            Assert.That(new Program().GetNextTestCell(arr), Is.EqualTo(0));

            Enumerable.Range(0, 27).Select(c => 1).ToArray().CopyTo(arr, 0);
            Assert.That(new Program().GetNextTestCell(arr), Is.EqualTo(27));

            Enumerable.Range(0, 81).Select(c => 1).ToArray().CopyTo(arr, 0);
            Assert.That(new Program().GetNextTestCell(arr), Is.EqualTo(81));
        }

        [Test]
        public void TestGetBlock()
        {
            var arr = Enumerable.Range(0, 81).ToArray();

            Assert.That(new Program().GetBlock(arr, 0, 0), Is.EqualTo(new[] {0, 1, 2, 9, 10, 11, 18, 19, 20}));
            Assert.That(new Program().GetBlock(arr, 0, 1), Is.EqualTo(new[] {3, 4, 5, 12, 13, 14, 21, 22, 23}));
            Assert.That(new Program().GetBlock(arr, 0, 2), Is.EqualTo(new[] {6, 7, 8, 15, 16, 17, 24, 25, 26}));

            Assert.That(new Program().GetBlock(arr, 1, 0), Is.EqualTo(new[] {27, 28, 29, 36, 37, 38, 45, 46, 47}));
            Assert.That(new Program().GetBlock(arr, 1, 1), Is.EqualTo(new[] {30, 31, 32, 39, 40, 41, 48, 49, 50}));
            Assert.That(new Program().GetBlock(arr, 1, 2), Is.EqualTo(new[] {33, 34, 35, 42, 43, 44, 51, 52, 53}));

            Assert.That(new Program().GetBlock(arr, 2, 0), Is.EqualTo(new[] {54, 55, 56, 63, 64, 65, 72, 73, 74}));
            Assert.That(new Program().GetBlock(arr, 2, 1), Is.EqualTo(new[] {57, 58, 59, 66, 67, 68, 75, 76, 77}));
            Assert.That(new Program().GetBlock(arr, 2, 2), Is.EqualTo(new[] {60, 61, 62, 69, 70, 71, 78, 79, 80}));
        }

        [Test]
        public void TestIsBlockValid()
        {
            for (int rb = 0; rb < 3; rb++)
            {
                for (int cb = 0; cb < 3; cb++)
                {
                    var arr = new int[81];

                    var pos = rb*3*9 + cb*3;
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            arr[pos + x*3 + y] = x*3 + y;
                        }
                    }

                    Assert.That(new Program().IsBlockValid(arr, rb, cb), Is.True);
                    arr[pos] = 2;
                    Assert.That(new Program().IsBlockValid(arr, rb, cb), Is.False);
                }
            }
        }

        [Test]
        public void TestAreBlocksValid()
        {
            var arr = Enumerable.Range(0, 81).ToArray();

            Assert.That(new Program().AreBlocksValid(arr), Is.True);

            arr[0] = 1;
            Assert.That(new Program().AreBlocksValid(arr), Is.False);
        }
        [Test]
        public void TestIsValid()
        {
            var arr = Enumerable.Range(0, 81).ToArray();

            Assert.That(new Program().IsValid(arr), Is.True);

            arr[0] = 1;
            Assert.That(new Program().IsValid(arr), Is.False);
        }
    }
}
