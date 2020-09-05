using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Entities
{
    public class Post
    {
        public Post()
        {
            DateCreated = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Content {get; set;}
        public int LikeCount {get; set;}
        public DateTime DateCreated {get; set;}
        public string ApplicationUserId {get; set;}
        [ForeignKey ("ApplicationUserId")]
        public ApplicationUser ApplicationUser {get; set;}
    }
}