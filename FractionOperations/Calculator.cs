using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionOperations
{
    public class Calculator : ICalculator
    {
        public string Calculate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            try
            {
                var items = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (items.Length != 3)
                {
                    throw new ArgumentException("Invalid input");
                }

                var fraction1 = ConvertToFraction(items[0]);
                var fraction2 = ConvertToFraction(items[2]);

                var operand = items[1];

                return ComputeFractions(fraction1, fraction2, operand);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string ComputeFractions(Fraction fraction1, Fraction fraction2, string op)
        {
            List<string> validOperators = new List<string>() { "+", "-", "*", "/" };
            if (!validOperators.Contains(op))
                throw new ArgumentException("Invalid input");

            Fraction result = null;

            switch (op)
            {
                case "+":
                    result = AddFractions(fraction1, fraction2);
                    break;
                case "-":
                    result = SubtractFractions(fraction1, fraction2);
                    break;
                case "*":
                    result = MultiplyFractions(fraction1, fraction2);
                    break;
                case "/":
                    result = DivideFractions(fraction1, fraction2);
                    break;
            }

            return ConvertToOutputFormat(result);
        }

        private Fraction DivideFractions(Fraction fraction1, Fraction fraction2)
        {
            if (fraction2.Numerator == 0)
                throw new Exception("Invalid Operation");

            if (fraction1.Numerator == 0)
                return new Fraction(0, 1);

            int sign = fraction2.Numerator < 0 ? -1 : 1;

            return new Fraction(sign * fraction1.Numerator * fraction2.Denominator,
                sign * fraction1.Denominator * fraction2.Numerator);
        }

        private Fraction MultiplyFractions(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction(fraction1.Numerator * fraction2.Numerator, fraction1.Denominator * fraction2.Denominator);
        }

        private Fraction SubtractFractions(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction(fraction1.Numerator * fraction2.Denominator - fraction1.Denominator * fraction2.Numerator,
                fraction1.Denominator * fraction2.Denominator);
        }

        private Fraction AddFractions(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction(fraction1.Numerator * fraction2.Denominator + fraction1.Denominator * fraction2.Numerator,
                fraction1.Denominator * fraction2.Denominator);
        }

        private Fraction ConvertToFraction(string input)
        {
            var number = input;
            int wholeNumber = 0, numerator = 0, denominator = 1;
            bool isNegative = false;

            if (input.StartsWith("-"))
            {
                isNegative = true;
                number = input.Substring(1);
            }

            string fraction = string.Empty;

            if (number.Contains("_"))
            {
                var items = number.Split('_');

                if (items.Length > 2)
                    throw new ArgumentException("Invalid input");

                if (!int.TryParse(items[0], out wholeNumber))
                    throw new ArgumentException("Invalid input");

                fraction = items[1];
            }
            else
            {
                fraction = number;
            }

            var fractionComponents = fraction.Split('/');

            if (fractionComponents.Length > 2)
            {
                throw new ArgumentException("Invalid input");
            }

            if (!int.TryParse(fractionComponents[0], out numerator)
                || numerator < 0)
                throw new ArgumentException("Invalid input");

            if (fractionComponents.Length > 1)
            {
                if (!int.TryParse(fractionComponents[1], out denominator)
                    || denominator <= 0)
                    throw new ArgumentException("Invalid input");
            }

            numerator += wholeNumber * denominator;

            if (isNegative)
                numerator *= -1;

            return new Fraction(numerator, denominator);
        }

        private string ConvertToOutputFormat(Fraction fraction)
        {
            int sign = fraction.Numerator < 0 ? -1 : 1;

            int wholeNumber = 0, numerator = fraction.Numerator * sign, denominator = fraction.Denominator;

            if (denominator == 1 || numerator % denominator == 0)
                return (fraction.Numerator / fraction.Denominator).ToString();

            wholeNumber = numerator / denominator * sign;
            numerator = numerator % denominator;

            List<int> divisors = new List<int>();
            for (int i = 2; i <= (denominator + 1) / 2; i++)
            {
                bool toBeEvaluated = true;
                foreach (var item in divisors)
                {
                    if (item % i == 0)
                    {
                        toBeEvaluated = false;
                        break;
                    }
                }

                while (toBeEvaluated && numerator % i == 0 && denominator % i == 0)
                {
                    numerator = numerator / i;
                    denominator = denominator / i;
                }

                if (denominator == 1)
                    return numerator.ToString();
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(wholeNumber == 0 ? string.Empty : $"{wholeNumber}_");
            sb.Append($"{numerator}/{denominator}");

            return sb.ToString();
        }
    }
}
