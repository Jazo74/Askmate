using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Models
{
    public class AnswerModel
    {
        public string Aid { get; set; }
        public string AUserId { get; set; }
        public string Atext { get; set; }
        public int Avote { get; set; }
        public DateTime AsubmissionTime { get; set; }
        public string Aimage { get; set; }
        public string Aaccepted { get; set; }
    }
}
