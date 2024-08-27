using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Sudoku.WPF
{
    public class SquarePositionToBorderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var listView = values[0] as ListView;
            var field = values[1] as SudokuBoard;
            var square = values[2] as SudokuSquare;

            var left = 1;
            var top = 1;
            var right = 1;
            var bottom = 1;

            if (listView != null && field != null && square != null) 
            {
                var index = listView?.Items.IndexOf(square);
                var x = index % field.Width;
                var y = index / field.Width;

                if (x > 0 && x % field.BlockSize == 0)
                {
                    left = 3;
                }

                if (y > 0 && y % field.BlockSize == 0)
                {
                    top = 3;
                }
            }

            var margin = new Thickness(left, top, right, bottom);
            return margin;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
