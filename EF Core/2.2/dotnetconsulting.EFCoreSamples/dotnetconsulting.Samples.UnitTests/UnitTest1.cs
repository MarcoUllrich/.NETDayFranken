using dotnetconsulting.Samples.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotnetconsulting.Samples.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var options = new DbContextOptionsBuilder<SamplesContext1>()
                .UseInMemoryDatabase(databaseName: "UnitTest")
                .Options;

            using (SamplesContext1 context = new SamplesContext1(options))
            {

            }
        }
    }
}