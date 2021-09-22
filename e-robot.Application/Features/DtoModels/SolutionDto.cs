using System;
using System.Collections.Generic;
using System.Text;

namespace e_robot.Application.Features.DtoModels
{
   public class SolutionDto
   {
        public int Id { get; set; }
        public int SuccessRates { get; set; }
        public int WarningRates { get; set; }
        public int Rank { get; set; }
        public string Status { get; set; }
        public int? ComplateMinute { get; set; }
        public DateTime? ComplatedDate { get; set; }
        public bool? Complated { get; set; }
        public string XmlBlock { get; set; }

        public ExamDto Exam { get; set; }

    }
}
