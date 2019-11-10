using System;

namespace FractionCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().RunTestCases();

            string example = "Example Input: ? 2_3/8 + 9/8\nEnter 'exit' to exit the program";
            Console.WriteLine(example);

            while (true)
            {
                string line = Console.ReadLine();

                if (line.ToLower().Trim().Equals("exit"))
                    break;

                if (!line.StartsWith('?'))
                {
                    Console.WriteLine(example);
                }
                else
                {
                    string input1, input2, op;
                    if (Expression.IsValidExpression(line.Trim('?'), out input1, out input2, out op))
                    {
                        try
                        {
                            Fraction f1 = new Fraction(input1.Trim());
                            Fraction f2 = new Fraction(input2.Trim());
                            Console.WriteLine(Expression.Calc(f1, f2, op[0]));
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                            Console.WriteLine(example);
                        }
                    }
                }
            }
        }

        public void RunTestCases()
        {
            string input1, input2, op;
            Fraction f1, f2;

            string fractionString = "1/2 * 3_3/4";
            if (Expression.IsValidExpression(fractionString, out input1, out input2, out op))
            {
                f1 = new Fraction(input1.Trim());
                f2 = new Fraction(input2.Trim());
                Console.WriteLine(string.Format("{0} = {1}", fractionString, Expression.Calc(f1, f2, op[0])));
            }

            f1 = new Fraction(1, 2);
            f2 = new Fraction(3, 3, 4);
            Console.WriteLine(string.Format("{0} {1} {2} = {3}", f1.ToString(), "*", f2.ToString(), (f1 * f2).ToString()));

            f1 = new Fraction(1, 2);
            f2 = new Fraction(15, 4);
            Console.WriteLine(string.Format("{0} {1} {2} = {3}", f1.ToString(), "*", f2.ToString(), (f1 * f2).ToString()));

            fractionString = "2_3/8 + 9/8";
            if (Expression.IsValidExpression(fractionString, out input1, out input2, out op))
            {
                f1 = new Fraction(input1.Trim());
                f2 = new Fraction(input2.Trim());
                Console.WriteLine(string.Format("{0} = {1}", fractionString, Expression.Calc(f1, f2, op[0])));
            }

            f1 = new Fraction(2, 3, 8);
            f2 = new Fraction(9, 8);
            Console.WriteLine(string.Format("{0} {1} {2} = {3}", f1.ToString(), "+", f2.ToString(), Expression.Calc(f1, f2, '+')));

            f1 = new Fraction(19, 8);
            f2 = new Fraction(9, 8);
            Console.WriteLine(string.Format("{0} {1} {2} = {3}", f1.ToString(), "+", f2.ToString(), Expression.Calc(f1, f2, '+')));

            fractionString = "1/2 / 3_3/4";
            if (Expression.IsValidExpression(fractionString, out input1, out input2, out op))
            {
                f1 = new Fraction(input1.Trim());
                f2 = new Fraction(input2.Trim());
                Console.WriteLine(string.Format("{0} = {1}", fractionString, Expression.Calc(f1, f2, op[0])));
            }

            fractionString = "1/2 - 3_3/4";
            if (Expression.IsValidExpression(fractionString, out input1, out input2, out op))
            {
                f1 = new Fraction(input1.Trim());
                f2 = new Fraction(input2.Trim());
                Console.WriteLine(string.Format("{0} = {1}", fractionString, Expression.Calc(f1, f2, op[0])));
            }

            fractionString = "-1/2 - 3_3/4";
            if (Expression.IsValidExpression(fractionString, out input1, out input2, out op))
            {
                f1 = new Fraction(input1.Trim());
                f2 = new Fraction(input2.Trim());
                Console.WriteLine(string.Format("{0} = {1}", fractionString, Expression.Calc(f1, f2, op[0])));
            }

            fractionString = "-1/2 + 3_3/4";
            if (Expression.IsValidExpression(fractionString, out input1, out input2, out op))
            {
                f1 = new Fraction(input1.Trim());
                f2 = new Fraction(input2.Trim());
                Console.WriteLine(string.Format("{0} = {1}", fractionString, Expression.Calc(f1, f2, op[0])));
            }
        }
    }
}
