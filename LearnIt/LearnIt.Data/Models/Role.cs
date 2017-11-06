using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnIt.Data.Models
{
    public class Role
    {
        private ICollection<ApplicationUser> applicationUsers;

        public Role()
        {
            this.applicationUsers = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers
        {
            get
            {
                return this.applicationUsers;
            }
            set
            {
                this.applicationUsers = value;
            }
        }

    }
}
