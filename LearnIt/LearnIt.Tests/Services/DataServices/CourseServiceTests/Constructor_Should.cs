using LearnIt.Data.Context;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace LearnIt.Tests.Services.DataServices.CourseServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowException_WhenNullValueIsGiven()
        {
            //Arrange && Act && Assert
            CourseService anon;
            Assert.ThrowsException<ArgumentNullException>(()=> anon = new CourseService(null));
        }

    }
}
