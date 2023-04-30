# MliybsTieba
Mliybs的C#百度贴吧库

## HttpClient
由于最好只实例化一次HttpClient 所以本库的所有方法都是HttpClient的扩展方法（而且还是异步的 记得加await）

示例代码：
```CSharp

using System;
using System.Net.Http;
using Mliybs.MliybsTieba;

namespace Namespace
{
    class Program
    {
        public static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            Console.WriteLine((await client.TiebaGet("吊图"))[0]);
        }
    }
}
```

输出结果为吊图吧的某一个帖子的链接（这里不能放啊）

### 已有方法
#### TiebaGet
获取某个吧的该页（共50个帖子）的所有帖子组成的字符串数组

#### TiebaImage
获取某一具体帖子内的所有用户发送的图片的链接所组成的字符串数组