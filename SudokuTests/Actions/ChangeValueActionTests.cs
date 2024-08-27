using Sudoku.Game.Actions;
using Sudoku.Game.Model;
using Sudoku.Game.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTests.Actions
{
    internal class ChangeValueActionTests
    {
        [Test]
        public void ChangeValueAction_DoAndUndo_ValuesAreChangedCorrectly()
        {
            // Prepare
            var startValue = 7;
            var targetValue = 5;
            var square = new SudokuSquare(startValue, false);
            var list = new List<SudokuSquare> { square };

            // Run
            var action = new ChangeValueAction(list, targetValue) as IUndoableAction;
            action.Do();

            // Check
            Assert.That(targetValue, Is.EqualTo(square.Number));

            // Undo
            action.Undo();

            // Check
            Assert.That(startValue, Is.EqualTo(square.Number));
        }
    }
}
