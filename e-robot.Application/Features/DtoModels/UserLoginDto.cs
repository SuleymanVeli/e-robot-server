using e_robot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace e_robot.Application.Features.DtoModels
{
    public class UserLoginDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Level { get; set; }
        public int Rank { get; set; }

    }
}
