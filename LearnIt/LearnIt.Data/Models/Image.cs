using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnIt.Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        
        //Base64 implementation
        //[Column(TypeName = "varchar(MAX)")]
        //public string ImageBase { get; set; }

        //Image as Binary (varbinary(MAX))
        public byte[] ImageBinary { get; set; }

        [Required]
        public int Position { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

         
    }
}
