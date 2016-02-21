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
    public class SolverTest
    {
        private Board testBoard;
        private Solver testSolver;

        public SolverTest()
        {
            testBoard = new Board();
            testSolver = new Solver();
        }

        public void TestBoardSolution(string inputString, string expectedOutput)
        {
            testBoard.UpdateBoard(InputParser.GetBoard(expectedOutput));
            Assert.That(testBoard.IsValid(), Is.EqualTo(true));
            testSolver.Solve(InputParser.GetBoard(inputString));
            Assert.That(testSolver.ActiveBoard.GetBoardStateAsIntArray(), Is.EqualTo(testBoard.GetBoardStateAsIntArray()));
        }
        
        [Test]
        public void TestBoardSolution_1()
        {
            string inputString =    "7.....2.....1..8..6...73.494.....3...5729.4...82.3..7.27.65......972......4..87..";
            string expectedOutput = "738945216945162837621873549496587321357291468182436975273659184819724653564318792";
            TestBoardSolution(inputString, expectedOutput);
        }

        [Test]
        public void TestBoardSolution_2()
        {
            string inputString =    "....8...14.....5...6...734.85.2....3..2.9.7..6....3.12.846...9...6.....53...1....";
            string expectedOutput = "793584621421369578568127349857241963132896754649753812284635197916478235375912486";
            TestBoardSolution(inputString, expectedOutput);
        }

        [Test]
        public void TestBoardSolution_3()
        {
            string inputString =    ".....7.4.9..8..5.7..3.4.9....1.9.254.........695.2.7....9.7.6..5.6..3..9.1.2.....";
            string expectedOutput = "168957342942831567753642918381796254274385196695124783439578621526413879817269435";
            TestBoardSolution(inputString, expectedOutput);
        }

        [Test]
        public void TestBoardSolution_4()
        {
            string inputString =    "3...9.......5.2.3..6...1..2....8..79..3.6.8..92..1....2..3...6..5.8.4.......5...8";
            string expectedOutput = "312698547897542631465731982546283179173965824928417356281379465659824713734156298";
            TestBoardSolution(inputString, expectedOutput);
        }

    }
}
