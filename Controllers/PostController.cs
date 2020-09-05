using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CodeChallenge.Data;
using CodeChallenge.Entities;
using CodeChallenge.Models.Request;
using CodeChallenge.Models.Response;
using CodeChallenge.Services;
using CodeChallenge.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [Route("api/blog-post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        ApplicationDbContext _dbContext;
        UserManager<ApplicationUser> _userManager;
        UserService _userService;
        RoleManager<IdentityRole> _roleManager;
        IMapper _mapper;
        PostService _postService;
        public PostController
        (
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            UserService userService,
            RoleManager<IdentityRole> roleManager,
            PostService postService,
            IMapper mapper
        )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _userService = userService;
            _roleManager = roleManager;
            _postService = postService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create Blog Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create-post")]
        [Authorize(Roles = Roles.Customer)]
        [ProducesResponseType(typeof(ApiResponse<CreateBlogPostRequestModel>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Create([FromBody] CreateBlogPostRequestModel model)
        {
            ApplicationUser user = _userManager.FindByIdAsync (User.FindFirst (ClaimTypes.NameIdentifier)?.Value).Result;
            var BlogPostModel = _mapper.Map<Post>(model);
            var response = _postService.CreatePost(BlogPostModel, user);
            if(response.status){
                    return Ok(new ApiResponse {message = "BlogPost created successfully", data = _userService.GetUserDetail(user.Id)});
                }
            return BadRequest (new ApiResponse {message = "Post creation failed"});
        }
        
        [HttpPost("like")]
        public IActionResult Like()
        {
            
            return Ok("Tested Endpoint for Like");
        }
    }
}