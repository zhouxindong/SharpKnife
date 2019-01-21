using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpKnife.Extensions
{
    public static class StreamEx
    {
        public static IEnumerable<string> Lines(this StreamReader reader)
        {
            var line_txt = reader.ReadLine();
            while (line_txt != null)
            {
                yield return line_txt;
                line_txt = reader.ReadLine();
            }
        }

        public static string AllText(this StreamReader reader)
        {
            var builder = new StringBuilder();
            foreach (var line in reader.Lines())
            {
                builder.AppendLine(line);
            }
            return builder.ToString();
        }
    }
}