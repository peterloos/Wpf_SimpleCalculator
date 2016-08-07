using System;
using System.Diagnostics;
using System.Windows;

namespace SimpleCalculator
{

    // TODO: WPF MIT MVVM !!!!

//    http://codereview.stackexchange.com/questions/57806/wpf-calculator-code !!!!!!!!!!!!!!!


    public partial class CalculatorWindow : Window
    {
        public CalculatorWindow()
        {
            // debug support
            ConsoleTraceListener listener = new ConsoleTraceListener();
            Debug.Listeners.Add(listener);

            this.InitializeComponent();

            //CalculatorViewModel vm = (CalculatorViewModel) this.DataContext;
            //vm.DigitCommand.Execute("3");

        }
    }
}
