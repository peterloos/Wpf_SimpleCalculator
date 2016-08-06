using SimpleCalculator.ViewModel;
using System;
using System.Collections.Generic;
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
            long n = 2;

            for (int i = 0; i < 48; i++)
            {
                Console.WriteLine("                          --> i={0}", i);

                vm.EqualCommand.Execute(null);
                String result = vm.DisplayInput;
                Console.WriteLine("   ==> {0}", result);

                n = 2 * n;
                Console.WriteLine("   --> {0}", n);

                // verify result
                String stripped = result.Replace(".", "");
                long l = Int64.Parse(stripped);


                // long l = Int64.Parse(result, NumberStyles.AllowDecimalPoint);
                // long l = Int64.Parse(result, CultureInfo.InvariantCulture.NumberFormat);  CultureInfo.GetCultureInfo("en-US")
                // long l = Int64.Parse(result, CultureInfo.GetCultureInfo("en-US"));
                // double d = Double.Parse(result, CultureInfo.GetCultureInfo("en-US")); 
                Console.WriteLine("   __> {0}", l);

                if (n != l)
                    Console.WriteLine("   ERROR !!!!");

            }
        }
    }
}
