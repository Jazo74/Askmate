using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Text { get; set; }

        public Question()
        {

        }

    }
}
