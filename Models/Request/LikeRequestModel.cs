using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models.Request
{
    public class LikeRequestModel
    {
        [Required]
        public int PostId {get; set;}
    }
}