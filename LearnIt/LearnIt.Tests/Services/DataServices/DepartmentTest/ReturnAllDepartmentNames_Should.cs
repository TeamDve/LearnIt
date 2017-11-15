using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnIt.Data.Context;
using LearnIt.Data.Models;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LearnIt.Tests.Services.DataServices.DepartmentTest
{
    [TestClass]
    public class ReturnAllDepartmentNames_Should
    {
        [TestMethod]
        public void ReturnListOfDepartmentNames_WhenParametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var department = new Department() { Name = "Stancho" };
            var department1 = new Department() { Name = "Pavcho" };
            var department2 = new Department() { Name = "Murcho" };

            List<Department> listofDepartments = new List<Department>() { department, department1, department2 };

            var departmentMock = new Mock<DbSet<Department>>().SetupData(listofDepartments);
            dbContextMock.SetupGet(x => x.Departments).Returns(departmentMock.Object);
            //Act
            DepartmentService departmentservice = new DepartmentService(dbContextMock.Object);

            var test = departmentservice.ReturnAllDepartmentNames();
            //Assert
            Assert.AreEqual(3, test.Count());

        }
    }
}
