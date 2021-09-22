using e_robot.Application.Features.DtoModels;
using e_robot.Application.Features.Queries;
using e_robot.Domain.Entities;
using MediatR;
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
    public class ExamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetExams")]
        [ProducesResponseType(typeof(IEnumerable<ExamListDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ExamListDto>>> GetExams()
        {
            var query = new ExamListQuery();
            var exams = await _mediator.Send(query);
            return Ok(exams);
        }

        [HttpGet("{id}", Name = "GetExamDetail")]
        [ProducesResponseType(typeof(ExamDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ExamDto>> GetExamDetailQuery(int id,string guid)
        {            
            var query = new ExamQuery { Guid = guid, Id = id };
            var exam = await _mediator.Send(query);
            return Ok(exam);
        }

    }
}
