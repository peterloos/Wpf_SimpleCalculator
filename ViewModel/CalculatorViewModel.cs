namespace SimpleCalculator.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Input;
    using WpfApplication.Common;

    public class CalculatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TokenScanner scanner;
        private String displayInput;
        private String displayHistory;

        private bool debugViewModel;

        public CalculatorViewModel ()
        {
            this.scanner = new TokenScanner();

            this.displayInput = "";
            this.displayHistory = "";

            this.debugViewModel = true;
        }

        // properties
        public String DisplayInput
        {
            get
            {
                return this.displayInput;
            }

            set
            {
                if (this.displayInput != value)
                {
                    this.displayInput = value;
                    this.OnPropertyChanged("DisplayInput");
                }
            }
        }

        public String DisplayHistory
        {
            get
            {
                return this.displayHistory;
            }

            set
            {
                if (this.displayHistory != value)
                {
                    this.displayHistory = value;
                    this.OnPropertyChanged("DisplayHistory");
                }
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
                        String msg = String.Format("Digit Command: {0}", digit);
                        Debug.WriteLineIf(this.debugViewModel, msg);
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
                        String msg = String.Format("Operator Command: {0}", param);
                        Debug.WriteLineIf(this.debugViewModel, msg);
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
                        Debug.WriteLineIf(this.debugViewModel, "Clear Command");
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
                        Debug.WriteLineIf(this.debugViewModel, "Back Command");
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
                        Debug.WriteLineIf(this.debugViewModel, "Equal Command");
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
                        Debug.WriteLineIf(this.debugViewModel, "Comma Command");
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
                        Debug.WriteLineIf(this.debugViewModel, "Negate Command");
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
