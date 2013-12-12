#define In
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPro
{

    class Program
    {

        static void Main(string[] args)
        {

            int v = 5;

#if In
            Console.WriteLine(v);
#else
            Console.WriteLine("java");
#endif
            Console.ReadLine();
        }

    }
}
