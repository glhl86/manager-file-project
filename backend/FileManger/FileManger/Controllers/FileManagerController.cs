using CrossCutting.ApiModel;
using CrossCutting.Enumerators;
using Domain.Business.BO;
using Domain.Business.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public FileManagerController(FileManagerContext context, ILogger<FileManagerController> log)
        {
            structureBO = new StructureBO(context);
            logger = log;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create([FromBody] StructureAM data)
        {
            try
            {
                data.StructureId = CreateData(data);

                if (data.ListStrure != null && data.ListStrure.Count > 0)
                    foreach (StructureAM st in data.ListStrure)
                    {
                        st.FatherStructureId = data.StructureId;
                        st.StructureId = CreateData(st);

                        if (st.ListStrure != null && st.ListStrure.Count > 0)
                            foreach (StructureAM j in st.ListStrure)
                            {
                                j.FatherStructureId = st.StructureId;
                                j.StructureId = CreateData(j);
                            }
                    }

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
        public IActionResult GetFiles()
        {
            try
            {
                StructureAM structreOne = new StructureAM { IsFile = false, DateRecord = DateTime.Now, PathFile = "/Temp/Folder", StructureName = "root-structreOne" };
                structreOne.ListStrure = new List<StructureAM>();
                StructureAM structreChildOne = new StructureAM { IsFile = false, DateRecord = DateTime.Now, PathFile = "/Temp/files", StructureName = "ChildOne" };

                //StructureAM structreTwo = new StructureAM();
                structreChildOne.ListStrure = new List<StructureAM>();
                StructureAM structreChildTwo = new StructureAM { IsFile = false, DateRecord = DateTime.Now, PathFile = "/Temp/files", StructureName = "structreChildTwo" };
                structreChildOne.ListStrure.Add(structreChildTwo);
                structreOne.ListStrure.Add(structreChildOne);

                return StatusCode(StatusCodes.Status201Created, new JsonResponse { Status = StatusCodes.Status201Created, Result = structreOne, Title = ApiMessage.SUCCESFULLY, TraceId = Guid.NewGuid().ToString() });
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
