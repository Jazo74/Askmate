using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Models
{
    public class QuestionModel
    {
        public string Qid { get; set; }
        public string QUserId { get; set; }
        public string Qtext { get; set; }
        public string Qtitle { get; set; }
        public int Qvote { get; set; }
        public int Qview { get; set; }
        public DateTime QsubmissionTime { get; set; }
        public string Qimage { get; set; }
    }
}
