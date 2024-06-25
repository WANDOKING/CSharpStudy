namespace TestConfig
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.FileProviders;

    [TestClass]
    public class TestJsonConfig
    {
        [TestMethod]
        public void ReadJson()
        {
            var builder = new ConfigurationBuilder();

            // 실행 파일 위치에서 찾는다.
            builder.SetBasePath(AppContext.BaseDirectory).AddJsonFile("ReadJson.json");

            IConfiguration configuration = builder.Build();

            Assert.AreEqual("Hello", configuration.GetValue<string>("StringTest"));
            Assert.AreEqual(1, configuration.GetValue<int>("Test1"));
            Assert.AreEqual(21, configuration.GetValue<int>("Test2:Test2_1"));
            Assert.AreEqual(22, configuration.GetValue<int>("Test2:Test2_2"));
            Assert.AreEqual(231, configuration.GetValue<int>("Test2:Test2_3:Test2_3_1"));
            Assert.AreEqual(232, configuration.GetValue<int>("Test2:Test2_3:Test2_3_2"));
        }

        [TestMethod]
        public void ReadJsonNotFound()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppContext.BaseDirectory);
            builder.AddJsonFile("NotExist.json");
            Assert.ThrowsException<FileNotFoundException>(() => builder.Build());
        }
    }
}