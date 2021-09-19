using CrossCutting.ApiModel;
using CrossCutting.Enumerators;
using Domain.Business.BO;
using Domain.Business.Interface;
using Domain.Models;
using FileManger.Utils;
using FileManger.Utils.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace FileManger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public IActionResult Create([FromForm] StructureAM data)
        {
            try
            {
                string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                string email = this.User.FindFirst(ClaimTypes.Email).Value;
                string rootFolder = Configuration.GetSection("Folder").Value;

                data.DateRecord = DateTime.Now;

                if (data.IsFile)
                {
                    
                    // Envio email
                    EmailAM mail = new EmailAM { Email = email, Message = ApiMessage.EMAIL_MESSAGE };
                    bool emailSent = sendEmails.SendEmailConfig(mail); // enviar solo si es archivo
                }
                else
                {
                    if (data.FatherStructureId == null)
                    {
                        data.StructureId = CreateData(data);
                        string path = Path.Combine(rootFolder, data.StructureName);

                        CreateDirectory(path);
                    }
                    else
                    {
                        string buildPath = BuildPath(data, data.StructureName);
                        string path = Path.Combine(rootFolder, buildPath);

                        data.PathFile = buildPath;
                        data.StructureId = CreateData(data);

                        CreateDirectory(path);
                    }
                }

                PermissionsAM permissions = new PermissionsAM { StructureId = data.StructureId, UserId = userId };
                permissionsBO.Create(permissions);

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

        private string BuildPath(StructureAM data, string path)
        {
            if (data.FatherStructureId != null && data.FatherStructureId > 0)
            {
                StructureAM father = structureBO.Get(data.FatherStructureId.Value);
                if (father != null && father.StructureId > 0)
                {
                    path = Path.Combine(father.StructureName, path);
                    path = BuildPath(father, path);
                }
                return path;
            }
            else
                return path;
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        #endregion

    }
}
