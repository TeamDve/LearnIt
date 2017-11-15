using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearnIt.Tests.Services.DataServices.PositionServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowException_WhenNullValueIsGiven()
        {
            //Arrange && Act && Assert
            PositionService anon;
            Assert.ThrowsException<ArgumentNullException>(() => anon = new PositionService(null));


        }
    }
}
