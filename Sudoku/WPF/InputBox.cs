﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace Sudoku.WPF
{
    public class InputBox
    {

        Window Box = new Window();// window for the inputbox
        FontFamily font = new FontFamily("Tahoma");// font for the whole inputbox
        int FontSize = 30;// fontsize for the input
        StackPanel sp1 = new StackPanel();// items container
        string title = "InputBox";// title as heading
        string boxcontent;// title
        string defaulttext = "";// default textbox content
        string errormessage = "Invalid answer";// error messagebox content
        string errortitle = "Error";// error messagebox heading title
        string okbuttontext = "OK";// Ok button content
        Brush BoxBackgroundColor = Brushes.White;// Window Background
        Brush InputBackgroundColor = Brushes.Ivory;// Textbox Background
        bool clicked = false;
        TextBox input = new TextBox();
        Button ok = new Button();
        bool inputreset = false;

        public InputBox(string content)
        {
            try
            {
                boxcontent = content;
            }
            catch { boxcontent = "Error!"; }
            windowdef();
        }

        public InputBox(string content, string Htitle, string DefaultText)
        {
            try
            {
                boxcontent = content;
            }
            catch { boxcontent = "Error!"; }
            try
            {
                title = Htitle;
            }
            catch
            {
                title = "Error!";
            }
            try
            {
                defaulttext = DefaultText;
            }
            catch
            {
                DefaultText = "Error!";
            }
            windowdef();
        }

        public InputBox(string content, string Htitle, string Font, int Fontsize)
        {
            try
            {
                boxcontent = content;
            }
            catch { boxcontent = "Error!"; }
            try
            {
                font = new FontFamily(Font);
            }
            catch { font = new FontFamily("Tahoma"); }
            try
            {
                title = Htitle;
            }
            catch
            {
                title = "Error!";
            }
            if (Fontsize >= 1)
                FontSize = Fontsize;
            windowdef();
        }

        private void windowdef()// window building - check only for window size
        {
            Box.Height = 500;// Box Height
            Box.Width = 300;// Box Width
            Box.Background = BoxBackgroundColor;
            Box.Title = title;
            Box.Content = sp1;
            Box.Closing += Box_Closing;
            TextBlock content = new TextBlock();
            content.TextWrapping = TextWrapping.Wrap;
            content.Background = null;
            content.HorizontalAlignment = HorizontalAlignment.Center;
            content.Text = boxcontent;
            content.FontFamily = font;
            content.FontSize = FontSize;
            sp1.Children.Add(content);

            input.Background = InputBackgroundColor;
            input.FontFamily = font;
            input.FontSize = FontSize;
            input.HorizontalAlignment = HorizontalAlignment.Center;
            input.Text = defaulttext;
            input.MinWidth = 200;
            sp1.Children.Add(input);
            ok.Width = 70;
            ok.Height = 30;
            ok.Click += ok_Click;
            ok.Content = okbuttontext;
            ok.HorizontalAlignment = HorizontalAlignment.Center;
            sp1.Children.Add(ok);

        }

        void Box_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!clicked)
                e.Cancel = true;
        }

        void ok_Click(object sender, RoutedEventArgs e)
        {
            Box.Close();
        }

        public string ShowDialog()
        {
            Box.ShowDialog();
            return input.Text;
        }
    }
}
