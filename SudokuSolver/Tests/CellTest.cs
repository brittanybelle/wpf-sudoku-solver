using NUnit.Framework;
using SudokuSolver.Models;

namespace SudokuSolver.Tests
{
    [TestFixture]
    public class CellTest
    {
        [Test]
        public void TestAllowedValuesOfCell()
        {
            int[] valuesToTest = new int[10] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            Assert.That(Cell.AllowedValues, Is.EquivalentTo(valuesToTest));
        }
    }
}
