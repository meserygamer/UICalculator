using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UICalculator.Models
{
    public class MathematicalFunction
    {
        public MathematicalFunction(Func<double, double, double> func
            , string functionMark
            , string functionName)
        {
            Func = func;
            FunctionMark = functionMark;
            FunctionName = functionName;
        }

        public string FunctionMark { get; set; }
        public string FunctionName { get; set; }

        public Func<double, double, double> Func { get; set; }


        public override string ToString()
        {
            return FunctionName;
        }
    }
}
