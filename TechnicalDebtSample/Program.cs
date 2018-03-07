using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalDebtSample
{
    public class Program
    {
        public static int Main()
        {
            var complexMethodExample = new ComplexMethodExample();
            complexMethodExample.AComplexMethod();
            Console.Read();
            return 0;
        }
    }
}
