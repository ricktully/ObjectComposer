using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rtully.LinkedIn.Articles.ObjectComposer;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        { 
            ComposerService.RegisterObjectMapper(new AutomobileDbModelToClientDto<AutomobileDto, AutomobileDbModel>());

            Assert.IsFalse(true, "1 should not be prime");
        }
    }
}
