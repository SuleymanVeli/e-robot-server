using e_robot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace e_robot.Domain.Entities
{
    public class Exam 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Contents { get; set; }
        public string BlockTools { get; set; }
        public string Level { get; set; }
        public int Position { get; set; }
        public int Rank { get; set; }
        public virtual List<Solution> Solutions { get; set; }
    }
}
