using System.Text.RegularExpressions;

namespace Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string ToReadableString(this Enum value)
        {
            string str = value.ToString();
            return Regex.Replace(str, "(\\B[A-Z])", " $1");
        }
    }
}
