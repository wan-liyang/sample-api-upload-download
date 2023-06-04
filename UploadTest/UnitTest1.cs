using System.Globalization;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace UploadTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        byte[] bytes = File.ReadAllBytes("test file.txt");
        using (var client = new HttpClient())
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(new MemoryStream(bytes)), "files", "test file.txt");

                using (var message = client.PostAsync("url", content))
                {
                    var result = message.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
            }
        }


        Assert.Pass();
    }

    [Test]
    public void Test2()
    {
        using (var client = new HttpClient())
        {
            using (var message = client.GetAsync("url"))
            {
                var result = message.Result.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            }
        }


        Assert.Pass();
    }
}
