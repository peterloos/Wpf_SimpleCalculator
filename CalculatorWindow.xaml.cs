using SimpleCalculator.ViewModel;
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

    // TODO: WPF MIT MVVM !!!!

//    http://codereview.stackexchange.com/questions/57806/wpf-calculator-code !!!!!!!!!!!!!!!


//    Auf Diese müsste es jetzt möglich sein, mit Hilfe eines Zufallszahlengenerators das Modell
//    mit einem Testgetriebenen Automaten zu testen !!!




    public partial class CalculatorWindow : Window
    {
        public CalculatorWindow()
        {
            this.InitializeComponent();

            //CalculatorViewModel vm = (CalculatorViewModel) this.DataContext;
            //vm.DigitCommand.Execute("3");
        }
    }
}
