using Newtonsoft.Json.Linq;
using Sudoku.Game.Helper;
using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SudokuTests.Model
{
    internal class SudokuBoardTests
    {
        [Test]
        public void SudokuBoard_GetSimplifiedCopy_CopyIsIdentical()
        {
            // Prepare
            var exampleBoard = new int[,] {
                { 1,2,3,1,2,3,1,2,3},
                { 4,5,6,4,5,6,4,5,6},
                { 7,8,9,7,8,9,7,8,9},
                { 1,2,3,1,2,3,1,2,3},
                { 4,5,6,4,5,6,4,5,6},
                { 7,8,9,7,8,9,7,8,9},
                { 1,2,3,1,2,3,1,2,3},
                { 4,5,6,4,5,6,4,5,6},
                { 7,8,9,7,8,9,7,8,9}
            };
            var board = SudokuBoardBuilder.GetFieldFromValues(exampleBoard);
            var originStr = board.ToString();

            // Run
            var copy = board.GetSimplifiedCopy();
            var copyStr = copy.ToString();

            // Check
            Assert.That(copyStr, Is.EqualTo(originStr));
        }

        /*
        [Test]
        public void SudokuBoard__Temp()
        {
            var temp = "295743861431865900876192543387459216612387495549216738763524189928671354154938600";
            var board = new SudokuBoard(temp);

            //var a = SudokuAlgorithms.HasMultipleSolutionsParallel(board);
        }*/

        [Test]
        public void PersistanceHelper_SaveAndLoadSudoku_FieldsAreIdentical()
        {
            var board = SudokuBoardBuilder.GetEasyExampleField();
            var filename = nameof(PersistanceHelper_SaveAndLoadSudoku_FieldsAreIdentical) + ".xml";

            board.Save(filename);
            var loaded = SudokuBoard.Load(filename);

            Assert.That(loaded, Is.Not.Null);
            Assert.That(board.ToString(), Is.EqualTo(loaded.ToString()));
        }
    }
}
