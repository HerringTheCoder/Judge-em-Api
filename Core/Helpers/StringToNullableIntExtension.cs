using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helpers
{
    public static class StringToNullableIntExtension
    {
        public static int? ToNullableInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i))
                return i;

            return null;
        }
    }
}
