using Sudoku.Game.Helper;
using Sudoku.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sudoku.Game.Model
{
    public class SudokuBoard : ObservableBase
    {
        public static readonly int DefaultFieldSize = 9;
        public static readonly int DefaultBlockSize = 3;

        private ObservableCollection<SudokuSquare> squares;
        public ObservableCollection<SudokuSquare> Squares
        {
            get => squares; set => SetField(ref squares, value);
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public int BlockSize { get; set; }

        public SudokuBoard()
        {
            Squares = new ObservableCollection<SudokuSquare>();
        }

        public SudokuBoard(int width, int height, int blockSize)
            :this()
        {
            FillBoard(width, height, blockSize);
        }

        public SudokuBoard(string boardAsString)
            : this()
        {
            FillBoard(boardAsString);
        }

        public void FillBoard(int width, int height, int blockSize)
        {
            Width = width;
            Height = height;
            BlockSize = blockSize;

            for (int i = 0; i < Width * Height; i++)
            {
                Squares.Add(new SudokuSquare());
            }
        }

        public void FillBoard(string boardAsString)
        {
            if (boardAsString.Length != DefaultFieldSize * DefaultFieldSize)
                throw new NotSupportedException("Input string invalid");

            Width = DefaultFieldSize;
            Height = DefaultFieldSize;
            BlockSize = DefaultBlockSize;

            Squares = new ObservableCollection<SudokuSquare>();
            for (int i = 0; i < Width * Height; i++)
            {
                if (int.TryParse(boardAsString[i].ToString(), out int num))
                {
                    if (num == 0)
                    {
                        Squares.Add(new SudokuSquare());
                    }
                    else
                    {
                        Squares.Add(new SudokuSquare(num, true));
                    }
                }
            }
        }

        public SudokuSquare this[int x, int y]
        {
            get
            {
                if(x < 0 || x >= Width)
                    throw new ArgumentOutOfRangeException("x");
                if(y < 0 || y >= Height)
                    throw new ArgumentOutOfRangeException("y");

                int i = x + y * Width;
                return Squares[i];
            }
        }

        public IEnumerable<IEnumerable<SudokuSquare>> GetRows()
        {
            var temp = Squares.AsEnumerable();
            while (temp.Any())
            {
                yield return temp.Take(Width);
                temp = temp.Skip(Width);
            }
        }

        public IEnumerable<IEnumerable<SudokuSquare>> GetColumns()
        {
            var temp = Squares.AsEnumerable();

            return temp.Select((x, i) => new { yRow = i % Width, Value = x })
                .GroupBy(x => x.yRow)
                .Select(x => x.Select(y => y.Value));
        }

        public IEnumerable<IEnumerable<SudokuSquare>> GetBlocks()
        {
            var blocks = new List<IEnumerable<SudokuSquare>>();
            int blockCount = (Width * Height) / (BlockSize * BlockSize);

            for (int i = 0; i < blockCount; i++)
            {
                int topX = (i * BlockSize) % Width;
                int topY = (i * BlockSize) / Width * BlockSize;

                var block = new List<SudokuSquare>();

                for (int x = topX, xB = 0; x < topX + BlockSize; x++, xB++)
                {
                    for (int y = topY, yB = 0; y < topY + BlockSize; y++, yB++)
                    {
                        block.Add(this[x, y]);
                    }
                }
                blocks.Add(block);
            }

            return blocks;
        }

        public SudokuBoard GetSimplifiedCopy()
        {
            var board = new SudokuBoard(ToString());
            return board;
        }

        public override string ToString()
        {
            return string.Join("", Squares);
        }

        public void Save(string filename)
        {
            var xmlSerializer = new XmlSerializer(GetType());
            using (var writer = new StreamWriter(filename))
            {
                xmlSerializer.Serialize(writer, this);
            }
        }

        public static SudokuBoard? Load(string filename)
        {
            XmlSerializer xmlSerializer = new(MethodBase.GetCurrentMethod().DeclaringType);
            using (var reader = new StreamReader(filename))
            {
                return xmlSerializer.Deserialize(reader) as SudokuBoard;
            }
        }
    }
}
