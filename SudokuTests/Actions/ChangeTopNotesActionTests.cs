using Sudoku.Game.Actions;
using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTests.Actions
{
    internal class ChangeTopNotesActionTests
    {
        [Test]
        public void ChangeTopNotesAction_NoSimiliarNoteSet_NoteIsAddedToAllSquares()
        {
            // Prepare
            int addedNote = 1;
            var a = new SudokuSquare();
            var b = new SudokuSquare();
            var c = new SudokuSquare();
            var list = new List<SudokuSquare> { a,b,c };

            // Run
            var action = new ChangeTopNotesAction(list, addedNote) as IUndoableAction;
            action.Do();

            // Check
            Assert.IsTrue(list.All(x => x.TopNotes.Contains(addedNote)));

            // Undo
            action.Undo();

            // Check
            Assert.IsTrue(list.All(x => !x.TopNotes.Contains(addedNote)));
        }

        [Test]
        public void ChangeTopNotesAction_OneSimiliarNoteAlreadySet_NoteIsAddedToAllSquares()
        {
            // Prepare
            int addedNote = 1;
            var a = new SudokuSquare();
            a.TopNotes.Add(addedNote);
            var b = new SudokuSquare();
            var c = new SudokuSquare();
            var list = new List<SudokuSquare> { a, b, c };

            // Run
            var action = new ChangeTopNotesAction(list, addedNote) as IUndoableAction;
            action.Do();

            // Check
            Assert.IsTrue(list.All(x => x.TopNotes.Contains(addedNote)));

            // Undo
            action.Undo();

            // Check
            Assert.IsTrue(list.Count(x => x.TopNotes.Contains(addedNote)) == 1);
        }

        [Test]
        public void ChangeTopNotesAction_AllSquaresHaveNote_NoteIsRemovedFromAllSquares()
        {
            // Prepare
            int addedNote = 1;
            var a = new SudokuSquare();
            a.TopNotes.Add(addedNote);
            var b = new SudokuSquare();
            b.TopNotes.Add(addedNote);
            var c = new SudokuSquare();
            c.TopNotes.Add(addedNote);
            var list = new List<SudokuSquare> { a, b, c };

            // Run
            var action = new ChangeTopNotesAction(list, addedNote) as IUndoableAction;
            action.Do();

            // Check
            Assert.IsTrue(list.All(x => !x.TopNotes.Contains(addedNote)));

            // Undo
            action.Undo();

            // Check
            Assert.IsTrue(list.All(x => x.TopNotes.Contains(addedNote)));
        }

        [Test]
        public void ChangeTopNotesAction_TargetNoteIsNull_AllNotesAreRemoved()
        {
            // Prepare
            int initialNote = 1;
            var a = new SudokuSquare();
            a.TopNotes.Add(initialNote);
            var b = new SudokuSquare();
            b.TopNotes.Add(initialNote);
            var c = new SudokuSquare();
            c.TopNotes.Add(initialNote);
            var list = new List<SudokuSquare> { a, b, c };

            // Run
            var action = new ChangeTopNotesAction(list, null) as IUndoableAction;
            action.Do();

            // Check
            Assert.IsTrue(list.All(x => !x.TopNotes.Any()));

            // Undo
            action.Undo();

            // Check
            Assert.IsTrue(list.All(x => x.TopNotes.Contains(initialNote)));
        }

    }
}
