using LearnIt.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LearnIt.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Role> DeptRoles { get; set; }

        public IDbSet<Department> Departments { get; set; }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Image> Images { get; set; }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<UserCourse> UsersCourses { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
