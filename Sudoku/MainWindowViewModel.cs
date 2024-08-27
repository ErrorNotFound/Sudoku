using Sudoku.Game.Actions;
using Sudoku.Game.Helper;
using Sudoku.Game.Model;
using Sudoku.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Sudoku.Game.Helper.Delegates;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Documents;

namespace Sudoku
{
    public class MainWindowViewModel : ObservableBase
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private readonly ProgressDelegate progressUpdate;

        private SudokuGame game;
        public SudokuGame Game
        {
            get => game;
            set => SetField(ref game, value);
        }

        private EditMode editMode;
        public EditMode EditMode
        {
            get => editMode;
            set => SetField(ref editMode, value);
        }


        public MainWindowViewModel(ProgressDelegate progress = null)
        {
            game = new SudokuGame();
            progressUpdate = progress;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;
        }

        public void ChangeValue(IEnumerable<SudokuSquare> squares, int value)
        {
            var action = new ChangeValueAction(squares, value);
            Game.DoAction(action);
        }

        public void ChangeTopNotes(IEnumerable<SudokuSquare> squares, int? value)
        {
            var action = new ChangeTopNotesAction(squares, value);
            Game.DoAction(action);
        }

        public void ChangeCenterNotes(IEnumerable<SudokuSquare> squares, int? value)
        {
            var action = new ChangeCenterNotesAction(squares, value);
            Game.DoAction(action);
        }

        internal void Delete(IEnumerable<SudokuSquare> squares)
        {
            var action = new DeleteAction(squares);
            Game.DoAction(action);
        }

        internal void DoubleSelect(SudokuSquare square)
        {
            var action = new SelectAction(square, Game.Board.Squares);
            Game.DoAction(action);
        }

        internal void Undo()
        {
            Game.UndoAction();
        }

        internal void Redo()
        {
            Game.RedoAction();
        }

        internal void Validate()
        {
            Game.ValidateAgainstSolution();
        }

        internal void Solve()
        {
            var action = new SolveAction(Game.Board);
            Game.DoAction(action);
        }

        internal void Generate()
        {
            if(worker.IsBusy)
            {
                if(worker.CancellationPending == false)
                {
                    worker.CancelAsync();
                }   
            }
            else
            {
                worker.RunWorkerAsync();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var board = SudokuBoardBuilder.GenerateRandomBoard(progressUpdate.Invoke, sender as BackgroundWorker);

            if(sender is BackgroundWorker _worker && _worker.CancellationPending)
            {
                e.Cancel = true;
                progressUpdate.Invoke(0, 100);
                return;
            }
            if(board != null)
            {
                e.Result = board;
            }
            progressUpdate.Invoke(0, 100);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(!e.Cancelled && e.Result is SudokuBoard board)
            {
                Game.StartNewGame(board);
            }
        }

        internal void Import(string boardAsString)
        {
            var board = new SudokuBoard(boardAsString);
            Game.StartNewGame(board);
        }



        internal void Load(string filename)
        {
            var board = SudokuBoard.Load(filename);
            if(board != null)
            {
                Game.StartNewGame(board);
            }
        }

        internal void Save(string filename)
        {
            Game.Board.Save(filename);
        }
    }
}
