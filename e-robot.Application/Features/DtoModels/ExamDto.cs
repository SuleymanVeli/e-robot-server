using System;
using System.Collections.Generic;
using System.Text;

namespace e_robot.Application.Features.DtoModels
{
    public class ExamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Contents { get; set; }
        public string BlockTools { get; set; }
        public string Level { get; set; }
        public int Rank { get; set; }
        public List<SolutionListDto> Solutions { get; set; }
    }
}
