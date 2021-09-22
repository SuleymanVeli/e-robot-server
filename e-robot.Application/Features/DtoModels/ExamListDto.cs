using System;
using System.Collections.Generic;
using System.Text;

namespace e_robot.Application.Features.DtoModels
{
    public class ExamListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public int Rank { get; set; }
    }
}
