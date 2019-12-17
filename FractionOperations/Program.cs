using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            while (true)
            {
                string input = Console.ReadLine();

                Console.WriteLine(calculator.Calculate(input));
            }
        }

        //Assumptions
        //Input will only contain these characters: numeric digits, _, +, -, *, /, space
        //Each input and expected output numbers lie within the boundaries of Int.Max and Int.min
        //Input will be valid format
        //Only one operation is allowed

        //TDD cases
        //Whole numbers
        //3 + 4, 4-3, 3-4, 5 * 4, 4 / 2, 5 / 2 = 2_1/2, 3 / 5 = 3/5, 31 / 6 = 5_1/6
        //-2 + 3, -2 - -4, -2 * -4, -2 / -5
        //Fractions
        //3_1/2 + 4_2/3, 4_1/3 - 3_2/5, 2_2/3 * 3_3/4, 3_3/7 / 2_2/5
        //4 + 2_5/7, 5_3/5 - 3, 4_5/7 * 2, 4 / 3_5/7, 5_1/3 / 4
        //3_3/5 + 3 * 4_1/2, 3_1/2 - 3 / 3_4/5 * 4_1/9 +9_1/4
        //Edge cases
        //0 * 0, 0 / 3, " ", 1/3 + 1/3, 3_1000/2000 + 2_8/8
        //Invalid cases
        //3 / 0, 3_1/0 + 2, 3_1/-2 + 2, 3_-1/2 + 2

    }
}
