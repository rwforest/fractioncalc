using System;
using System.Text.RegularExpressions;

namespace FractionCLI
{
    public class Expression
    {
        /// <summary>
        /// Check if it is a valid expression
        /// </summary>
        /// <param name="expression">Expression string</param>
        /// <param name="input1">First input</param>
        /// <param name="input2">Second input</param>
        /// <param name="op">operator</param>
        /// <returns>Whether its a valid expression</returns>
        public static bool IsValidExpression(string expression, out string input1, out string input2, out string op)
        {
            op = input1 = input2 = string.Empty;

            // assuming only two values
            string expressionPattern = @".+\s+(\+|-|\*|\/)\s+.+";

            Regex regex = new Regex(expressionPattern, RegexOptions.IgnoreCase);

            Match m = regex.Match(expression);

            if (m.Success)
            {
                input1 = expression.Split(m.Groups[1].Value)[0];
                input2 = expression.Split(m.Groups[1].Value)[1];
                op = m.Groups[1].Value;
            }

            return m.Success;
        }

        /// <summary>
        /// Calculate the expression
        /// </summary>
        /// <param name="f1">First fraction</param>
        /// <param name="f2">Second fraction</param>
        /// <param name="op">Operator</param>
        /// <returns>Result string</returns>
        public static string Calc(Fraction f1, Fraction f2, char op)
        {
            switch(op)
            {
                case '+':
                    return (f1 + f2).ToString();
                case '-':
                    return (f1 - f2).ToString();
                case '*':
                    return (f1 * f2).ToString();
                case '/':
                    return (f1 / f2).ToString();
                default:
                    throw new Exception("Operator not found exception!");

            }
        }
    }
}
