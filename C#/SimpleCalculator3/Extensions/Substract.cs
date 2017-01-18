using System.ComponentModel.Composition;
using Calculator.Interfaces;

namespace Calculator.Extensions
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '-')]
    class Subtract : IOperation
    {

        public int Operate(int left, int right)
        {
            return left - right;
        }

    }
}