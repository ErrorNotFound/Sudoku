using Sudoku.Game.Model;
using Sudoku.Game.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sudoku.Game.Helper.Delegates;
using static Sudoku.Game.Helper.SudokuAlgorithms;

namespace Sudoku.Game.Helper
{
    public static class SudokuBoardBuilder
    {
        public static SudokuBoard GetFieldFromValues(int[,] values)
        {
            if (values.Length != 81 || values.GetLength(0) != 9 || values.GetLength(1) != 9)
                throw new NotSupportedException("No 9x9 field");

            var board = new SudokuBoard(values.GetLength(0), values.GetLength(1), 3);

            // iterate rows
            for (int y = 0; y < values.GetLength(0); y++)
            {
                // iterate columns
                for (int x = 0; x < values.GetLength(1); x++)
                {
                    var value = values[y, x];
                    if (value >= 0 && value < 10)
                    {
                        board[x, y].Number = value != 0 ? value : null;
                        board[x, y].IsFixed = value != 0;
                    }
                }
            }

            return board;
        }

        public static SudokuBoard GetEasyExampleField()
        {
            var values = new int[,] {
                {4,0,0,0,7,0,0,0,8},
                {0,5,7,0,0,0,3,9,0},
                {8,0,0,9,0,3,0,0,4},
                {0,0,6,7,0,4,5,0,0},
                {5,0,0,0,0,0,0,0,1},
                {0,0,2,5,0,1,8,0,0},
                {2,0,0,8,0,7,0,0,9},
                {0,1,8,0,0,0,7,3,0},
                {7,0,0,0,1,0,0,0,5}
            };

            return GetFieldFromValues(values);
        }

        public static SudokuBoard GenerateRandomBoard(ProgressDelegate progress = null, BackgroundWorker worker = null)
        {
            ProgressDelegate callback = delegate (int step, int total) {
                progress?.Invoke(step, total);
            };

            var board = new SudokuBoard(SudokuBoard.DefaultFieldSize, SudokuBoard.DefaultFieldSize, SudokuBoard.DefaultBlockSize);
            if (SolveBoard(board, worker))
            {
                RemoveNumbers(board, 54, callback, worker);
                foreach (var setSquare in board.Squares.Where(x => x.Number != null))
                {
                    setSquare.IsFixed = true;
                }
                return board;
            }

            return null;
        }
    }
}
