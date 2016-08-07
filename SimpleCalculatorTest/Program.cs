using SimpleCalculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculatorTest
{
    public class Program
    {
        public static void Main()
        {
            // debug support
            ConsoleTraceListener listener = new ConsoleTraceListener();
            Debug.Listeners.Add(listener);

            // Test_01_PowerByTwo();
            Test_01_Random();
        }

        public static void Test_01_Random()
        {
            CalculatorViewModel vm = new CalculatorViewModel();

            Random rand = new Random();

            for (int i = 0; i < 100000; i ++)
            {
                int n = rand.Next(19);

                switch (n)
                {
                    case 0:
                        vm.DigitCommand.Execute("0");
                        break;
                    case 1:
                        vm.DigitCommand.Execute("1");
                        break;
                    case 2:
                        vm.DigitCommand.Execute("2");
                        break;
                    case 3:
                        vm.DigitCommand.Execute("3");
                        break;
                    case 4:
                        vm.DigitCommand.Execute("4");
                        break;
                    case 5:
                        vm.DigitCommand.Execute("5");
                        break;
                    case 6:
                        vm.DigitCommand.Execute("6");
                        break;
                    case 7:
                        vm.DigitCommand.Execute("7");
                        break;
                    case 8:
                        vm.DigitCommand.Execute("8");
                        break;
                    case 9:
                        vm.DigitCommand.Execute("9");
                        break;
                    case 10:
                        vm.OperatorCommand.Execute("+");
                        break;
                    case 11:
                        vm.OperatorCommand.Execute("-");
                        break;
                    case 12:
                        vm.OperatorCommand.Execute("*");
                        break;
                    case 13:
                        vm.OperatorCommand.Execute("/");
                        break;
                    case 14:
                        vm.NegateCommand.Execute(null);
                        break;
                    case 15:
                        vm.CommaCommand.Execute(null);
                        break;
                    case 16:
                        vm.BackCommand.Execute(null);
                        break;
                    case 17:
                        vm.ClearCommand.Execute(null);
                        break;
                    case 18:
                        vm.EqualCommand.Execute(null);
                        break;
                }
            }

            String lastResult = vm.DisplayInput;
            String lastHistory = vm.DisplayHistory;

            Console.WriteLine("LastResult: {0}", lastResult);
            Console.WriteLine("LastHistory: {0}", lastHistory);

        }
        public static void Test_01_PowerByTwo()
        {
            CalculatorViewModel vm = new CalculatorViewModel();

            // calculation with calculator
            vm.DigitCommand.Execute("2");
            vm.OperatorCommand.Execute("*");
            vm.DigitCommand.Execute("2");

            // calculation with program
            long n1 = 2;

            for (int i = 0; i < 48; i++)
            {
                vm.EqualCommand.Execute(null);
                String result = vm.DisplayInput;
                Console.WriteLine("RESULT ==> {0}", result);

                // verify result
                String stripped = result.Replace(".", "");
                long n2 = Int64.Parse(stripped);

                if (n1 != n2)
                    Console.WriteLine("ERROR: {0} differs from {1} !!!!", n1, n2);
            }
        }
    }
}
