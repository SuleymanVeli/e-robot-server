using e_robot.Domain.Common;
using e_robot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace e_robot.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string OtpCode { get; set; }
        public UserStatus Status { get; set; }
        public int Level { get; set; }
        public int Rank { get; set; }
        public bool Deleted { get; set; } = false;

        public virtual List<Solution> Solutions { get; set; }
    }
}
