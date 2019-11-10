using System;
using System.Text.RegularExpressions;

namespace FractionCLI
{
    public class Fraction
    {
        public int Whole { get; set; }        
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fraction">fraction string</param>
        public Fraction(string fraction)
        {
            ParseFraction(fraction);
            MixedtoImproper(Whole, Numerator, Denominator);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="num">Whole Number</param>
        public Fraction(int num) : this(num, 0 , 1) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="num">Numerator</param>
        /// <param name="den">Denominator</param>
        public Fraction(int num, int den) : this(0, num, den) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="whole">Whole number</param>
        /// <param name="num">Numerator</param>
        /// <param name="den">Denominator</param>
        public Fraction(int whole, int num, int den)
        {
            if (den == 0)
                throw new Exception("Denominator cannot be 0, divide by 0 error!");

            Whole = whole;
            Numerator = num;
            Denominator = den;

            MixedtoImproper(whole, num, den);
            Simplify();
        }

        /// <summary>
        /// Overload the + operator
        /// </summary>
        /// <param name="f1">Fraction on the left side of the formula</param>
        /// <param name="f2">Fraction on the right side of formula</param>
        /// <returns>Result fraction</returns>
        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            Fraction sum = new Fraction
            (
                f1.Numerator * f2.Denominator + f2.Numerator * f1.Denominator,
                f1.Denominator * f2.Denominator
            );

            sum.Simplify();
            return sum;
        }

        /// <summary>
        /// Overload the - operator
        /// </summary>
        /// <param name="f1">Fraction on the left side of the formula</param>
        /// <param name="f2">Fraction on the right side of formula</param>
        /// <returns>Result fraction</returns>
        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            Fraction difference = new Fraction
            (
                f1.Numerator * f2.Denominator - f2.Numerator * f1.Denominator,
                f1.Denominator * f2.Denominator
            );

            difference.Simplify();
            return difference;
        }

        /// <summary>
        /// Overload the * operator
        /// </summary>
        /// <param name="f1">Fraction on the left side of the formula</param>
        /// <param name="f2">Fraction on the right side of formula</param>
        /// <returns>Result fraction</returns>
        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            Fraction product = new Fraction
            (
                f1.Numerator * f2.Numerator,
                f1.Denominator * f2.Denominator
            );

            product.Simplify();
            return product;
        }
        /// <summary>
        /// Overload the / operator
        /// </summary>
        /// <param name="f1">Fraction on the left side of the formula</param>
        /// <param name="f2">Fraction on the right side of formula</param>
        /// <returns>Result fraction</returns>
        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            Fraction quotient = new Fraction
            (
                f1.Numerator * f2.Denominator,
                f1.Denominator * f2.Numerator
            );

            quotient.Simplify();
            return quotient;
        }

        /// <summary>
        /// Find the greatest common denominator
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        /// <returns></returns>
        public int GCD(int num1, int num2)
        {
            num1 = Math.Abs(num1);
            num2 = Math.Abs(num2);

            if (num2 == 0)
                return num1;

            while (num1 != num2)
                if (num1 < num2)
                    num2 = num2 - num1;
                else
                    num1 = num1 - num2;

            return num1;
        }

        /// <summary>
        /// Simplify the fraction using GCD
        /// </summary>
        public void Simplify()
        {
            int gcd = GCD(Numerator, Denominator);
            Numerator = Numerator / gcd;
            Denominator = Denominator / gcd;

            // invert the sign
            if (Denominator < 0)
            {
                Denominator = -1 * Denominator;
                Numerator = -1 * Numerator;
            }
        }

        /// <summary>
        /// Convert improper fraction to mixed
        /// </summary>
        /// <param name="num">Numerator</param>
        /// <param name="den">Denominator</param>
        private void ImpropertoMixed(int num, int den)
        {
            if (Math.Abs(num) > Math.Abs(den))
            {
                Whole = num / den;
                Numerator = Numerator % Denominator;

                if (Numerator < 0)
                    Numerator *= -1;
            }
        }

        /// <summary>
        /// Convert mixed fraction to improper
        /// </summary>
        /// <param name="whole">Whole number</param>
        /// <param name="num">Numerator</param>
        /// <param name="den">Denominator</param>
        private void MixedtoImproper(int whole, int num, int den)
        {
            if (Math.Abs(whole) > 0)
            {
                Whole = 0;
                Numerator = den * Math.Abs(whole) + num;
                Denominator *= (whole > 0 ? 1 : -1);
            }
        }

        /// <summary>
        /// Parse the fraction into properties
        /// </summary>
        /// <param name="fraction">Fraction string</param>
        public void ParseFraction(string fraction)
        {
            GroupCollection groups;
            if (IsFraction(fraction, out groups))
            {
                // Always has count of 6 due to the regex
                if (groups.Count == 6)
                {
                    // Fraction + Whole
                    if (!string.IsNullOrEmpty(groups[5].Value))
                    {
                        Whole = int.Parse(groups[1].Value);
                        Numerator = int.Parse(groups[4].Value);
                        Denominator = int.Parse(groups[5].Value);
                    }
                    // Fraction
                    else if (string.IsNullOrEmpty(groups[5].Value) && !string.IsNullOrEmpty(groups[3].Value))
                    {
                        Whole = 0;
                        Numerator = int.Parse(groups[1].Value);
                        Denominator = int.Parse(groups[3].Value);
                    }
                    // Whole
                    else if (string.IsNullOrEmpty(groups[5].Value) && string.IsNullOrEmpty(groups[3].Value) && !string.IsNullOrEmpty(groups[1].Value))
                    {
                        Whole = int.Parse(groups[1].Value);
                        Numerator = 0;
                        Denominator = 1;
                    }
                }
            }
            else
                throw new Exception("Not a fraction exception!");
        }

        /// <summary>
        /// Check if the string is a fraction
        /// </summary>
        /// <param name="fraction">Fraction string</param>
        /// <param name="groups">Regex group collection</param>
        /// <returns></returns>
        public static bool IsFraction(string fraction, out GroupCollection groups)
        {
            string fractionPattern = @"^\s*(\d+)(\s*\/(\d*)|_+(\d+)\s*\/\s*(\d+))?\s*$";
            Regex regex = new Regex(fractionPattern, RegexOptions.IgnoreCase);
            
            Match m = regex.Match(fraction);
            groups = m.Groups;

            return m.Success;
        }

        /// <summary>
        /// Print the faction as string
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            ImpropertoMixed(Numerator, Denominator);
            string ret = Numerator != 0 ? (Whole != 0 ? $"{Whole}_{Numerator}/{Denominator}" : $"{Numerator}/{Denominator}") : Whole.ToString();

            // convert back to improper for calc
            MixedtoImproper(Whole, Numerator, Denominator);
            return ret;
        }
    }
}
