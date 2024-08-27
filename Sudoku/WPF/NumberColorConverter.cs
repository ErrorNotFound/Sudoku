using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Sudoku.WPF
{
    public class NumberColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var square = value as SudokuSquare;

            var colorFixed = Colors.Black;
            var colorUser = Colors.MediumBlue;

            var color = square?.IsFixed ?? true ? colorFixed : colorUser ;

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
