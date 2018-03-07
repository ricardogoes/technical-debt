using System;
using System.Collections.Generic;

namespace TechnicalDebtSample
{
    public class ComplexMethodExample
    {
        public void AComplexMethod()
        {
            List<int> evenNumbers = null;
            List<int> numbersDivisibleByThree = null;
            List<int> primeNumbers = null;
            for (int n = 0; n < 10; n++)
            {
                if (n%2 == 0)
                {
                    if (evenNumbers == null)
                    {
                        evenNumbers = new List<int>();
                    }
                    evenNumbers.Add(n);
                    if (n%3 == 0)
                    {
                        if (numbersDivisibleByThree == null)
                        {
                            numbersDivisibleByThree = new List<int>();
                        }
                        numbersDivisibleByThree.Add(n);
                    }
                }
                else if (n%3 == 0)
                {
                    if (numbersDivisibleByThree == null)
                    {
                        numbersDivisibleByThree = new List<int>();
                    }
                    numbersDivisibleByThree.Add(n);
                    bool itIsPrime = true;
                    for (int j = 3; j < n; j += 2)
                    {
                        if (n%j == 0)
                        {
                            itIsPrime = false;
                            break;
                        }
                    }
                    if (itIsPrime)
                    {
                        if (primeNumbers == null)
                        {
                            primeNumbers = new List<int>();
                        }
                        primeNumbers.Add(n);
                    }
                }
                else
                {
                    bool itIsPrime = true;
                    for (int j = 3; j < n; j += 2)
                    {
                        if (n % j == 0)
                        {
                            itIsPrime = false;
                            break;
                        }
                    }
                    if (itIsPrime)
                    {
                        if (primeNumbers == null)
                        {
                            primeNumbers = new List<int>();
                        }
                        primeNumbers.Add(n);
                    }
                }
            }
            if (evenNumbers != null)
            {
                Console.WriteLine("Even Numbers");
                foreach (var even in evenNumbers)
                {
                    Console.WriteLine(even);
                }
            }
            if (primeNumbers != null)
            {
                Console.WriteLine("Prime Numbers");
                foreach (var even in primeNumbers)
                {
                    Console.WriteLine(even);
                }
            }
            if (numbersDivisibleByThree != null)
            {
                Console.WriteLine("Numbers Divisible By Three");
                foreach (var even in numbersDivisibleByThree)
                {
                    Console.WriteLine(even);
                }
            }
        }
    }
}