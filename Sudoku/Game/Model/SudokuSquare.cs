using Sudoku.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Model
{
    [Serializable]
    public class SudokuSquare : ObservableBase
    {
        private int? number;
        public int? Number { get => number; set
            {
                if (IsFixed)
                {
                    throw new InvalidOperationException("Square is fixed");
                }
                SetField(ref number, value);
            } }

        private bool isFixed;
        public bool IsFixed { get => isFixed; set => SetField(ref isFixed, value); }

        private bool isError;
        public bool IsError { get => isError; set => SetField(ref isError, value); }

        private bool isSelected;
        public bool IsSelected { get => isSelected; set => SetField(ref isSelected, value); }

        private ObservableCollection<int> topNotes;
        public ObservableCollection<int> TopNotes
        {
            get => topNotes;
            set => SetField(ref topNotes, value);
        }

        private ObservableCollection<int> centerNotes;
        public ObservableCollection<int> CenterNotes
        {
            get => centerNotes;
            set => SetField(ref centerNotes, value);
        }


        public SudokuSquare() 
            :this(null, false)
        { 
        }

        public SudokuSquare(int? value, bool isFixed)
        {
            TopNotes = new ObservableCollection<int>();
            CenterNotes = new ObservableCollection<int>();
            TopNotes.CollectionChanged += TopNotes_CollectionChanged;
            CenterNotes.CollectionChanged += CenterNotes_CollectionChanged;
            Number = value;
            IsFixed = isFixed;
        }
        ~SudokuSquare()
        {
            TopNotes.CollectionChanged -= TopNotes_CollectionChanged;
            CenterNotes.CollectionChanged -= CenterNotes_CollectionChanged;
        }

        private void TopNotes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TopNotes));
        }

        private void CenterNotes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CenterNotes));
        }

        public override string? ToString()
        {
            return Number?.ToString() ?? "0";
        }
    }
}
