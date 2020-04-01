using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public class Comment
    {
        public string Id { get; set; }
        public string Message { get; set; }

        public DateTime SubTime { get; set; }

        public Comment(string id, string message, DateTime subTime)
        {
            Id = id;
            Message = message;
            SubTime = subTime;
        }

    }
}
