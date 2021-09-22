using e_robot.Application.Features.Commands;
using e_robot.Application.Features.DtoModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace e_robot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SolutionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor accessor;

        public SolutionsController(IMediator mediator, IHttpContextAccessor accessor)
        {
            _mediator = mediator;
            this.accessor = accessor;
        }

        [HttpGet("{examId}", Name = "GetSolution")]
        [ProducesResponseType(typeof(SolutionDto), (int)HttpStatusCode.OK)]        
        public async Task<ActionResult<SolutionDto>> GetSolution(int examId, int? id)
        {
            string guid = accessor.HttpContext.User.Identity.Name;
            var query = new SolutionCommmand { ExamId = examId , Id = id, UserGuid = guid };
            var solution = await _mediator.Send(query);
            return Ok(solution);
        }

        [HttpPost("UpdateBlock",Name = "Update Solution Block")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> GetExamDetailQuery(SolutionUpdateBlockCommmand command)
        {           
            await _mediator.Send(command);
            return Created("", 1);
        }
    }
}
