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
    public class AddDepToUserDepartment_Should
    {
        [TestMethod]
        public void AddDepartmentToUser_WhenParametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            var department = new Department() { Name = "Stancho" };
            List<Department> listofDepartments = new List<Department>() { department };
            var departmentMock = new Mock<DbSet<Department>>().SetupData(listofDepartments);
            dbContextMock.SetupGet(x => x.Departments).Returns(departmentMock.Object);
            var user = new ApplicationUser()
            {
                UserName = "FakeUser",
                Id = "asd",
                Department = null
            };
            List<ApplicationUser> userList = new List<ApplicationUser>(){ user };
            var usersDbSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(userList);
            dbContextMock.SetupGet<IDbSet<ApplicationUser>>(x => x.Users).Returns(usersDbSetMock.Object);

            //Act
            DepartmentService departmentservice = new DepartmentService(dbContextMock.Object);

            departmentservice.AddDepToUserDepartment(user.UserName, department.Name);
       
            //Assert
            Assert.AreEqual(department.Name,user.Department.Name);





        }
    }
}
