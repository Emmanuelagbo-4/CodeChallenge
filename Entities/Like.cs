using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Entities
{
    public class Like
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public int IsLike {get; set;}
        public int PostId {get; set;}
        [ForeignKey("PostId")]
        public Post Post {get; set;}
    }
}