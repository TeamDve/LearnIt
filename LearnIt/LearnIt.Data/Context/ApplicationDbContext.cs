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

        public virtual IDbSet<Position> DeptRoles { get; set; }

        public virtual IDbSet<Department> Departments { get; set; }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Question> Questions { get; set; }

        public virtual IDbSet<UserCourse> UsersCourses { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
