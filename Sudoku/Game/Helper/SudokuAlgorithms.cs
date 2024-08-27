using Sudoku.Game.Model;
using Sudoku.Game.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sudoku.Game.Helper.Delegates;

namespace Sudoku.Game.Helper
{
    public static class SudokuAlgorithms
    {
        private static List<int> numberList = Enumerable.Range(1, 9).ToList();

        private static bool HasMultipleSolutionsParallel(string board, int? onlyConsiderIndex = null)
        {
            var emptySquareIndexes = onlyConsiderIndex != null ? new List<int>() { (int)onlyConsiderIndex} : board.AllIndexesOf("0");

            foreach (var emptySquareIndex in emptySquareIndexes)
            {
                int solutions = 0;

                Parallel.ForEach(numberList, number =>
                {
                    var sb = new StringBuilder(board);
                    sb[emptySquareIndex] = (char)(number + 48);

                    if(SolveBoard(new SudokuBoard(sb.ToString())))
                    {
                        solutions++;
                    }
                });

                if (solutions > 1)
                    return true;
            }

            return false;
        }

        public static bool SolveBoard(SudokuBoard board, BackgroundWorker worker = null)
        {
            var validators = new List<IValidator>()
            {
                new HorizontalValidator(),
                new VerticalValidator(),
                new BlockValidator()
            };

            var nextEmptySquare = board.Squares.FirstOrDefault(x => x.Number == null);
            if (nextEmptySquare == null)
            {
                // no more empty square -> we are done, but check if solution is valid
                return validators.All(x => x.IsValid(board, out List<SudokuSquare> invalids));
            }

            foreach (var number in numberList.AsShuffled())
            {
                if(worker?.CancellationPending ?? false)
                {
                    return false;
                }

                nextEmptySquare.Number = number;
                if (validators.All(x => x.IsValid(board, out List<SudokuSquare> invalids)))
                {
                    if (SolveBoard(board))
                    {
                        return true;
                    }
                }
                nextEmptySquare.Number = null;
            }
            return false;
        }

        public static void RemoveNumbers(SudokuBoard board, int count, ProgressDelegate progressCallback = null, BackgroundWorker worker = null)
        {
            var shuffledSquares = board.Squares.Where(x => x.Number != null).ToList().AsShuffled();
            var squareStack = new Stack<SudokuSquare>(shuffledSquares);
            int total = count;

            while (count > 0)
            {
                if(squareStack.Count == 0) 
                {
                    throw new Exception("No more squares can be removed");
                }

                if (worker?.CancellationPending ?? false)
                {
                    return;
                }

                // Get and save a square
                var currentSquare = squareStack.Pop();
                var currentSquareNumber = currentSquare.Number;

                // remove number
                currentSquare.IsFixed = false;
                currentSquare.Number = null;

                if(SolveBoard(board.GetSimplifiedCopy()) && HasMultipleSolutionsParallel(board.ToString(), board.Squares.IndexOf(currentSquare)) == false)
                {
                    // we can safely delete the square
                    count--;
                    progressCallback?.Invoke(total - count, total);
                    Debug.WriteLine($"Removed Number. Remaining {count}");
                }
                else
                {
                    // we need to reset and skip this square
                    currentSquare.Number = currentSquareNumber;
                    currentSquare.IsFixed = true;
                }
            }
            Debug.WriteLine("Removing done");
        }  
    }
}
