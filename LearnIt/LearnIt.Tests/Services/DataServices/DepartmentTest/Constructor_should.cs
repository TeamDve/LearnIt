using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearnIt.Tests.Services.DataServices.DepartmentTest
{
    [TestClass]
    public class Constructor_should
    {
        [TestMethod]
        public void ThrowException_WhenNullValueIsGiven()
        {
            //Arrange && Act && Assert
            DepartmentService anon;
            Assert.ThrowsException<ArgumentNullException>(() => anon = new DepartmentService(null));
        }
    }
}
