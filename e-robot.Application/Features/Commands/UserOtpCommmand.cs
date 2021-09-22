using e_robot.Application.Features.DtoModels;
using e_robot.Application.Handlers;
using e_robot.Application.Contracts.Persistence;
using e_robot.Domain.Entities;
using e_robot.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using e_robot.Application.Exceptions;
using System.Net;
using e_robot.Application.Models;

namespace e_robot.Application.Features.Commands
{
    public class UserOtpCommmand : IRequest<int>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
    public class UserOtpCommmandHander : IRequestHandler<UserOtpCommmand, int>
    {
        private IAsyncRepository repository;

        public UserOtpCommmandHander(IAsyncRepository repository, IConfiguration configuration)
        {
            this.repository = repository;        
        }

        public async Task<int> Handle(UserOtpCommmand request, CancellationToken cancellationToken)
        {
            User user = await repository.GetAsync<User>(u => u.Email == request.Email && u.Status == UserStatus.WAIT_OTP && u.OtpCode == request.Code);

            if (user == null) throw new EntryPointNotFoundException();

            user.Status = UserStatus.SUCCESSS;

            user.OtpCode = null;
            await repository.UpdateSimpleAsync<User>(user);

            return 1;
        }
    }
}
