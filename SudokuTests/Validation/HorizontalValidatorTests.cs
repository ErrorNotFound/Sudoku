using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Game.Helper;
using Sudoku.Game.Model;
using Sudoku.Game.Validation;


namespace SudokuTests.Validation
{
    internal class HorizontalValidatorTests
    {
        [Test]
        public void HorizontalValidator_ValidGame_ReturnsTrue() 
        {
            // Prepare
            var values = new int[,] {
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9}
            };
            var field = SudokuBoardBuilder.GetFieldFromValues(values);

            // Run
            var validator = new HorizontalValidator();
            var result = validator.IsValid(field, out List<SudokuSquare> invalids);

            // Check
            Assert.IsTrue(result);
        }

        [Test]
        public void HorizontalValidator_InvalidGame_ReturnsFalse()
        {
            // Prepare
            var values = new int[,] {
                { 1,1,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9},
                { 1,2,3,4,5,6,7,8,9}
            };
            var field = SudokuBoardBuilder.GetFieldFromValues(values);

            // Run
            var validator = new HorizontalValidator();
            var result = validator.IsValid(field, out List<SudokuSquare> invalids);

            // Check
            Assert.IsFalse(result);
        }
    }
}
