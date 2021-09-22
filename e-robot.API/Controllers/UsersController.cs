using e_robot.Application.Features.Commands;
using e_robot.Application.Features.DtoModels;
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
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login", Name = "Login")]
        [ProducesResponseType(typeof(UserLoginDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserLoginDto>> GetExamDetailQuery(UserLoginCommmand command)
        {
            var model = await _mediator.Send(command);
            return Ok(model);
        }

        [HttpPost("Register", Name = "Register")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> GetExamDetailQuery(UserRegisterCommmand command)
        {
            await _mediator.Send(command);
            return Ok("");
        }

        [HttpPost("Otp",Name = "Otp")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetExamDetailQuery(UserOtpCommmand command)
        {
            var model = await _mediator.Send(command);
            return Ok(model);
        }

    }
}
