using Sudoku.Game.Actions;
using Sudoku.Game.Helper;
using Sudoku.Game.Validation;
using Sudoku.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sudoku.Game.Model
{
    public delegate void GameEndedDelegate(bool newHighscore);

    public class SudokuGame : ObservableBase
    {
        private SudokuBoard board;
        public SudokuBoard Board { get => board; 
            private set  
            { 
                SetField(ref board, value); 
                actionStack.Clear();
                redoStack.Clear();
            } 
        }

        private TimeSpan myGameTime;

        public TimeSpan GameTime
        {
            get { return myGameTime; }
            set { SetField(ref myGameTime, value); }
        }

        private Highscore myHighscore;

        public Highscore Highscore
        {
            get { return myHighscore; }
            set { myHighscore = value; }
        }

        private const string myHighscoreFilename = "Highscore.xml";

        private SudokuBoard solution;

        private List<IValidator> validators = new();
        private Stack<IUndoableAction> actionStack = new();
        private Stack<IUndoableAction> redoStack = new();

        private DispatcherTimer timer;

        public event GameEndedDelegate GameEnded;

        public SudokuGame()
        {
            validators.Add(new HorizontalValidator());
            validators.Add(new VerticalValidator());
            validators.Add(new BlockValidator());
            //validators.Add(new AllSquaresSetValidator());

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            if(File.Exists(myHighscoreFilename))
            {
                Highscore = Highscore.Load(myHighscoreFilename);
            }
            else
            {
                Highscore = new Highscore();
                for(int i = 0; i <= 10; i++) 
                {
                    Highscore.TryAddNewTime(TimeSpan.FromMinutes(120));
                }
                Highscore.Save(myHighscoreFilename);
            }

            StartNewGame(SudokuBoardBuilder.GetEasyExampleField());
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            GameTime += TimeSpan.FromSeconds(1);
        }

        public void StartNewGame(SudokuBoard board)
        {
            Board = board;
            solution = board.GetSimplifiedCopy();
            if(!SudokuAlgorithms.SolveBoard(solution))
            {
                // board not solvable

            }
            GameTime = TimeSpan.Zero;
        }

        public void ValidateAgainstSolution()
        {
            for(int i = 0; i < Board.Squares.Count; i++)
            {
                var square = Board.Squares[i];
                if(square != null && !square.IsFixed && square.Number != null)
                {
                    if(square.Number != solution.Squares[i].Number)
                    {
                        square.IsError = true;
                    }
                }
            }
        }

        private void ValidateSquares()
        {
            var invalidSquares = new List<SudokuSquare>();

            foreach(IValidator validator in validators)
            {
                validator.IsValid(Board, out List<SudokuSquare> invalids);
                invalidSquares.AddRange(invalids);
            }

            // set the error flag on all squares
            Board.Squares.All(x => { x.IsError = invalidSquares.Contains(x); return true; });
        }

        private bool HasGameEnded()
        {
            return Board.ToString() == solution.ToString();
        }

        private void HandleBoardChange()
        {
            ValidateSquares();

            if(HasGameEnded())
            {
                timer.Stop();

                bool newHighscoure = Highscore.TryAddNewTime(GameTime);
                if(newHighscoure)
                {
                    Highscore.Save(myHighscoreFilename);
                }

                GameEnded?.Invoke(newHighscoure);
            }
        }

        public void DoAction(IAction action)
        {
            if(action.IsValidOperation())
            {
                if(GameTime == TimeSpan.Zero && !timer.IsEnabled)
                {
                    timer.Start();
                }

                action.Do();

                if(action is IUndoableAction undoableAction)
                {
                    actionStack.Push(undoableAction);
                    redoStack.Clear();
                }

                HandleBoardChange();
            }
        }

        public void UndoAction()
        {
            if(actionStack.Count > 0)
            {
                var action = actionStack.Pop();
                action.Undo();
                redoStack.Push(action);

                HandleBoardChange();
            }
        }

        public void RedoAction()
        {
            if(redoStack.Count > 0)
            {
                var action = redoStack.Pop();
                action.Do();
                actionStack.Push(action);

                HandleBoardChange();
            }
        }
    }
}
