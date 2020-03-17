using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public class FakeDataLoader : IDataService
    {

        private List<Question> questions = new List<Question>
        {
            new Question {Id = 1, Title = "Q1", Text = "T1" },
            new Question {Id = 2, Title = "Q2", Text = "T2" },
            new Question {Id = 3, Title = "Q3", Text = "T3" },
            new Question {Id = 4, Title = "Q4", Text = "T4" }
        };

        public int AddQuestion(string title, string text)
        {
            int nextId = questions.Select(q => q.Id).Max() + 1;
            questions.Add(new Question { Id = nextId, Title = title, Text = text });
            return nextId;
        }

        public int CountAnswers(int questionId)
        {
            return new Random().Next();
        }

        public List<Question> GetQuestions()
        {
            return questions;
        }
       

        public Question GetQuestion(int questionId)
        {
            return questions.Where(q => q.Id == questionId).First();

        }

        void IDataService.AddQuestion(string title, string text)
        {
            throw new NotImplementedException();
        }
    }
}
