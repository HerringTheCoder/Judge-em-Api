using System;
using System.Text;

namespace Core.Helpers
{
    public static class CodeGenerator
    {
        public static string Generate(int length)
        {
            var stringBuilder = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                var flt = random.NextDouble();
                var shift = Convert.ToInt32(Math.Floor(25 * flt));
                var letter = Convert.ToChar(shift + 65);
                stringBuilder.Append(letter);
            }

            return stringBuilder.ToString();
        }
    }
}
