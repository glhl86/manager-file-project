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

                if (data.IsFile)
                {

                }



                // EmailAM email = new EmailAM { Email = "dexterdexter86@gmail.com", Message = "Carpetas configuradas" };
                // bool emailSent = sendEmails.SendEmailConfig(email); solo enviar si es archivo

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

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetFiles()
        {
            try
            {
                List<PermissionsAM> permissions = permissionsBO.Get();

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

        #region PRIVATE METHODS
        /// <summary>
        /// Guardar la estructura de las carpetas y archivos
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private long CreateData(StructureAM data)
        {
            return structureBO.Create(data);
        }

        #endregion

    }
}
