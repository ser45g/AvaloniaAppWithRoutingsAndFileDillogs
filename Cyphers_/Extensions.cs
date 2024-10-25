using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyphers
{
    internal static class Extensions
    {
        public static string ReplaceAt(this string input, int index, char newChar)
        {

            if (input == null)
            {
                throw new ArgumentNullException("The argument <input> was null");
            }
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}
