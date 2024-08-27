using Sudoku.Game.Helper;
using Sudoku.Game.Model;
using Sudoku.Game.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTests.Validation
{
    internal class AllSquaresSetValidatorTests
    {
        [Test]
        public void AllSquaresSetValidator_AllSquaresHaveValue_ReturnsTrue()
        {
            // Prepare
            var values = new int[,] {
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
            var board = SudokuBoardBuilder.GetFieldFromValues(values);

            // Run
            var validator = new AllSquaresSetValidator();
            var result = validator.IsValid(board, out List<SudokuSquare> invalids);

            // Check
            Assert.IsTrue(result);
        }

        [Test]
        public void AllSquaresSetValidator_SomeSquaresHaveNoValue_ReturnsFalse()
        {
            // Prepare
            var values = new int[,] {
                { 1,2,0,1,2,3,1,2,3},
                { 4,5,6,4,5,6,4,5,6},
                { 7,9,9,7,8,9,7,8,9},
                { 1,2,3,1,2,3,1,2,3},
                { 4,5,6,4,5,6,4,5,6},
                { 7,8,9,7,8,9,7,8,9},
                { 1,2,3,1,2,3,1,2,3},
                { 4,5,6,4,5,6,4,5,6},
                { 7,8,9,7,8,9,7,8,9}
            };
            var field = SudokuBoardBuilder.GetFieldFromValues(values);

            // Run
            var validator = new AllSquaresSetValidator();
            var result = validator.IsValid(field, out List<SudokuSquare> invalids);

            // Check
            Assert.IsFalse(result);
        }
    }
}
