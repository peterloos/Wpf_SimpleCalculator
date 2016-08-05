using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WpfApplication.Common;

namespace SimpleCalculator.ViewModel
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TokenScanner scanner;
        private String displayInput;
        private String displayHistory;

        public CalculatorViewModel ()
        {
            this.scanner = new TokenScanner();
            this.displayInput = "";
            this.displayHistory = "";
        }

        // properties
        public String DisplayInput
        {
            set
            {
                if (this.displayInput != value)
                {
                    this.displayInput = value;
                    this.OnPropertyChanged("DisplayInput");
                }
            }

            get
            {
                return this.displayInput;
            }
        }

        public String DisplayHistory
        {
            set
            {
                if (this.displayHistory != value)
                {
                    this.displayHistory = value;
                    this.OnPropertyChanged("DisplayHistory");
                }
            }

            get
            {
                return this.displayHistory;
            }
        }

        // commands
        public ICommand DigitCommand
        {
            get
            {
                return new RelayCommand (
                    param =>
                    {
                        char digit = ((String) param)[0];
                        Console.WriteLine("Digit Command: {0}", digit);
                        this.scanner.PushChar(digit);
                        this.DisplayInput = this.scanner.CurrentInput;
                    }
                );
            }
        }

        public ICommand OperatorCommand
        {
            get
            {
                return new RelayCommand (
                    param =>
                    {
                        Console.WriteLine("Operator Command: {0}", param);
                        Operator op = Operator.NoOp;

                        switch (((String)param)[0])
                        {
                            case '+':
                                op = Operator.AddOp;
                                break;
                            case '-':
                                op = Operator.SubOp;
                                break;
                            case '*':
                                op = Operator.MulOp;
                                break;
                            case '/':
                                op = Operator.DivOp;
                                break;
                        }

                        this.scanner.PushOp(op);
                        this.DisplayInput = this.scanner.CurrentInput;
                        this.DisplayHistory = this.scanner.CurrentHistory;
                    }
                );
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                return new RelayCommand(
                    param =>
                    {
                        Console.WriteLine("Clear Command");
                        this.scanner.Reset();
                        this.DisplayInput = this.scanner.CurrentInput;
                        this.DisplayHistory = this.scanner.CurrentHistory;
                    }
                );
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return new RelayCommand(
                    param =>
                    {
                        Console.WriteLine("Back Command");
                        this.scanner.Back();
                        this.DisplayInput = this.scanner.CurrentInput;
                    }
                );
            }
        }

        public ICommand EqualCommand
        {
            get
            {
                return new RelayCommand(
                    param =>
                    {
                        Console.WriteLine("Equal Command");
                        this.scanner.Equal();
                        this.DisplayInput = this.scanner.CurrentInput;
                        this.DisplayHistory = this.scanner.CurrentHistory;
                    }
                );
            }
        }

        public ICommand CommaCommand
        {
            get
            {
                return new RelayCommand(
                    param =>
                    {
                        Console.WriteLine("Comma Command");
                        this.scanner.Comma();
                        this.DisplayInput = this.scanner.CurrentInput;
                    }
                );
            }
        }

        public ICommand NegateCommand
        {
            get
            {
                return new RelayCommand(
                    param =>
                    {
                        Console.WriteLine("Negate Command");
                        this.scanner.Negate();
                        this.DisplayInput = this.scanner.CurrentInput;
                    }
                );
            }
        }

        // private helper methods
        private void OnPropertyChanged (String propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                handler.Invoke(this, args);
            }
        }
    }
}
