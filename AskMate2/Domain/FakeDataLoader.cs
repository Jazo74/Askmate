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
            new Question (1,"Q1","T1"),
            new Question (2,"Q2","T2"),
            new Question (3,"Q3","T3"),
            new Question (4,"Q4","T4")
            //new Question {Id = 4, Title = "Q4", Text = "T4" }
        };

        public int AddQuestion(string title, string text)
        {
            int nextId = questions.Select(q => q.Id).Max() + 1;
            questions.Add(new Question(nextId,title,text));
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
