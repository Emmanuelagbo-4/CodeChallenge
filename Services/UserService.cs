using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using CodeChallenge.Data;
using CodeChallenge.Entities;
using CodeChallenge.Models.Response;
using Microsoft.AspNetCore.Identity;

namespace CodeChallenge.Services
{
    public class UserService
    {
        ApplicationDbContext _dbContext;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        IConfiguration Configuration { get; }
        IMapper _mapper;

        public UserService
        (
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMapper mapper
        )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            Configuration = configuration;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> Register(ApplicationUser User, string Password, string Role)
        {
            User.UserName = User.Email;

            var result = _userManager.CreateAsync(User, Password).Result;

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Role));
                }
                await _userManager.AddToRoleAsync(User, Role);
                return new ServiceResponse { status = true, data = User };
            }
            else
            {
                return new ServiceResponse { status = false, response = "User account creation failed" };
            }
        }

        public ServiceResponse GetUserDetail(string Id)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(x => x.Id == Id);
            user.PasswordHash = null;
            user.SecurityStamp = null;
            user.ConcurrencyStamp = null;
            return new ServiceResponse { status = true, data = user };
        }
    }
}