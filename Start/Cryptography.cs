using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace UHQKEK.Start
{
    static class Cryptography
    {
        public static string ToBase64(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static long ToUnixTime(this DateTime time)
        {
            var totalSeconds = (long)(time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return totalSeconds;
        }
        public static string RandomString(string Randomize)
        {
            string After = "";

            // Lists
            string HexList = "123456789abcdef";
            string LowerList = "abcdefghijklmnopqrstuvwxyz";
            string UpperList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string DigitList = "1234567890";
            string SymbolList = "!@#$%^&*()_+";
            string UpperDigitList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string UpperLowerDigitList = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            Random ran = new Random();

            for (int i = 0; i < Randomize.Length - 1; i++)
            {
                // HexaDecimal Random
                if ((Randomize[i].ToString() + Randomize[i + 1].ToString()).Equals("?h"))
                {
                    After += HexList[ran.Next(0, HexList.Length)];
                }
                // LowerCase Random
                else if ((Randomize[i].ToString() + Randomize[i + 1].ToString()).Equals("?l"))
                {
                    After += LowerList[ran.Next(0, LowerList.Length)];
                }
                // UpperCase Random
                else if ((Randomize[i].ToString() + Randomize[i + 1].ToString()).Equals("?u"))
                {
                    After += UpperList[ran.Next(0, UpperList.Length)];
                }
                // Digit Random
                else if ((Randomize[i].ToString() + Randomize[i + 1].ToString()).Equals("?d"))
                {
                    After += DigitList[ran.Next(0, DigitList.Length)];
                }
                // Upper Digit Random
                else if ((Randomize[i].ToString() + Randomize[i + 1].ToString()).Equals("?m"))
                {
                    After += UpperDigitList[ran.Next(0, UpperDigitList.Length)];
                }
                // Upper Lower Digit Random
                else if ((Randomize[i].ToString() + Randomize[i + 1].ToString()).Equals("?i"))
                {
                    After += UpperLowerDigitList[ran.Next(0, UpperLowerDigitList.Length)];
                }
                else if ((Randomize[i].ToString() + Randomize[i + 1].ToString()).Equals("?s"))
                {
                    After += SymbolList[ran.Next(0, SymbolList.Length)];
                }
                // Dash Separators
                else if ((Randomize[i].ToString().Contains("-")))
                {
                    After += "-";
                }
                // Incase there's a static number/letter here, we keep it the same
                else if (Randomize[i - 1].ToString().Equals("-") && !Randomize[i].ToString().Equals("?"))
                {
                    After += Randomize[i].ToString();
                }

            }

            return After;
        }
        public static IEnumerable<string> LR(string input, string left, string right, bool recursive = false, bool useRegex = false)
        {
            if (left == string.Empty && right == string.Empty)
            {
                return new string[] { input };
            }
            else if (((left != string.Empty && !input.Contains(left)) || (right != string.Empty && !input.Contains(right))))
            {
                return new string[] { };
            }

            var partial = input;
            var pFrom = 0;
            var pTo = 0;
            var list = new List<string>();

            if (recursive)
            {
                if (useRegex)
                {
                    try
                    {
                        var pattern = BuildLRPattern(left, right);
                        MatchCollection mc = Regex.Matches(partial, pattern);
                        foreach (Match m in mc)
                            list.Add(m.Value);
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        while (left == string.Empty || (partial.Contains(left)) && (right == string.Empty || partial.Contains(right)))
                        {
                            // Search for left delimiter and Calculate offset
                            pFrom = left == string.Empty ? 0 : partial.IndexOf(left) + left.Length;
                            // Move right of offset
                            partial = partial.Substring(pFrom);
                            // Search for right delimiter and Calculate length to parse
                            pTo = right == string.Empty ? (partial.Length - 1) : partial.IndexOf(right);
                            // Parse it
                            var parsed = partial.Substring(0, pTo);
                            list.Add(parsed);
                            // Move right of parsed + right
                            partial = partial.Substring(parsed.Length + right.Length);
                        }
                    }
                    catch { }
                }
            }

            // Non-recursive
            else
            {
                if (useRegex)
                {
                    var pattern = BuildLRPattern(left, right);
                    MatchCollection mc = Regex.Matches(partial, pattern);
                    if (mc.Count > 0) list.Add(mc[0].Value);
                }
                else
                {
                    try
                    {
                        pFrom = left == string.Empty ? 0 : partial.IndexOf(left) + left.Length;
                        partial = partial.Substring(pFrom);
                        pTo = right == string.Empty ? partial.Length : partial.IndexOf(right);
                        list.Add(partial.Substring(0, pTo));
                    }
                    catch { }
                }
            }

            return list;
        }

        public static IEnumerable<string> JSON(string input, string field, bool recursive = false, bool useJToken = false)
        {
            var list = new List<string>();

            if (useJToken)
            {
                if (recursive)
                {
                    if (input.Trim().StartsWith("["))
                    {
                        JArray json = JArray.Parse(input);
                        var jsonlist = json.SelectTokens(field, false);
                        foreach (var j in jsonlist)
                            list.Add(j.ToString());
                    }
                    else
                    {
                        JObject json = JObject.Parse(input);
                        var jsonlist = json.SelectTokens(field, false);
                        foreach (var j in jsonlist)
                            list.Add(j.ToString());
                    }
                }
                else
                {
                    if (input.Trim().StartsWith("["))
                    {
                        JArray json = JArray.Parse(input);
                        list.Add(json.SelectToken(field, false).ToString());
                    }
                    else
                    {
                        JObject json = JObject.Parse(input);
                        list.Add(json.SelectToken(field, false).ToString());
                    }
                }
            }
            else
            {
                var jsonlist = new List<KeyValuePair<string, string>>();
                parseJSON("", input, jsonlist);
                foreach (var j in jsonlist)
                    if (j.Key == field)
                        list.Add(j.Value);

                if (!recursive && list.Count > 1) list = new List<string>() { list.First() };
            }

            return list;
        }

        /// <summary>
        /// Parses a string via a Regex pattern containing Groups, then returns them according to an output format.
        /// </summary>
        /// <param name="input">The string to parse</param>
        /// <param name="pattern">The Regex pattern containing groups</param>
        /// <param name="output">The output format string, for which [0] will be replaced with the full match, [1] with the first group etc.</param>
        /// <param name="recursive">Whether to make multiple matches</param>
        /// <param name="options">The Regex Options to use</param>
        /// <returns>The parsed string(s).</returns>
        public static IEnumerable<string> REGEX(string input, string pattern, string output, bool recursive = false, RegexOptions? options = null)
        {
            var list = new List<string>();
            if (!options.HasValue) options = new RegexOptions();

            if (recursive)
            {
                var matches = Regex.Matches(input, pattern, options.Value);
                foreach (Match match in matches)
                {
                    var final = output;
                    for (var i = 0; i < match.Groups.Count; i++) final = final.Replace("[" + i + "]", match.Groups[i].Value);
                    list.Add(final);
                }
            }
            else
            {
                var match = Regex.Match(input, pattern, options.Value);
                if (match.Success)
                {
                    var final = output;
                    for (var i = 0; i < match.Groups.Count; i++) final = final.Replace("[" + i + "]", match.Groups[i].Value);
                    list.Add(final);
                }
            }

            return list;
        }

        private static string BuildLRPattern(string ls, string rs)
        {
            var left = string.IsNullOrEmpty(ls) ? "^" : Regex.Escape(ls); // Empty LEFT = start of the line
            var right = string.IsNullOrEmpty(rs) ? "$" : Regex.Escape(rs); // Empty RIGHT = end of the line
            return "(?<=" + left + ").+?(?=" + right + ")";
        }

        private static void parseJSON(string A, string B, List<KeyValuePair<string, string>> jsonlist)
        {
            jsonlist.Add(new KeyValuePair<string, string>(A, B));

            if (B.StartsWith("["))
            {
                JArray arr = null;
                try { arr = JArray.Parse(B); } catch { return; }

                foreach (var i in arr.Children())
                    parseJSON("", i.ToString(), jsonlist);
            }

            if (B.Contains("{"))
            {
                JObject obj = null;
                try { obj = JObject.Parse(B); } catch { return; }

                foreach (var o in obj)
                    parseJSON(o.Key, o.Value.ToString(), jsonlist);
            }
        }
    }
}
