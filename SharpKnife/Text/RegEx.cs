using System.Text.RegularExpressions;

namespace SharpKnife.Text
{
    public static class RegEx
    {
        /// <summary>
        /// 整数
        /// </summary>
        public static Regex IntegeRegex = new Regex(@"^((\+|-)\d)?\d*$", RegexOptions.Compiled);

        /// <summary>
        /// IP: [0-255]...
        /// </summary>
        public static Regex IpV4Regex =
            new Regex(@"^(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))$",
                RegexOptions.Compiled);

        /// <summary>
        /// URL部分，不包括查询数据
        /// </summary>
        public static Regex UrlRegex = new Regex(@"https?://[-\w.]+(:\d+)?(/([\w/_.]*)?)?", RegexOptions.Compiled);

        /// <summary>
        /// //开头或/*...*/
        /// </summary>
        public static Regex CommonCommentRegex = new Regex(
            @"(^\s*//.*$)|(/\*[^(\*/)]+\*/)", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        /// 只有空格的行
        /// </summary>
        public static Regex EmptyLineRegex = new Regex(
            @"^[^\w`~!@#\$%&\^\*\(\)\-_\+=\{\}\|\\\[\]:;',\.\?<>/" + "\"" + "]*$",
            RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        /// ;或#开头的行
        /// </summary>
        public static Regex IniCommentRegex = new Regex(@"^\s*[;#].*$", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        /// INI文件 [SectionName]
        /// </summary>
        public static Regex IniSectionRegex = new Regex(@"\[(?<SectionName>[^\]]+)", RegexOptions.Compiled);

        /// <summary>
        /// INI文件 PropertyName = PropertyValue
        /// </summary>
        public static Regex IniPropertyNameRegex = new Regex(@"([\s]*)(?<PropertyName>([a-zA-Z_]{1}[\w_]*))([\s]*)(?=\=)",
            RegexOptions.Compiled | RegexOptions.Singleline);

        public static Regex IniPropertyValueRegex =
            new Regex(
                @"(?<=\=)([\s])*(?<PropertyValue>[\w`~!@#\$%&\^\*\(\)\-_\+=\{\}\|\\\[\]:;',\.\?<> /" + "\"" + "]*)" +
                @"(\r|\n)*$", RegexOptions.Compiled | RegexOptions.Singleline);
    }
}