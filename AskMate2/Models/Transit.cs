using System;

namespace AskMate2.Models
{
    public class Transit
    {
        public string Qid { get; set; }
        public string Qtext { get; set; }
        public string Qtitle { get; set; }
        public int Qvote { get; set; }
        public int Qview { get; set; }
        public DateTime QsubmissionTime { get; set; }
        public string Qimage { get; set; }

        public string Aid { get; set; }
        public string Atext { get; set; }
        public int Avote { get; set; }
        public DateTime AsubmissionTime { get; set; }
        public string Aimage { get; set; }




    }
}
