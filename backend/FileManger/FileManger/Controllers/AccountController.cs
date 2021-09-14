using CrossCutting.ApiModel;
using CrossCutting.Enumerators;
using Domain.Business.BO;
using Domain.Business.Interface;
using Domain.Models;
using FileManger.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetUsers()
        {

            try
            {
                List<Users> userData = userManager.Users.Include(j => j.UsersRoles).ThenInclude(j => j.Role).ToList();

                return StatusCode(StatusCodes.Status201Created, new JsonResponse { Result = userData, Status = StatusCodes.Status201Created, Title = ApiMessage.OK, TraceId = Guid.NewGuid().ToString() });
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

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetRolesAsync()
        {
            try
            {
                var data = roleManager.Roles.ToList();

                return StatusCode(StatusCodes.Status201Created, new JsonResponse { Status = StatusCodes.Status201Created, Result= data, Title = ApiMessage.SUCCESFULLY, TraceId = Guid.NewGuid().ToString() });
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
    }
}
