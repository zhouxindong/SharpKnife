using System;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpKnife.Text;

namespace SharpKnifeTests.Text
{
    [TestClass()]
    public class RegExTests
    {
        [TestMethod()]
        public void VerifyRegexTest()
        {

        }

        [TestMethod]
        public void IntegeRegexTest()
        {
            Assert.IsTrue(RegEx.IntegeRegex.IsMatch("324"));
            Assert.IsTrue(RegEx.IntegeRegex.IsMatch("-12"));
            Assert.IsTrue(RegEx.IntegeRegex.IsMatch("0"));
            Assert.IsTrue(RegEx.IntegeRegex.IsMatch("+1"));

            Assert.IsFalse(RegEx.IntegeRegex.IsMatch("0.0"));
            Assert.IsFalse(RegEx.IntegeRegex.IsMatch("23.47"));
            Assert.IsFalse(RegEx.IntegeRegex.IsMatch("42.0F"));
        }

        [TestMethod]
        public void IpV4RegexTest()
        {
            Assert.IsTrue(RegEx.IpV4Regex.IsMatch("0.0.0.0"));
            Assert.IsTrue(RegEx.IpV4Regex.IsMatch("255.255.255.255"));
            Assert.IsTrue(RegEx.IpV4Regex.IsMatch("192.168.13.3"));
            Assert.IsTrue(RegEx.IpV4Regex.IsMatch("199.199.199.199"));
            Assert.IsTrue(RegEx.IpV4Regex.IsMatch("9.9.9.9"));
            Assert.IsTrue(RegEx.IpV4Regex.IsMatch("99.99.99.99"));

            Assert.IsFalse(RegEx.IpV4Regex.IsMatch("192.168.3."));
            Assert.IsFalse(RegEx.IpV4Regex.IsMatch("192.168.256.3"));
            Assert.IsFalse(RegEx.IpV4Regex.IsMatch("299.299.299.299"));
        }

        [TestMethod]
        public void UrlRegexTest()
        {
            Assert.IsTrue(RegEx.UrlRegex.IsMatch("http://172.16.1.67/index"));
        }

        [TestMethod]
        public void CommonCommentRegexTest()
        {
            var str = "   // this is a comment.\n" +
                      " a line with // charactiers.\n" +
                      "// another comment\n" +
                      "some text\n" +
                      " /* this is a multilines comment.\n" +
                      "this ia the second lines. */\n" +
                      "/* this is a single comment.*/\n" +
                      "end of text";

            var del_comment = RegEx.CommonCommentRegex.Replace(str, "\b");
            Console.WriteLine(del_comment);
        }

        [TestMethod]
        public void EmptyLineRegexTest()
        {
            var sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            sb.Append(Environment.NewLine);
            sb.Append("0123456789");
            sb.Append(Environment.NewLine);
            sb.AppendLine("`");
            sb.AppendLine("~");
            sb.AppendLine("!");
            sb.AppendLine("@");
            sb.AppendLine("#");
            sb.AppendLine("$");
            sb.AppendLine("%");
            sb.AppendLine("^");
            sb.AppendLine("&");
            sb.AppendLine("\t");
            sb.AppendLine("*");
            sb.AppendLine("(");
            sb.AppendLine(")");
            sb.AppendLine("_");
            sb.AppendLine("-");
            sb.AppendLine("+");
            sb.AppendLine("    ");
            sb.AppendLine("=");
            sb.AppendLine(" {");
            sb.AppendLine("}");
            sb.AppendLine("\t[");
            sb.AppendLine("]");
            sb.AppendLine("|");
            sb.AppendLine("\\");
            sb.AppendLine(":");
            sb.AppendLine(";");
            sb.AppendLine("'");
            sb.AppendLine("\"");
            sb.AppendLine("<  ");
            sb.AppendLine("  >   ");
            sb.AppendLine(",");
            sb.AppendLine(".");
            sb.AppendLine("?");
            sb.Append("/");

            var del_empty = RegEx.EmptyLineRegex.Replace(sb.ToString(), MatchEmptyLine);
            Console.WriteLine(del_empty);
        }

        private string MatchEmptyLine(Match match)
        {
            return "EMPTY LINE";//string.Empty;
        }

        [TestMethod]
        public void IniCommentRegexTest()
        {
            var str = "   ; this is a comment.\n" +
          " a line with ; and # charactiers.\n" +
          "# another comment\n" +
          "some text\n" +
          " ;; this is a multilines comment.\n" +
          " ## this ia the second lines. \n" +
          "; this is a single comment.*/\n" +
          "end of text";

            var del_comment = RegEx.IniCommentRegex.Replace(str, "");
            Console.WriteLine(del_comment);
        }

        [TestMethod]
        public void IniSectionRegexTest()
        {
            var str = "  [Server] \n";
            var server = RegEx.IniSectionRegex.Match(str).Groups["SectionName"];
            Assert.AreEqual("Server", server.Value);
            str = "[People]\n";
            Assert.AreEqual("People", RegEx.IniSectionRegex.Match(str).Groups["SectionName"].Value);
        }

        [TestMethod]
        public void IniPropertyNameRegexTest()
        {
            var str = "  Name   = pczx \n";
            string name = RegEx.IniPropertyNameRegex.Match(str).Groups["PropertyName"].Value;
            Assert.AreEqual("Name", name);

            str = " _name = sfsdf\n";
            name = RegEx.IniPropertyNameRegex.Match(str).Groups["PropertyName"].Value;
            Assert.AreEqual("_name", name);

            str = "8Name= sdf \n";
            name = RegEx.IniPropertyNameRegex.Match(str).Groups["PropertyName"].Value;
            Assert.AreEqual("Name", name);

            str = "=safsdf\n";
            name = RegEx.IniPropertyNameRegex.Match(str).Groups["PropertyName"].Value;
            Assert.IsTrue(string.IsNullOrEmpty(name));

            str = " = adsfsf\n";
            Assert.IsTrue(string.IsNullOrEmpty(RegEx.IniPropertyNameRegex.Match(str).Groups["PropertyName"].Value));

            str = "\tName\t= WebRequestMethods.Http proxy host";
            name = RegEx.IniPropertyNameRegex.Match(str).Groups["PropertyName"].Value;
            Assert.AreEqual("Name", name);
        }

        [TestMethod]
        public void IniPropertyValueRegexTest()
        {
            var str = "Name = pczx\n";
            string value = RegEx.IniPropertyValueRegex.Match(str).Groups["PropertyValue"].Value.Trim();
            Assert.AreEqual("pczx", value);

            str = "Name = 0.8345f \n";
            value = RegEx.IniPropertyValueRegex.Match(str).Groups["PropertyValue"].Value.Trim();
            Assert.AreEqual("0.8345f", value);

            str = "Name =   this is a text have spaces    \n";
            value = RegEx.IniPropertyValueRegex.Match(str).Groups["PropertyValue"].Value.Trim();
            Assert.AreEqual("this is a text have spaces", value);

            str = "=";
            value = RegEx.IniPropertyValueRegex.Match(str).Groups["PropertyValue"].Value.Trim();
            Assert.IsTrue(string.IsNullOrEmpty(value));

            str = "\tName\t= WebRequestMethods.Http proxy host";
            value = RegEx.IniPropertyValueRegex.Match(str).Groups["PropertyValue"].Value.Trim();
            Assert.AreEqual("WebRequestMethods.Http proxy host", value);

        }
    }
} 