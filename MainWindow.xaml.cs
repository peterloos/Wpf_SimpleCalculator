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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleCalculator
{
    internal enum Operators { NoOp, AddOp, SubOp, MulOp, DivOp };

    public partial class MainWindow : Window
    {
        private int numOperands;

        private int lastOperand;

        private Operators lastOp;

        private bool reset;
        private bool equalActive;

        public MainWindow()
        {
            this.InitializeComponent();

            this.numOperands = 0;
            this.lastOp = Operators.NoOp;

            this.reset = true;
            this.equalActive = false;
        }

        private void Button_0_Click(Object sender, RoutedEventArgs e)
        {
            this.AddString("0");
        }

        private void Button_1_Click(Object sender, RoutedEventArgs e)
        {
            this.AddString("1");
        }

        private void Button_2_Click(Object sender, RoutedEventArgs e)
        {
            this.AddString("2");
        }

        private void Button_3_Click(Object sender, RoutedEventArgs e)
        {
            this.AddString("3");
        }

        private void Button_4_Click(Object sender, RoutedEventArgs e)
        {
            this.AddString("4");
        }

        private void Button_5_Click(Object sender, RoutedEventArgs e)
        {
            this.AddString("5");
        }

        private void Button_6_Click(Object sender, RoutedEventArgs e)
        {
            this.AddString("6");
        }

        private void Button_7_Click(Object sender, RoutedEventArgs e)
        {
            this.AddString("7");
        }

        private void Button_8_Click(Object sender, RoutedEventArgs e)
        {
            this.AddString("8");
        }

        private void Button_9_Click(Object sender, RoutedEventArgs e)
        {
            this.AddString("9");
        }

        private void Button_Add_Click(Object sender, RoutedEventArgs e)
        {
            if (this.TextBoxInput.Text == "")
                return;

            if (this.numOperands == 0)
            {
                this.numOperands = 1;
                this.lastOperand = Int32.Parse (this.TextBoxInput.Text);

                if (this.equalActive)
                {
                    this.TextBoxStack.Text = this.lastOperand.ToString();
                    this.equalActive = false;
                }
                else
                    this.TextBoxStack.Text += this.lastOperand.ToString();

                this.TextBoxStack.Text += " + ";
                this.TextBoxInput.Text = "";
            }
            else if (this.numOperands == 1)
            {
                int currentOperand = Int32.Parse(this.TextBoxInput.Text);

                this.TextBoxStack.Text += this.TextBoxInput.Text;
                this.TextBoxStack.Text += " + ";

                if (this.lastOp == Operators.AddOp)
                    this.lastOperand += currentOperand;
                else if (this.lastOp == Operators.SubOp)
                    this.lastOperand -= currentOperand;
                else if (this.lastOp == Operators.MulOp)
                    this.lastOperand *= currentOperand;
                else if (this.lastOp == Operators.DivOp)
                    this.lastOperand /= currentOperand;

                this.TextBoxInput.Text = this.lastOperand.ToString();
                this.reset = true;
            }

            this.lastOp = Operators.AddOp;
        }

        private void Button_Sub_Click(Object sender, RoutedEventArgs e)
        {
            if (this.TextBoxInput.Text == "")
                return;

            if (this.numOperands == 0)
            {
                this.numOperands = 1;
                this.lastOperand = Int32.Parse(this.TextBoxInput.Text);

                if (this.equalActive)
                {
                    this.TextBoxStack.Text = this.lastOperand.ToString();
                    this.equalActive = false;
                }
                else
                    this.TextBoxStack.Text += this.lastOperand.ToString();

                this.TextBoxStack.Text += " - ";
                this.TextBoxInput.Text = "";
            }
            else if (this.numOperands == 1)
            {
                int currentOperand = Int32.Parse(this.TextBoxInput.Text);

                this.TextBoxStack.Text += this.TextBoxInput.Text;
                this.TextBoxStack.Text += " - ";

                if (this.lastOp == Operators.AddOp)
                    this.lastOperand += currentOperand;
                else if (this.lastOp == Operators.SubOp)
                    this.lastOperand -= currentOperand;
                else if (this.lastOp == Operators.MulOp)
                    this.lastOperand *= currentOperand;
                else if (this.lastOp == Operators.DivOp)
                    this.lastOperand /= currentOperand;

                this.TextBoxInput.Text = this.lastOperand.ToString();
                this.reset = true;
            }

            this.lastOp = Operators.SubOp;
        }

        private void Button_Mul_Click(Object sender, RoutedEventArgs e)
        {
            if (this.TextBoxInput.Text == "")
                return;

            if (this.numOperands == 0)
            {
                this.numOperands = 1;
                this.lastOperand = Int32.Parse(this.TextBoxInput.Text);

                if (this.equalActive)
                {
                    this.TextBoxStack.Text = this.lastOperand.ToString();
                    this.equalActive = false;
                }
                else
                    this.TextBoxStack.Text += this.lastOperand.ToString();

                this.TextBoxStack.Text += " * ";
                this.TextBoxInput.Text = "";
            }
            else if (this.numOperands == 1)
            {
                int currentOperand = Int32.Parse(this.TextBoxInput.Text);

                this.TextBoxStack.Text += this.TextBoxInput.Text;
                this.TextBoxStack.Text += " * ";

                if (this.lastOp == Operators.AddOp)
                    this.lastOperand += currentOperand;
                else if (this.lastOp == Operators.SubOp)
                    this.lastOperand -= currentOperand;
                else if (this.lastOp == Operators.MulOp)
                    this.lastOperand *= currentOperand;
                else if (this.lastOp == Operators.DivOp)
                    this.lastOperand /= currentOperand;

                this.TextBoxInput.Text = this.lastOperand.ToString();
                this.reset = true;
            }

            this.lastOp = Operators.MulOp;
        }

        private void Button_Div_Click(Object sender, RoutedEventArgs e)
        {
            if (this.TextBoxInput.Text == "")
                return;

            if (this.numOperands == 0)
            {
                this.numOperands = 1;
                this.lastOperand = Int32.Parse(this.TextBoxInput.Text);

                if (this.equalActive)
                {
                    this.TextBoxStack.Text = this.lastOperand.ToString();
                    this.equalActive = false;
                }
                else
                    this.TextBoxStack.Text += this.lastOperand.ToString();

                this.TextBoxStack.Text += " / ";
                this.TextBoxInput.Text = "";
            }
            else if (this.numOperands == 1)
            {
                int currentOperand = Int32.Parse(this.TextBoxInput.Text);

                this.TextBoxStack.Text += this.TextBoxInput.Text;
                this.TextBoxStack.Text += " / ";

                if (this.lastOp == Operators.AddOp)
                    this.lastOperand += currentOperand;
                else if (this.lastOp == Operators.SubOp)
                    this.lastOperand -= currentOperand;
                else if (this.lastOp == Operators.MulOp)
                    this.lastOperand *= currentOperand;
                else if (this.lastOp == Operators.DivOp)
                    this.lastOperand /= currentOperand;

                this.TextBoxInput.Text = this.lastOperand.ToString();
                this.reset = true;
            }

            this.lastOp = Operators.DivOp;
        }

        private void Button_Equal_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextBoxInput.Text == "")
                return;

            if (this.numOperands == 1)
            {
                this.numOperands = 0;

                this.TextBoxStack.Text += this.TextBoxInput.Text;

                int currentOperand = Int32.Parse(this.TextBoxInput.Text);
                if (this.lastOp == Operators.AddOp)
                    this.lastOperand += currentOperand;
                else if (this.lastOp == Operators.SubOp)
                    this.lastOperand -= currentOperand;
                else if (this.lastOp == Operators.MulOp)
                    this.lastOperand *= currentOperand;
                else if (this.lastOp == Operators.DivOp)
                    this.lastOperand /= currentOperand;

                this.TextBoxInput.Text = this.lastOperand.ToString();

                this.reset = true;

                this.equalActive = true;
            }
        }

        private void Button_Clear_Click(Object sender, RoutedEventArgs e)
        {
            this.numOperands = 0;
            this.lastOp = Operators.NoOp;

            this.reset = true;

            this.TextBoxInput.Text = "";
            this.TextBoxStack.Text = "";
        }

        private void Button_Back_Click(Object sender, RoutedEventArgs e)
        {
            String s = "";
            int len = this.TextBoxInput.Text.Length;
            if (len > 0)
                s = this.TextBoxInput.Text.Substring(0, len - 1);
            this.TextBoxInput.Text = s;
        }
        
        // private helper methods
        private void AddString(String s)
        {
            if (this.reset == true)
            {
                this.reset = false;
                this.TextBoxInput.Text = "";
            }

            this.TextBoxInput.Text += s;
        }
    }
}
