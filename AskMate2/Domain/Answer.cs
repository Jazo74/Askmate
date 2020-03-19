using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public class Answer
    {
        public string AId { get; set; }
        public string QId { get; set; }

        public string Text { get; set; }

        public Answer(string Aid, string QId, string Text)
        {
            this.AId = Aid;
            this.QId = QId;
            this.Text = Text;
        }

        public override string ToString()
        {
            return $"[{AId}] {QId} ({Text})";
        }
    }
}
