using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnIt;
using LearnIt.Controllers;
using Moq;
using LearnIt.Data.Services.Contracts;

namespace LearnIt.Tests.Controllers
{
    [TestClass]
    public class Index_Should_Dummy
    {
        [TestMethod]
        public void ShouldBeSuccessfull()
        {
            bool success = false;
            Assert.IsFalse(success);
        }
    }
}
