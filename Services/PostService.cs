using System.Linq;
using AutoMapper;
using CodeChallenge.Data;
using CodeChallenge.Entities;
using CodeChallenge.Models.Response;
using Microsoft.AspNetCore.Identity;

namespace CodeChallenge.Services
{
    public class PostService
    {
        ApplicationDbContext _dbContext;
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        IMapper _mapper;
        public PostService
        (
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper
        )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public ServiceResponse CreatePost(Post model, ApplicationUser user)
        {
            model.ApplicationUserId = user.Id;
            model.ApplicationUser = user;
            var result = _dbContext.Posts.Add(model);
            int count = _dbContext.SaveChanges();

            if (count > 0)
            {
                return new ServiceResponse { status = true, data = result.Entity };
            }
            return new ServiceResponse {status = false, response = "Post Created Succesfully"};
        }

        public ServiceResponse GetUserDetail(string Id)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(x => x.Id == Id);
            user.PasswordHash = null;
            user.SecurityStamp = null;
            user.ConcurrencyStamp = null;
            return new ServiceResponse { status = true, data = user };
        }

        public ServiceResponse LikePost(Like model)
        {
            var Post = _dbContext.Posts.Where(x => x.Id == model.PostId).FirstOrDefault();
            Post.LikeCount ++;
            Post.ApplicationUser.PasswordHash = null;
            Post.ApplicationUser.SecurityStamp = null;
            Post.ApplicationUser.ConcurrencyStamp = null;
            var result = _dbContext.Likes.Add(model);
            int count = _dbContext.SaveChanges();

            if (count > 0)
            {
                return new ServiceResponse { status = true, data = result.Entity };
            }
            return new ServiceResponse {status = false, response = "Liked Post Succesfully"}; 

           
        }
    }
}