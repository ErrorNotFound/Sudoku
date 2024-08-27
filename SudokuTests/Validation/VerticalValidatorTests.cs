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
    internal class VerticalValidatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VerticalValidator_ValidGame_ReturnsTrue() 
        {
            // Prepare
            var values = new int[,] { 
                { 1,1,1,1,1,1,1,1,1},
                { 2,2,2,2,2,2,2,2,2},
                { 3,3,3,3,3,3,3,3,3},
                { 4,4,4,4,4,4,4,4,4},
                { 5,5,5,5,5,5,5,5,5},
                { 6,6,6,6,6,6,6,6,6},
                { 7,7,7,7,7,7,7,7,7},
                { 8,8,8,8,8,8,8,8,8},
                { 9,9,9,9,9,9,9,9,9}            };
            var field = SudokuBoardBuilder.GetFieldFromValues(values);

            // Run
            var validator = new VerticalValidator();
            var result = validator.IsValid(field, out List<SudokuSquare> invalids);

            // Check
            Assert.IsTrue(result);
        }

        [Test]
        public void VerticalValidator_InvalidGame_ReturnsFalse()
        {
            // Prepare
            var values = new int[,] {
                { 1,1,1,1,1,1,1,1,1},
                { 1,2,2,2,2,2,2,2,2},
                { 3,3,3,3,3,3,3,3,3},
                { 4,4,4,4,4,4,4,4,4},
                { 5,5,5,5,5,5,5,5,5},
                { 6,6,6,6,6,6,6,6,6},
                { 7,7,7,7,7,7,7,7,7},
                { 8,8,8,8,8,8,8,8,8},
                { 9,9,9,9,9,9,9,9,9}
            };
            var field = SudokuBoardBuilder.GetFieldFromValues(values);

            // Run
            var validator = new VerticalValidator();
            var result = validator.IsValid(field, out List<SudokuSquare> invalids);

            // Check
            Assert.IsFalse(result);
        }
    }
}
