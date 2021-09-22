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
    public class UserRegisterCommmand : IRequest<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
    public class UserRegisterCommmandHander : IRequestHandler<UserRegisterCommmand, string>
    {
        private IAsyncRepository repository;

        public UserRegisterCommmandHander(IAsyncRepository repository, IConfiguration configuration)
        {
            this.repository = repository;        
        }

        public async Task<string> Handle(UserRegisterCommmand request, CancellationToken cancellationToken)
        {
            User user = await repository.GetAsync<User>(u => u.Email == request.Email);
            string code = RandomHandler.RandomNumeric(6);
            if (user == null)
            {
                await repository.InsertAsync<User, UserRegisterCommmand>(request, new { Guid = Guid.NewGuid().ToString(), PasswordHash = Md5Handler.MD5Hash(request.Password), OtpCode = code, Status = UserStatus.WAIT_OTP });
            }
            else
            {
                if (user.Status != UserStatus.WAIT_OTP) 
                    throw new HttpStatusException(HttpStatusCode.Conflict, "User aready exist");

                user.OtpCode = code;
                user.PasswordHash = Md5Handler.MD5Hash(request.Password);
                user.Name = request.Name;
                user.Surname = request.Surname;

                await repository.UpdateSimpleAsync<User>(user);
            }


            var title = "Salam";
            var text = "Sizin təsdiq kodunuz: "+ code;
            


            string html = StaticData.EmailOtpCodeHtml
                .Replace("{1}", title)
                .Replace("{2}", text);              

            EmailHandler.SendEmail(request.Email, "Tesdiq kodu", html);

            return code;
        }
    }
}
