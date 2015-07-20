using System.Text;

namespace GitFirstApp.Models.My_Models
{
    public class StringUtilities
    {

        /// <summary>
        /// Produces optional, URL-friendly version of a title, "like-this-one". 
        /// hand-tuned for speed, reflects performance refactoring contributed
        /// by John Gietzen (user otac0n) 
        /// </summary>
        public static string UrlFriendly(string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            var len = title.Length;
            var prevdash = false;
            var sb = new StringBuilder(len);

            for (var i = 0; i < len; i++)
            {
                var c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else switch (c)
                {
                    case ' ':
                    case ',':
                    case '.':
                    case '/':
                    case '\\':
                    case '-':
                    case '_':
                    case '=':
                        if (!prevdash && sb.Length > 0)
                        {
                            sb.Append('-');
                            prevdash = true;
                        }
                        break;
                    case '#':
                        if (i > 0)
                            if (title[i - 1] == 'C' || title[i - 1] == 'F')
                                sb.Append("-sharp");
                        break;
                    case '+':
                        sb.Append("-plus");
                        break;
                    default:
                        if ((int) c >= 128)
                        {
                            var prevlen = sb.Length;
                            sb.Append(RemapInternationalCharToAscii(c));
                            if (prevlen != sb.Length) prevdash = false;
                        }
                        break;
                }
                if (sb.Length == maxlen) break;
            }

            return prevdash ? sb.ToString().Substring(0, sb.Length - 1) : sb.ToString();
        }

        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }
    }
}