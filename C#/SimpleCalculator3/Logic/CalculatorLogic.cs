using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Calculator.Interfaces;

namespace Calculator.Logic
{
    [Export(typeof(ICalculator))]
    public class CalculatorLogic : ICalculator
    {
        // Imported Operations
        [ImportMany]
        IEnumerable<Lazy<IOperation, IOperationData>> operations;

        public string Calculate(string input)
        {
            int left;
            int right;
            char operation;

            int operationSignIndex = this.FindFirstNonDigit(input); //finds the operator
            if (operationSignIndex < 0)
            {
                return "Could not parse command.";
            }

            try
            {
                //separate out the numbers
                left = int.Parse(input.Substring(0, operationSignIndex)); //before operation sign
                right = int.Parse(input.Substring(operationSignIndex + 1)); //after operation sign
            }
            catch
            {
                return "Could not parse command.";
            }

            operation = input[operationSignIndex];

            foreach (Lazy<IOperation, IOperationData> i in this.operations)
            {
                if (i.Metadata.Symbol.Equals(operation))
                {
                    return i.Value.Operate(left, right).ToString();
                }
            }
            return "Operation Not Found!";
        }

        private int FindFirstNonDigit(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!(char.IsDigit(s[i])))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}