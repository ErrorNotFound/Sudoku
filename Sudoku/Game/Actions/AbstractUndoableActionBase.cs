using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Actions
{
    public abstract class AbstractUndoableActionBase : IUndoableAction
    {
        protected Dictionary<SudokuSquare, int?> HistoryNumbers;
        protected Dictionary<SudokuSquare, List<int>> HistoryTopNotes;
        protected Dictionary<SudokuSquare, List<int>> HistoryCenterNotes;

        private bool actionPerformed;

        public AbstractUndoableActionBase(IEnumerable<SudokuSquare> squares)
        {
            HistoryNumbers = squares.Where(x => !x.IsFixed).ToDictionary(x => x, x => x.Number);
            HistoryTopNotes = squares.Where(x => !x.IsFixed).ToDictionary(x => x, x => x.TopNotes.ToList());
            HistoryCenterNotes = squares.Where(x => !x.IsFixed).ToDictionary(x => x, x => x.CenterNotes.ToList());
        }

        protected abstract void OnDo();
        protected abstract bool OnIsValidOperation();
        protected virtual void OnUndo() { }

        public void Do()
        {
            if (!actionPerformed)
            {
                OnDo();
                actionPerformed = true;
            }
            else
                throw new InvalidOperationException("Action already performed");
        }

        public bool IsValidOperation()
        {
            return OnIsValidOperation();
        }

        public void Undo()
        {
            if (actionPerformed)
            {
                // reset numbers
                foreach (var square in HistoryNumbers)
                {
                    square.Key.Number = square.Value;
                }

                // reset topnotes
                foreach (var square in HistoryTopNotes)
                {
                    square.Key.TopNotes.Clear();

                    foreach (var note in square.Value)
                    {
                        square.Key.TopNotes.Add(note);
                    }
                }

                // reset centernotes
                foreach (var square in HistoryCenterNotes)
                {
                    square.Key.CenterNotes.Clear();

                    foreach (var note in square.Value)
                    {
                        square.Key.CenterNotes.Add(note);
                    }
                }

                OnUndo();

                actionPerformed = false;
            }
            else
                throw new InvalidOperationException("Action not yet performed");
        }

        
    }
}
