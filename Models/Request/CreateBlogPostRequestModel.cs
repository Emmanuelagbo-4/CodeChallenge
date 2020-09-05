using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models.Request
{
    public class CreateBlogPostRequestModel
    {
        [Required]
        public string Content {get; set;}
    }
}