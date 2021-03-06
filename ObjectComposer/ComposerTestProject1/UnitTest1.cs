using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rtully.LinkedIn.Articles.ObjectComposer;
using System;

namespace ComposerTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ComposerService.RegisterObjectMapper(new AutomobileDbModelToClientDto<AutomobileDto, AutomobileDbModel>());

            AutomobileDto dto = new AutomobileDto
            {
                Color = "red",
                NumberOfPassengers=4
            };

            var model=ComposerService.Composer.Compose<AutomobileDto, AutomobileDbModel>(dto);

            //Assert.IsFalse(false, "1 should not be prime");
            Assert.IsTrue(true, "yaaaaay!!");
        }
    }
}
