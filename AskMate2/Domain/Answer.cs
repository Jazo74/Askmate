using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public class Answer
    {
        public int Id { get; set; } //OK?

        public string Message { get; set; }

        public int QuestionId { get; set; }


    }
}
