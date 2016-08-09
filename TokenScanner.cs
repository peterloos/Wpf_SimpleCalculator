namespace SimpleCalculator
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public class TokenScanner
    {
        // input and history
        private StringBuilder currentInput;
        private StringBuilder currentHistory;

        // last recognized operand and operator
        private double lastOperand;
        private Operator lastOperator;

        // flags controlling interactive input
        private bool twoOperandsExisting;
        private bool replaceNextOperatorIfAny;
        private bool resetInput;
        private bool isBackspaceAllowed;
        private bool isConsecutiveEqual;

        private bool debugScanner;

        // c'tor
        public TokenScanner ()
        {
            this.debugScanner = true;

            this.currentInput = new StringBuilder(32);
            this.currentHistory = new StringBuilder(32);

            this.Reset();
        }

        // properties
        public String CurrentInput
        {
            get
            {
                StringBuilder copy = new StringBuilder (this.currentInput.ToString());
                return this.RawToDisplay(copy);
            }
        }

        public String CurrentHistory
        {
            get
            {
                return this.currentHistory.ToString();
            }
        }

        // public interface
        public void PushChar(char ch)
        {
            this.isBackspaceAllowed = true;

            if (this.resetInput)
            {
                this.currentInput.Clear();
                this.currentInput.Append(ch);

                this.resetInput = false; 
                this.replaceNextOperatorIfAny = false;
            }
            else if (this.currentInput.Length == 1)
            {
                if (this.currentInput[0] == '0')
                {
                    this.currentInput[0] = ch;
                }
                else
                {
                    this.currentInput.Append(ch);
                }
            }
            else
            {
                this.currentInput.Append(ch);
            }

            this.DumpInputLine();
        }

        public void Back()
        {
            if (!this.isBackspaceAllowed)
                return;

            bool isNegative = false;
            if (this.currentInput.Length >= 2 && this.currentInput[0] == '-')
            {
                isNegative = true;
            }

            if (this.currentInput.Length == 1 && !isNegative)
            {
                this.currentInput.Clear();
                this.currentInput.Append('0');
            }
            else if (this.currentInput.Length == 2 && isNegative)
            {
                this.currentInput.Clear();
                this.currentInput.Append('0');
            }
            else
            {
                this.currentInput.Remove(this.currentInput.Length - 1, 1);
            }

            this.DumpInputLine();
        }

        public void Negate()
        {
            // should never occur
            if (this.currentInput.Length == 0)
                return;

            // don't negate zero
            if (this.currentInput.Length == 1 && this.currentInput[0] == '0')
                return;

            char sign = this.currentInput[0];
            if (sign != '-')
            {
                this.currentInput.Insert(0, '-');
            }
            else
            {
                this.currentInput.Remove(0, 1);
            }

            this.DumpInputLine();
        }

        public void Comma()
        {
            if (this.resetInput)
            {
                this.resetInput = false;

                this.currentInput.Clear();
                this.currentInput.Append("0,");
            }
            else if (this.currentInput.ToString().IndexOf(',') == -1)
            {
                this.currentInput.Append(',');  // check for several commas
            }

            this.DumpInputLine();
        }

        public void PushOp(Operator op)
        {
            // input needs to be reset upon next input
            this.resetInput = true;

            // prevent backspace key destroying current result
            this.isBackspaceAllowed = false;

            // current operator isn't equal
            this.isConsecutiveEqual = false;

            if (! this.replaceNextOperatorIfAny)
            {
                // convert input into numerical value
                double operand = this.ParseInputAsDouble(this.currentInput.ToString());
                
                // build new history
                this.currentHistory.Append(operand.ToString());
                this.currentHistory.Append(this.OperatorToString(op));

                if (this.twoOperandsExisting)
                {
                    // evaluate operation
                    this.lastOperand =
                        this.CalculateValue(this.lastOperand, this.lastOperator, operand);
                }
                else
                {
                    // no first operand: assign input to first operand
                    this.lastOperand = operand;
                    this.twoOperandsExisting = true;
                }

                // replace input with last operand or result of calculation
                this.currentInput.Clear();
                this.currentInput.Append(this.lastOperand.ToString());

                this.replaceNextOperatorIfAny = true;
            }
            else
            {
                this.currentHistory.Remove(this.currentHistory.Length - 3, 3);
                this.currentHistory.Append(this.OperatorToString(op));
            }

            // store current operator
            this.lastOperator = op;

            this.DumpInputLine();
        }

        public void Equal()
        {
            if (!this.isConsecutiveEqual)
            {
                // clear history buffer
                this.currentHistory.Clear();

                // calculate current calculation result
                double operand = this.ParseInputAsDouble(this.currentInput.ToString());
                double result = this.CalculateValue(this.lastOperand, this.lastOperator, operand);

                // replace input buffer with result of operation
                this.currentInput.Clear();
                this.currentInput.Append(result.ToString());

                // clear last operand
                this.twoOperandsExisting = false;

                // prevent backspace key destroying current result
                this.isBackspaceAllowed = false;

                // handle upcoming equal key, if any
                this.isConsecutiveEqual = true;

                // and store second operator, if necessary
                this.lastOperand = operand;

                // in case of equal next operator should not be replaced
                this.replaceNextOperatorIfAny = false;
            }
            else
            {
                // calculate current calculation result
                double operand = this.ParseInputAsDouble(this.currentInput.ToString());
                double result = this.CalculateValue(operand, this.lastOperator, this.lastOperand);

                // replace input buffer with result of operation
                this.currentInput.Clear();
                this.currentInput.Append(result.ToString());
            }

            this.DumpInputLine();
        }

        public void Reset()
        {
            this.currentInput.Clear();
            this.currentInput.Append('0');
            this.currentHistory.Clear();

            this.twoOperandsExisting = false;
            this.replaceNextOperatorIfAny = false;
            this.resetInput = false;
            this.isBackspaceAllowed = false;
            this.isConsecutiveEqual = false;

            this.lastOperand = 0.0;
            this.lastOperator = Operator.NoOp;

            this.DumpInputLine();
        }

        // private helper methods
        private String OperatorToString (Operator op)
        {
            String result = "";

            switch (op)
            {
                case Operator.AddOp:
                    result = " + ";
                    break;
                case Operator.SubOp:
                    result = " - ";
                    break;
                case Operator.MulOp:
                    result = " * ";
                    break;
                case Operator.DivOp:
                    result = " / ";
                    break;
            }

            return result;
        }

        private double CalculateValue(double firstOperand, Operator op, double secondOperand)
        {
            double result = 0.0;

            try
            {
                switch (op)
                {
                    case Operator.AddOp:
                        result = checked(firstOperand + secondOperand);
                        break;
                    case Operator.SubOp:
                        result = checked(firstOperand - secondOperand);
                        break;
                    case Operator.MulOp:
                        result = checked(firstOperand * secondOperand);
                        break;
                    case Operator.DivOp:
                        if (secondOperand != 0.0)
                            result = checked(firstOperand / secondOperand);
                        break;
                }
            }
            catch (OverflowException)
            {
                String msg = String.Format("OverflowException: {0} {1} {2}",
                    firstOperand.ToString(), op.ToString(), secondOperand.ToString());
                Debug.WriteLineIf(this.debugScanner, msg);
            }

            return result;
        }

        private String RawToDisplay(StringBuilder sb)
        {
            if (sb.Length <= 3)
                return sb.ToString();

            int exponentIndex = sb.ToString().IndexOfAny(new char[] { 'e', 'E' });
            if (exponentIndex >= 0)
                return sb.ToString();

            int commaIndex = sb.ToString().IndexOf (',');
            int negativeSignIndex = sb.ToString().IndexOf('-');

            String result = "";

            // retrieve part of number to extend with decimal points
            if (commaIndex == -1)
            {
                if (negativeSignIndex == -1)
                {
                    result = this.AddDecimalSeparators(sb);
                }
                else
                {
                    result = '-' + this.AddDecimalSeparators(sb.Remove(0, 1));
                }
            }
            else
            {
                if (negativeSignIndex == -1)
                {
                    char[] triple = new char[commaIndex];
                    sb.CopyTo(0, triple, 0, commaIndex);
                    sb.Remove(0, commaIndex);

                    String integralPart = new String(triple);
                    result = this.AddDecimalSeparators(new StringBuilder(integralPart)) + sb.ToString();
                }
                else
                {
                    sb.Remove(0, 1);  // remove '-' sign
                    commaIndex--;     // comma index includes '-' sign

                    char[] triple = new char[commaIndex];
                    sb.CopyTo(0, triple, 0, commaIndex);
                    sb.Remove(0, commaIndex);

                    String integralPart = new String(triple);
                    result = '-' + this.AddDecimalSeparators(new StringBuilder(integralPart)) + sb.ToString();
                }
            }

            return result;
        }

        private String AddDecimalSeparators(StringBuilder sb)
        {
            String result = "";
            while (sb.Length > 3)
            {
                char[] triple = new char[3];
                sb.CopyTo(sb.Length - 3, triple, 0, 3);
                sb.Remove(sb.Length - 3, 3);
                result = "." + new String(triple) + result;
            }

            result = sb.ToString() + result;
            return result;
        }

        private double ParseInputAsDouble (String s)
        {
            double d = 0.0;
            try
            {
                d = Double.Parse(s);
            }
            catch (FormatException)
            {
                String msg = String.Format("FormatException: {0}", s);
                Debug.WriteLineIf(this.debugScanner, msg);
            }
            catch (OverflowException)
            {
                String msg = String.Format("OverflowException: {0}", s);
                Debug.WriteLineIf(this.debugScanner, msg);
            }

            return d;
        }

        // private trace helper method
        private void DumpInputLine()
        {
            String msg = String.Format("INPUT: {0}", this.currentInput.ToString());
            Debug.WriteLineIf(this.debugScanner, msg);
        }
    }
}
