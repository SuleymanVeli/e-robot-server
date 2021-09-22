using e_robot.Domain.Common;
using e_robot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace e_robot.Domain.Entities
{
    public class Solution
    {
        public Solution()
        {

        }

        public Solution(int UserId, int ExamId)
        {
            this.ExamId = ExamId;
            this.UserId = UserId;
        }

        public int Id { get; set; }
        public int ExamId { get; set; }
        public int UserId { get; set; }
        public int SuccessRates { get; set; }
        public int WarningRates { get; set; }
        public int Rank { get; set; }
        public SolutionStatus Status { get; set; }
        public int? ComplateMinute { get; set; }
        public DateTime? ComplatedDate { get; set; }
        public bool? Complated { get; set; }
        public string XmlBlock { get; set; }
        public bool Delete { get; set; } = false;


        public virtual User User { get; set; }
        public virtual Exam Exam { get; set; }
    }
}
