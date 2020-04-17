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
        public string AUserId { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Aaccepted { get; set; }
        public DateTime SubmissionTime = new DateTime();


        public Answer(string aId, string qId, string text, string image)
        {
            AId = aId;
            QId = qId;
            Text = text;
            Image = image;
            Aaccepted = "no";
        }

        public Answer(string aId, string userId, string qId, string text, string image)
        {
            AId = aId;
            AUserId = userId;
            QId = qId;
            Text = text;
            Image = image;
            Aaccepted = "no";
            SubmissionTime = DateTime.Now;
        }

        public Answer(string aId, string userId, string qId, DateTime submissionTime, string text, string image)
        {
            AId = aId;
            AUserId = userId;
            QId = qId;
            Text = text;
            Image = image;
            Aaccepted = "no";
            SubmissionTime = submissionTime;
        }

        public override string ToString()
        {
            return $"[{AId}] {QId} ({Text})";
        }
    }
}
