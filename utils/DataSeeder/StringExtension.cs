using System.Collections.Generic;
using System.Text;

namespace DataSeeder
{
    public static class StringExtension
    {
        public static List<string> GetData(this string str, char separator)
        {
            var result = new List<string>();

            var builder = new StringBuilder();
            var commasOpened = false;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == separator && !commasOpened && builder.Length > 0)
                {
                    result.Add(builder.ToString());
                    builder.Clear();
                }
                else if (str[i] == '"')
                {
                    commasOpened = !commasOpened;
                }
                else
                {
                    builder.Append(str[i]);
                }
            }

            if (builder.Length > 0)
            {
                result.Add(builder.ToString());
            }

            return result;
        }
    }
}