using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using SudokuSolver.Models;

namespace SudokuSolver.Tests
{
    [TestFixture]
    public class BoardTest
    {
        private Board testBoard = new Board();

        public BoardTest()
        {
            string inputString = "4.....8.5.3..........7......2.....6.....8.4......1.......6.3.7.5..2.....1.4......";
            testBoard.UpdateBoard(InputParser.GetBoard(inputString));
        }

        [Test]
        public void TestBoardSize()
        {
            Assert.That(testBoard.Cells.Count, Is.EqualTo(9));
            Assert.That(testBoard.Cells[8].Count, Is.EqualTo(9));
        }

        [Test]
        public void TestBoardInput()
        {
            Assert.That(testBoard.Cells[0][0].Content, Is.EqualTo(4));
            Assert.That(testBoard.Cells[0][6].Content, Is.EqualTo(8));
            Assert.That(testBoard.Cells[0][8].Content, Is.EqualTo(5));
            Assert.That(testBoard.Cells[1][1].Content, Is.EqualTo(3));
            Assert.That(testBoard.Cells[2][3].Content, Is.EqualTo(7));
            Assert.That(testBoard.Cells[3][1].Content, Is.EqualTo(2));
            Assert.That(testBoard.Cells[3][7].Content, Is.EqualTo(6));
            Assert.That(testBoard.Cells[4][4].Content, Is.EqualTo(8));
            Assert.That(testBoard.Cells[4][6].Content, Is.EqualTo(4));
            Assert.That(testBoard.Cells[5][4].Content, Is.EqualTo(1));
            Assert.That(testBoard.Cells[6][3].Content, Is.EqualTo(6));
            Assert.That(testBoard.Cells[6][5].Content, Is.EqualTo(3));
            Assert.That(testBoard.Cells[6][7].Content, Is.EqualTo(7));
            Assert.That(testBoard.Cells[7][0].Content, Is.EqualTo(5));
            Assert.That(testBoard.Cells[7][3].Content, Is.EqualTo(2));
            Assert.That(testBoard.Cells[8][0].Content, Is.EqualTo(1));
            Assert.That(testBoard.Cells[8][2].Content, Is.EqualTo(4));
        }
    }
}
