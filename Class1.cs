using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Mliybs
{
    namespace MliybsTieba
    {
        /// <summary>
        /// 百度贴吧类
        /// </summary>
        public static class Tieba
        {
            /// <summary>
            /// 搜索某一个吧内某一页的所有帖子
            /// </summary>
            /// <param name="client"></param>
            /// <param name="keyword"></param>
            /// <param name="page"></param>
            /// <returns>返回该页的所有帖子链接组成的字符串数组</returns>
            public static async Task<string[]> TiebaGet(this HttpClient client, string keyword, uint page = 1)
            {
                if (page == 0 || page == 1)
                    page = 1;

                else
                    page = (page - 1) * 50;
                    
                try
                {
                    var res = await client.GetAsync($"https://tieba.baidu.com/f?kw={keyword}&ie=utf-8&pn={page}");

                    res.EnsureSuccessStatusCode();

                    var content = await res.Content.ReadAsStringAsync();

                    var collection = Regex.Matches(content, "/p/[0-9]+");

                    return collection.Select(item => "https://tieba.baidu.com" + item.Value).ToArray();
                }
                catch (Exception)
                {
                    throw new("获取失败！");
                }
            }

            /// <summary>
            /// 获取贴吧某一帖子里所发送的所有图片链接
            /// </summary>
            /// <param name="client"></param>
            /// <param name="url"></param>
            /// <returns>所有用户发送的图片的链接组成的字符串数组</returns>
            public static async Task<string[]> TiebaImage(this HttpClient client, string url)
            {
                try
                {
                    var res = await client.GetAsync(url);

                    res.EnsureSuccessStatusCode();

                    var content = await res.Content.ReadAsStringAsync();

                    var collection = Regex.Matches(content, @"<img class=""BDE_Image"" src="".{177}"" size=");

                    return collection.Select(item => item.Value.Replace(@"<img class=""BDE_Image"" src=""", string.Empty).Replace(@""" size=", string.Empty)).ToArray();
                }
                catch (Exception)
                {
                    throw new("获取失败！");
                }
            }
        }
    }
}