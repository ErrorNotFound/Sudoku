using Microsoft.Win32;
using Sudoku.Game.Helper;
using Sudoku.Game.Model;
using Sudoku.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using static Sudoku.Game.Helper.Delegates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainWindowViewModel(OnProgressUpdate);
            DataContext = vm;
            vm.Game.GameEnded += GameEnded;
        }

        private void OnProgressUpdate(int step, int total)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                progressBar.Minimum = 0;
                progressBar.Maximum = total;
                progressBar.Value = step;
            }));            
        }

        private void GameEnded(bool newHighscore)
        {
            string text = "Game finished!";

            if(newHighscore)
            {
                text += " Congratulations. You achieved a new Highscore";
            }

            MessageBox.Show(text);
        }

        private void ListView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;            
            var squares = listView.SelectedItems.Cast<SudokuSquare>();

            bool isCtrl = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
            bool isShift = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

            switch (e.Key)
            {
                case Key.Delete:
                    {
                        if (isCtrl)
                        {
                            viewModel.ChangeTopNotes(squares, null);
                        }
                        else
                        {
                            viewModel.Delete(squares);
                        }
                    }
                    
                    break;
                case Key.Z:
                    if (isCtrl)
                    {
                        viewModel.Undo();
                        
                    }
                    break;
                case Key.Y:
                    if (isCtrl)
                    {
                        viewModel.Redo();
                    }
                    break;
                case Key.Space:
                    viewModel.EditMode = viewModel.EditMode.Next();
                    break;
                default:
                    if (KeyToNumberMapper.GetNumber(e.Key) is int number)
                    {
                        if (isCtrl)
                        {
                            viewModel.ChangeTopNotes(squares, number);
                        }
                        else if(isShift)
                        {
                            viewModel.ChangeCenterNotes(squares, number);
                        }
                        else
                        {
                            viewModel.ChangeValue(squares, number);
                        }
                    }

                    break;
            }

            e.Handled = true;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var lvi = (ListViewItem)sender;
                lvi.IsSelected = true;
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control)
            {
                listView.SelectedItems.Clear();
            }      
        }

        private void Button_Validate_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            viewModel.Validate();
            /*
            var result = viewModel.Validate();
            if(result == true)
            {
                MessageBox.Show("Congratulations! You solved the Sudoku");
            } */
        }

        private void Button_Solve_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            viewModel.Solve();
        }


        private void Button_Generate_Click(object sender, RoutedEventArgs e)
        {
            

            var viewModel = (MainWindowViewModel)DataContext;
            viewModel.Generate();
            progressBar.Value = 0;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var viewModel = (MainWindowViewModel)DataContext;
                viewModel.Save(dialog.FileName);
            }
            
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var viewModel = (MainWindowViewModel)DataContext;
                viewModel.Load(dialog.FileName);
            }
        }

        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;

            if (sender is ListViewItem lvi)
            {
                if(lvi.DataContext is SudokuSquare square)
                {
                    viewModel.DoubleSelect(square);
                }
            }
            e.Handled = true;
        }

        private void Button_Test(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            viewModel.Game.Board.Squares.First().IsSelected = true;
        }

        private void Button_Import_Click(object sender, RoutedEventArgs e)
        {
            var result = Microsoft.VisualBasic.Interaction.InputBox("Prompt here",
                                           "Title here",
                                           "Default data",
                                           -1, -1);

            var viewModel = (MainWindowViewModel)DataContext;
            viewModel.Import(result.Replace('.', '0'));
        }

        private void Button_Highscore_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;

            HighscoreWindow wnd = new HighscoreWindow(viewModel.Game.Highscore.Items);
            wnd.ShowDialog();
        }
    }
}
