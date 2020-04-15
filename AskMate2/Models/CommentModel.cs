using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Models
{
    public class CommentModel
    {
        public string Cid { get; set; }
        public string CUserid { get; set; }
        public string Qid { get; set; }
        public string Aid { get; set; }
        public string Ctext { get; set; }
        public DateTime CsubmissionTime { get; set; }
        public int CeditNr { get; set; }
    }
}
