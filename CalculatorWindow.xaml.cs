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

//    Symbol	Unicode	Description
//⌥	U+2325	Option key
//⌫	U+232B	Delete / Backspace
//␡	U+2421	Alternative DEL
//⌦	U+2326	Forward delete

//&#x2014; in XAML



    public partial class CalculatorWindow : Window
    {
        public CalculatorWindow()
        {
            this.InitializeComponent();
        }
    }
}
