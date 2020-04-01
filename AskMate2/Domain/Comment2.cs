using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public class Comment2
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public DateTime SubTime { get; set; }

        public Comment2(int id, string message, DateTime subTime)
        {
            Id = id;
            Message = message;
            SubTime = subTime;
        }
    }
}
