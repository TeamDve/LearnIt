using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearnIt.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<UserCourse> usersCourses;

        public ApplicationUser()
            :base()
        {
            this.usersCourses = new HashSet<UserCourse>();
        }

        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public virtual ICollection<UserCourse> UsersCourses
        {
            get
            {
                return this.usersCourses;
            }
            set
            {
                this.usersCourses = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
