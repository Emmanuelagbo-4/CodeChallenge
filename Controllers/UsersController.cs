using System.Threading.Tasks;
using AutoMapper;
using CodeChallenge.Data;
using CodeChallenge.Entities;
using CodeChallenge.Helper;
using CodeChallenge.Models.Request;
using CodeChallenge.Models.Response;
using CodeChallenge.Services;
using CodeChallenge.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ApplicationDbContext _dbContext;
        UserManager<ApplicationUser> _userManager;
        UserService _userService;
        RoleManager<IdentityRole> _roleManager;
        IMapper _mapper;
        public UsersController
        (
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            UserService userService,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper
        )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _userService = userService;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        /// <summary>
        /// User authentication with email and password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("auth")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<AuthenticateUserResponseModel>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequestModel model)
        {
            var resp = await _userService.GenerateToken(model);
            if (resp.status)
            {
                // _logger.LogInformation("User Successfully authenticated");
                return Ok(new ApiResponse
                {
                    message = "Authentication Successful",
                    data = resp.data
                });
            }
            // _logger.LogWarning("User authentication failed");
            return BadRequest(new ApiResponse
            {
                message = resp.response
            });
        }
        /// <summary>
        /// Register Customer
        /// </summary>
        [HttpPost("customer")]
        [ProducesResponseType(typeof(ApiResponse<ApplicationUser>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCustomer([FromBody] UserRegistrationRequestModel UserDTO)
        {
            try
            {
                if ((await _userManager.FindByEmailAsync(UserDTO.Email)) != null)
                {
                    return BadRequest(new ApiResponse { message = "User email in existence" });
                }

                var AppUser = _mapper.Map<ApplicationUser>(UserDTO);
               var response = await _userService.Register(AppUser, UserDTO.Password, Roles.Customer);
                if (response.status)
                {
                    var user = response.data as ApplicationUser;
                    return Ok(new ApiResponse
                    {
                        message = "Customer Registration Successful.",
                        data = _userService.GetUserDetail(user.Id)
                    });
                }
                return BadRequest(new ApiResponse
                {
                    message = response.response as string,
                });
            }
            catch (AppException ex)
            {
                return BadRequest(new ApiResponse { message = ex.Message });
            }

        }
          /// <summary>
        /// Register Admin
        /// </summary>
        [HttpPost("admin")]
        [ProducesResponseType(typeof(ApiResponse<ApplicationUser>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateAdmin([FromBody] UserRegistrationRequestModel UserDTO)
        {
            try
            {
                if ((await _userManager.FindByEmailAsync(UserDTO.Email)) != null)
                {
                    return BadRequest(new ApiResponse { message = "User email in existence" });
                }

                var AppUser = _mapper.Map<ApplicationUser>(UserDTO);
                var response = await _userService.Register(AppUser, UserDTO.Password, Roles.Admin);
                if (response.status)
                {
                    var user = response.data as ApplicationUser;
                    return Ok(new ApiResponse
                    {
                        message = "Admin Created Successful.",
                        data = _userService.GetUserDetail(user.Id)
                    });
                }
                return BadRequest(new ApiResponse
                {
                    message = response.response as string,
                });
            }
            catch (AppException ex)
            {
                return BadRequest(new ApiResponse { message = ex.Message });
            }

        }

        [HttpPost("like")]
        public IActionResult Like()
        {
            return Ok("Tested Endpoint for Like");
        }
    }
}