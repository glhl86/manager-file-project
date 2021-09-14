using CrossCutting.ApiModel;
using CrossCutting.Enumerators;
using Domain.Business.BO;
using Domain.Business.Interface;
using Domain.Models;
using FileManger.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FileManger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;
        private readonly SignInManager<Users> singInManager;
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<Users> userManager;
        private readonly IPerson personBO;

        public AccountController(FileManagerContext context, ILogger<AccountController> log, UserManager<Users> userManag, SignInManager<Users> signInManag, RoleManager<Role> roleManag)
        {
            personBO = new PersonBO(context);
            singInManager = signInManag;
            roleManager = roleManag;
            userManager = userManag;
            logger = log;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateUsersAsync([FromBody] LoginAM data)
        {
            try
            {
                data.Person.IdState = (long?)ApiEnums.Estates.ACTIVE;
                data.Person.RegistrationDate = DateTime.Now;

                var personId = personBO.Create(data.Person);

                if (personId > 0)
                {
                    Users user = new Users
                    {
                        Email = data.Email,
                        UserName = data.Email,
                        IdPerson = personId,
                        IdState = 1,
                    };

                    var resultCreated = await userManager.CreateAsync(user);
                    var passwordResult = await userManager.AddPasswordAsync(user, data.Password);
                    var asignRol = await userManager.AddToRoleAsync(user, data.RolName);
                }

                return StatusCode(StatusCodes.Status201Created, new JsonResponse { Status = StatusCodes.Status201Created, Title = ApiMessage.SUCCESFULLY, TraceId = Guid.NewGuid().ToString() });
            }
            catch (Exception e)
            {
                logger.LogInformation("Error: {mess}", e);
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = ApiMessage.INTERNAL_ERROR,
                    Errors = new string[] { e.Message },
                    TraceId = Guid.NewGuid().ToString()
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAM data)
        {
            try
            {
                Users user = await userManager.FindByEmailAsync(data.Email);
                var result = await singInManager.PasswordSignInAsync(data.Email, data.Password, true, false);

                if (user != null && user.IdState == 1 && result.Succeeded)
                {
                    HttpContext.Session.SetString("IdUsers", user.Id);
                    return StatusCode(StatusCodes.Status200OK, new JsonResponse { Status = StatusCodes.Status200OK, Result = user, Title = ApiMessage.OK, TraceId = Guid.NewGuid().ToString() });
                }
                else
                    return StatusCode(StatusCodes.Status401Unauthorized, new JsonResponse { Status = StatusCodes.Status401Unauthorized, Title = ApiMessage.INVALID_LOGIN, TraceId = Guid.NewGuid().ToString() });
            }
            catch (Exception e)
            {
                logger.LogError("Error: {msg}", e);
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = ApiMessage.INTERNAL_ERROR,
                    Errors = new string[] { e.Message },
                    TraceId = Guid.NewGuid().ToString()
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await singInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();

            foreach (var cookie in HttpContext.Request.Cookies.Keys)
                Response.Cookies.Delete(cookie);


            return StatusCode(StatusCodes.Status200OK, new JsonResponse { Status = StatusCodes.Status200OK, Title = ApiMessage.LOGOUT, TraceId = Guid.NewGuid().ToString() });
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetRolesAsync()
        {
            try
            {
                var data = roleManager.Roles.ToList();

                return StatusCode(StatusCodes.Status200OK, new JsonResponse { Status = StatusCodes.Status200OK, Result = data, Title = ApiMessage.SUCCESFULLY, TraceId = Guid.NewGuid().ToString() });
            }
            catch (Exception e)
            {
                logger.LogInformation("Error: {mess}", e);
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = ApiMessage.INTERNAL_ERROR,
                    Errors = new string[] { e.Message },
                    TraceId = Guid.NewGuid().ToString()
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(LoginAM data)
        {
            var user = await userManager.FindByEmailAsync(data.Email);

            if (user != null)
            {
                string token = await userManager.GeneratePasswordResetTokenAsync(user);

                var result = await userManager.ResetPasswordAsync(user, token, data.Password);
                var updateResult = await userManager.UpdateAsync(user);

                if (result.Succeeded && updateResult.Succeeded)
                    return StatusCode(StatusCodes.Status200OK, new JsonResponse { Status = StatusCodes.Status200OK, Title = ApiMessage.FORGOTPASSWORD, TraceId = Guid.NewGuid().ToString() });
                else
                    return StatusCode(StatusCodes.Status304NotModified, new JsonResponse { Status = StatusCodes.Status304NotModified, Title = ApiMessage.INVALID_FORGOTPASSWORD, TraceId = Guid.NewGuid().ToString() });
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified, new JsonResponse { Status = StatusCodes.Status304NotModified, Title = ApiMessage.INVALID_FORGOTPASSWORD, TraceId = Guid.NewGuid().ToString() });
            }
        }
    }
}
