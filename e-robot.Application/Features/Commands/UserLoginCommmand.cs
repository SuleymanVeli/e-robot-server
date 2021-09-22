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

namespace e_robot.Application.Features.Commands
{
    public class UserLoginCommmand : IRequest<UserLoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginCommmandHander : IRequestHandler<UserLoginCommmand, UserLoginDto>
    {
        private IAsyncRepository repository;
        private IConfiguration configuration;

        public LoginCommmandHander(IAsyncRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<UserLoginDto> Handle(UserLoginCommmand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetAsync<User>(u=>u.Email == request.Email && u.Deleted == false);

            var response = new UserLoginDto();

            if (user == null || user.Status == UserStatus.WAIT_OTP)
                throw new EntryPointNotFoundException();

            string password = Md5Handler.MD5Hash(request.Password);

            if (password != user.PasswordHash)
            {
                throw new HttpStatusException(HttpStatusCode.Forbidden,"Wrong Password");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Guid)                
            };

            //var userClaims = user.Claims?.Select(s => new Claim(s.ClaimType, s.ClaimValue)).ToList();

            //  if (userClaims != null ) claims.AddRange(userClaims);
           
            response.Email = user.Email;
            response.Name = user.Name;
            response.Surname = user.Surname;
            response.AccessToken = JwtHandler.GetJwt(claims, configuration);

            return response;
        }
    }
}
