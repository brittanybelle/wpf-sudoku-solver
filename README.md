# wpf-sudoku-solver
A visual sudoku solver in C#, using the WPF framework.

## Installation/Setup
Simply download this repo and open the SudokuSolver solution file in Visual Studio.

### Compatibility
This application was developed and tested in Visual Studio 2015 (Community Edition), targeting .NET 4.5.

### Testing
[NUnit 3.0](http://www.nunit.org/) is used to perform some basic unit tests. Before building the solution, you may want to check that the [NUnit NuGet package](https://www.nuget.org/packages/NUnit/) is installed.

## Usage
To use the program, first set the initial state of the board on the left side of the application window. You can enter the solution manually using the grid of input boxes, or you can load a file. Once the input is specified, you simply need to click the 'Solve' button and the solver algorithm will take over.

### Loading a Problem File
The program will accept files with a variety of input formats. As long as the solution file is encoded with UTF-8 or ASCII and has an extension of **.txt** or **.csv**, the application will load the file and attempt to parse it. When parsing, the program looks for digits `1-9`, which it interprets as cell values, and the characters `0` and `.` which it interprets as a blank cell.

File loading may fail if the input format is ambiguous. In particular, the parser will check that the sum of all characters in the set `[0-9, .]` is exactly equal to 81; if this is not true then the file is rejected.

As long as the file format appears to be valid, the input will also be parsed to ensure that the submitted board is legal. Illegal boards are those that include the same number twice in a single row/column/3x3-square. Note that an *unsolvable* but legal board will still be accepted as valid input.

### Sample Valid File Input Formats
Example 1:
```
4 . . | . . . | 8 . 5
. 3 . | . . . | . . .
. . . | 7 . . | . . .
------+-------+------
. 2 . | . . . | . 6 .
. . . | . 8 . | 4 . .
. . . | . 1 . | . . .
------+-------+------
. . . | 6 . 3 | . 7 .
5 . . | 2 . . | . . .
1 . 4 | . . . | . . .
```
Example 2:
```
400000805
030000000
000700000
020000060
000080400
000010000
000603070
500200000
104000000
```
Example 3:
```
4.....8.5.3..........7......2.....6.....8.4......1.......6.3.7.5..2.....1.4......
```
