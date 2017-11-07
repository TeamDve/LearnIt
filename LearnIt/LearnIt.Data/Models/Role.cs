using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnIt.Data.Models
{
    public class Position
    {
        private ICollection<ApplicationUser> applicationUsers;

        public Position()
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
