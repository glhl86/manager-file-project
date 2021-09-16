using CrossCutting.ApiModel;
using CrossCutting.Enumerators;
using Domain.Business.BO;
using Domain.Business.Interface;
using Domain.Models;
using FileManger.Utils;
using FileManger.Utils.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        private readonly ILogger<FileManagerController> logger;
        private readonly IStructure structureBO;
        private readonly IPermissions permissionsBO;
        private readonly ISendEmails sendEmails;

        public IConfiguration Configuration { get; }

        public FileManagerController(FileManagerContext context, ILogger<FileManagerController> log, IConfiguration config)
        {
            logger = log;
            Configuration = config;
            structureBO = new StructureBO(context);
            permissionsBO = new PermissionsBO(context);
            sendEmails = new SendEmails(Configuration);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create([FromBody] StructureAM data)
        {
            try
            {
                data.StructureId = CreateData(data);

                PermissionsAM permissions = new PermissionsAM { StructureId = data.StructureId, UserId = "d42a4c42-8a97-4d5a-a02e-11d0dbcf7050" };
                permissionsBO.Create(permissions);

                if (data.ListStrure != null && data.ListStrure.Count > 0)
                    foreach (StructureAM st in data.ListStrure)
                    {
                        st.FatherStructureId = data.StructureId;
                        st.StructureId = CreateData(st);

                        permissions = new PermissionsAM { StructureId = st.StructureId, UserId = "d42a4c42-8a97-4d5a-a02e-11d0dbcf7050" };
                        permissionsBO.Create(permissions);

                        if (st.ListStrure != null && st.ListStrure.Count > 0)
                            foreach (StructureAM j in st.ListStrure)
                            {
                                j.FatherStructureId = st.StructureId;
                                j.StructureId = CreateData(j);

                                permissions = new PermissionsAM { StructureId = j.StructureId, UserId = "d42a4c42-8a97-4d5a-a02e-11d0dbcf7050" };
                                permissionsBO.Create(permissions);
                            }
                    }

                // EmailAM email = new EmailAM { Email = "dexterdexter86@gmail.com", Message = "Carpetas configuradas" };
                // bool emailSent = sendEmails.SendEmailConfig(email);

                return StatusCode(StatusCodes.Status201Created, new JsonResponse { Status = StatusCodes.Status201Created, Result = data, Title = ApiMessage.SUCCESFULLY, TraceId = Guid.NewGuid().ToString() });
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

        private long CreateData(StructureAM data)
        {
            return structureBO.Create(data);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetFiles([FromBody] UserAM data)
        {
            try
            {
                List<PermissionsAM> permissions = permissionsBO.Get(j => j.UserId == data.Id);

                return StatusCode(StatusCodes.Status201Created, new JsonResponse { Status = StatusCodes.Status201Created, Result = permissions, Title = ApiMessage.SUCCESFULLY, TraceId = Guid.NewGuid().ToString() });
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
