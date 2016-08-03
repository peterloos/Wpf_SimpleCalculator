using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleCalculator
{
    public partial class CalculatorWindow : Window
    {
        private TokenScanner scanner;

        public CalculatorWindow()
        {
            this.InitializeComponent();

            this.scanner = new TokenScanner();
            this.TextBoxInput.Text = this.scanner.CurrentInput;
        }

        private void Button_Digit_Click(Object sender, RoutedEventArgs e)
        {
            Button b = (Button) sender;

            char digit = ' ';

            if (b == this.Button_0)
                digit = '0';
            else if (b == this.Button_1)
                digit = '1';
            else if (b == this.Button_2)
                digit = '2';
            else if (b == this.Button_3)
                digit = '3';
            else if (b == this.Button_4)
                digit = '4';
            else if (b == this.Button_5)
                digit = '5';
            else if (b == this.Button_6)
                digit = '6';
            else if (b == this.Button_7)
                digit = '7';
            else if (b == this.Button_8)
                digit = '8';
            else if (b == this.Button_9)
                digit = '9';

            this.scanner.PushChar(digit);
            this.TextBoxInput.Text = this.scanner.CurrentInput;
        }

        private void Button_Operator_Click(Object sender, RoutedEventArgs e)
        {
            Button b = (Button) sender;

            Operator op = Operator.NoOp;

            if (b == this.Button_Add)
                op = Operator.AddOp;
            else if (b == this.Button_Sub)
                op = Operator.SubOp;
            else if (b == this.Button_Mul)
                op = Operator.MulOp;
            else if (b == this.Button_Div)
                op = Operator.DivOp;

            Console.WriteLine("Operator: {0}", op);

            this.scanner.PushOp(op);
            this.TextBoxInput.Text = this.scanner.CurrentInput;
            this.TextBoxHistory.Text = this.scanner.CurrentHistory;
        }

        private void Button_Clear_Click(Object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Clear");

            this.scanner.Reset();
            this.TextBoxInput.Text = this.scanner.CurrentInput;
            this.TextBoxHistory.Text = this.scanner.CurrentHistory;
        }

        private void Button_Back_Click(Object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Back");
            this.scanner.Back();
            this.TextBoxInput.Text = this.scanner.CurrentInput;
        }

        private void Button_Equal_Click(Object sender, RoutedEventArgs e)
        {
            this.scanner.Equal();
            this.TextBoxInput.Text = this.scanner.CurrentInput;
            this.TextBoxHistory.Text = this.scanner.CurrentHistory;
        }

        private void Button_Negate_Click(Object sender, RoutedEventArgs e)
        {
            this.scanner.Negate();
            this.TextBoxInput.Text = this.scanner.CurrentInput;
        }

        private void Button_Comma_Click(Object sender, RoutedEventArgs e)
        {
            this.scanner.Comma();
            this.TextBoxInput.Text = this.scanner.CurrentInput;
        }
    }
}
