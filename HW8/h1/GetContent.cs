using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;
/*
 思路：
1、抓取远程网页源码，这里要实现自动判断网页编码，否则有可能抓到乱码。我是先看应答的 http头的chareset，一般这个很准，但像csdn的新闻比较变态http应答的头里的chareset和网页的meta里声明的 chareset不一致，所以我手工加了一下判断，如果不一致再在内存流里用网页声明的编码读取一遍源码
2、把网页分割成几大块。试用了一下tidy的.net包装及HtmlParse的.net版本，都不太好用。于是我自己写了个算法，可以把网页里的div块，td块等都提取出来，支持嵌套的情况。一般只提取div的文字块儿就行了。
3、把汉字少于200的文本块去了，一般少于200字的文本块不会是正文，即便是正文，一般来说也不会有太多的价值，我直接去掉。
4、 因为div支持嵌套，所以剩下的文本块，有可能是重复的，一个是另一个的父节点，所以要把最里层的文本块找出来，最里层的文本块肯定是汉字最多的，而其它 文本最少的，所以要计算出剩余文本块中汉字占所有字符比例最高的文本块，基本上它就是正文的文本块了。当然有的网页正文里也可能还有div的文本块，这时 候可能会判断错误，但只要正文嵌套的Div文本块的汉字少于200字，我的算法还是能准确提取正文文本块的。这一步我用写了一个自定义的方法传递给 List的Sort方法。
5、把<p><br>等标签替换成特殊占位符[p][br]等，因为最终的正文需要保留段落和回车换行等格式。这一步用正则实现。
6、把最后剩下的文本块的html标签去掉，我用正则过滤的。
7、把[p]替换成回车换行加俩空格，把[br]替换成回车换行，这步也用正则。到此，正文提取完毕
 */
namespace h1
{
    public class GetMainContentHelper
    {
        ///<summary>
        /// 判断两段儿文本里哪个中文占的比例高
        ///</summary>
        ///<param name="x"></param>
        ///<param name="y"></param>
        ///<returns></returns>
        public static int CompareDinosByChineseLength(string x, string y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    Regex r = new Regex("[\u4e00-\u9fa5]");
                    float xCount = (float)(r.Matches(x).Count) / (float)x.Length;
                    float yCount = (float)(r.Matches(y).Count) / (float)y.Length;

                    int retval = xCount.CompareTo(yCount);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    {
                        return x.CompareTo(y);
                    }
                }
            }
        }

        ///<summary>
        /// 获取一个网页源码中的标签列表，支持嵌套，一般或去div，td等容器
        ///</summary>
        ///<param name="input"></param>
        ///<param name="tag"></param>
        ///<returns></returns>
        public static List<string> GetTags(string input, string tag)
        {
            StringReader strReader = new StringReader(input);
            int lowerThanCharCounter = 0;
            int lowerThanCharPos = 0;
            Stack<int> tagPos = new Stack<int>();
            List<string> taglist = new List<string>();
            int i = 0;
            while (true)
            {
                try
                {
                    int intCharacter = strReader.Read();
                    if (intCharacter == -1) break;

                    char convertedCharacter = Convert.ToChar(intCharacter);

                    if (lowerThanCharCounter > 0)
                    {
                        if (convertedCharacter == '>')
                        {
                            lowerThanCharCounter--;

                            string biaoqian = input.Substring(lowerThanCharPos, i - lowerThanCharPos + 1);
                            if (biaoqian.StartsWith(string.Format("<{0}", tag)))
                            {
                                tagPos.Push(lowerThanCharPos);
                            }
                            if (biaoqian.StartsWith(string.Format("</{0}", tag)))
                            {
                                if (tagPos.Count < 1)
                                    continue;
                                int tempTagPos = tagPos.Pop();
                                string strdiv = input.Substring(tempTagPos, i - tempTagPos + 1);
                                taglist.Add(strdiv);
                            }
                        }
                    }

                    if (convertedCharacter == '<')
                    {
                        lowerThanCharCounter++;
                        lowerThanCharPos = i;
                    }
                }
                finally
                {
                    i++;
                }
            }
            return taglist;
        }

        ///<summary>
        /// 获取指定网页的源码，支持编码自动识别
        ///</summary>
        ///<param name="url"></param>
        ///<returns></returns>
        public static string getDataFromUrl(string url)
        {
            string str = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            //设置http头
            request.AllowAutoRedirect = true;
            request.AllowWriteStreamBuffering = true;
            request.Referer = "";
            request.Timeout = 10 * 1000;
            request.UserAgent = "";

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //根据http应答的http头来判断编码
                    string characterSet = response.CharacterSet;
                    Encoding encode;
                    if (characterSet != "")
                    {
                        if (characterSet == "ISO-8859-1")
                        {
                            characterSet = "gb2312";
                        }
                        encode = Encoding.GetEncoding(characterSet);
                    }
                    else
                    {
                        encode = Encoding.Default;
                    }

                    //声明一个内存流来保存http应答流
                    Stream receiveStream = response.GetResponseStream();
                    MemoryStream mStream = new MemoryStream();

                    byte[] bf = new byte[255];
                    int count = receiveStream.Read(bf, 0, 255);
                    while (count > 0)
                    {
                        mStream.Write(bf, 0, count);
                        count = receiveStream.Read(bf, 0, 255);
                    }
                    receiveStream.Close();

                    mStream.Seek(0, SeekOrigin.Begin);

                    //从内存流里读取字符串
                    StreamReader reader = new StreamReader(mStream, encode);
                    char[] buffer = new char[1024];
                    count = reader.Read(buffer, 0, 1024);
                    while (count > 0)
                    {
                        str += new String(buffer, 0, count);
                        count = reader.Read(buffer, 0, 1024);
                    }

                    //从解析出的字符串里判断charset，如果和http应答的编码不一直
                    //那么以页面声明的为准，再次从内存流里重新读取文本
                    Regex reg =
                        new Regex(@"<meta[\s\S]+?charset=(.*)""[\s\S]+?>",
                                  RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    MatchCollection mc = reg.Matches(str);
                    if (mc.Count > 0)
                    {
                        string tempCharSet = mc[0].Result("$1");
                        if (string.Compare(tempCharSet, characterSet, true) != 0)
                        {
                            encode = Encoding.GetEncoding(tempCharSet);
                            str = string.Empty;
                            mStream.Seek(0, SeekOrigin.Begin);
                            reader = new StreamReader(mStream, encode);
                            buffer = new char[255];
                            count = reader.Read(buffer, 0, 255);
                            while (count > 0)
                            {
                                str += new String(buffer, 0, count);
                                count = reader.Read(buffer, 0, 255);
                            }
                        }
                    }
                    reader.Close();
                    mStream.Close();
                }
            }
            catch (Exception ex)
            {
                //Trace.TraceError(ex.ToString());
                { Console.WriteLine(ex.Message); return ""; }
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
            return str;
        }

        ///<summary>
        /// 从一段网页源码中获取正文
        ///</summary>
        ///<param name="input"></param>
        ///<returns></returns>
        public static string GetMainContent(string input)
        {
            string reg1 = @"<(p|br)[^<]*>";
            string reg2 =
                @"(\[([^=]*)(=[^\]]*)?\][\s\S]*?\[/\1\])|(?<lj>(?=[^\u4E00-\u9FA5\uFE30-\uFFA0,."");])<a\s+[^>]*>[^<]{2,}</a>(?=[^\u4E00-\u9FA5\uFE30-\uFFA0,."");]))|(?<Style><style[\s\S]+?/style>)|(?<select><select[\s\S]+?/select>)|(?<Script><script[\s\S]*?/script>)|(?<Explein><\!\-\-[\s\S]*?\-\->)|(?<li><li(\s+[^>]+)?>[\s\S]*?/li>)|(?<Html></?\s*[^> ]+(\s*[^=>]+?=['""]?[^""']+?['""]?)*?[^\[<]*>)|(?<Other>&[a-zA-Z]+;)|(?<Other2>\#[a-z0-9]{6})|(?<Space>\s+)|(\&\#\d+\;)";

            //1、获取网页的所有div标签
            List<string> list = GetTags(input, "div");

            //2、去除汉字较少的div
            List<string> needToRemove = new List<string>();
            foreach (string s in list)
            {
                Regex r = new Regex("[\u4e00-\u9fa5]");
                if (r.Matches(s).Count < 300)
                {
                    needToRemove.Add(s);
                }
            }
            foreach (string s in needToRemove)
            {
                list.Remove(s);
            }

            //3、把剩下的div按汉字比例多少倒序排列,
            list.Sort(CompareDinosByChineseLength);
            if (list.Count < 1)
            {
                return "";
            }
            input = list[list.Count - 1];
            

            //4、把p和br替换成特殊的占位符[p][br]
            input = new Regex(reg1, RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, "[$1]");

            //5、去掉HTML标签，保留汉字
            input = new Regex(reg2, RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, "");

            //6、把特殊占维护替换成回车和换行
            input = new Regex("\\[p]", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, "\r\n ");
            input = new Regex("\\[br]", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, "\r\n");
            return input;
        }
    }
}
