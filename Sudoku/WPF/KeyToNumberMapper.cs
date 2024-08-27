using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sudoku.WPF
{
    public static class KeyToNumberMapper
    {
        public static int? GetNumber(Key key) => key switch
        {
            Key.D1 => 1,
            Key.D2 => 2,
            Key.D3 => 3,
            Key.D4 => 4,
            Key.D5 => 5,
            Key.D6 => 6,
            Key.D7 => 7,
            Key.D8 => 8,
            Key.D9 => 9,
            Key.NumPad1 => 1,
            Key.NumPad2 => 2,
            Key.NumPad3 => 3,
            Key.NumPad4 => 4,
            Key.NumPad5 => 5,
            Key.NumPad6 => 6,
            Key.NumPad7 => 7,
            Key.NumPad8 => 8,
            Key.NumPad9 => 9,
            Key.End => 1,
            Key.Down => 2,
            Key.PageDown => 3,
            Key.Left => 4,
            Key.Clear => 5,
            Key.Right => 6,
            Key.Home => 7,
            Key.Up => 8,
            Key.PageUp => 9,
            _ => null
        };
    }
}
