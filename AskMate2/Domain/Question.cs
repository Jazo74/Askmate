﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public class Question
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int VoteNumber { get; set; }
        public int ViewNumber { get; set; }
        DateTime SubmissionTime = new DateTime();
        string Image { get; set; }


        public Question(string id, string title, string text, int voteNumber, int viewNumber, DateTime submissionTime, string image)
        {
            Id = id;
            Title = title;
            Text = text;
            VoteNumber = voteNumber;
            ViewNumber = viewNumber;
            SubmissionTime = submissionTime;
            Image = image;
        }

        public override string ToString()
        {
            return $"[{Id}] {Title} ({Text})";
        }
    }
}
