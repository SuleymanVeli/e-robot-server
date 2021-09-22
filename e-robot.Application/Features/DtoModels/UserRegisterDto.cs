using e_robot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace e_robot.Application.Features.DtoModels
{
    public class UserRegisterDto
    {       
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
