using System;

namespace AskMate2.Models
{
    public class Transit
    {
        public string Qid { get; set; }
        public string Aid { get; set; }
        public string Qtext { get; set; }
        public string Atext { get; set; }
        public string Qtitle { get; set; }
        public int Qvote { get; set; }
        public int Avote { get; set; }
    }
}
