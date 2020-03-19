//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AskMate2.Domain
//{
//    public class FakeDataLoader : IDataService
//    {

//        private List<Question> questions = new List<Question>
//        {
//            new Question("1","Question 1 title","What is wrong with you?"),
//            new Question("2","Question 2 title","What is that thing about number 42?"),
//            new Question("3","Question 3 title","Corona beer, nay or yay?"),
//            new Question("4","Question 4 title","Placeholder question?")
//        };

//        //public int AddQuestion(string title, string text)
//        //{
//        //    int nextId = questions.Select(q => q.Id).Max() + 1;
//        //    questions.Add(new Question(nextId,title,text));
//        //    return nextId;
//        //}

//        public int CountAnswers(int questionId)
//        {
//            return new Random().Next();
//        }

//        public List<Question> GetQuestions()
//        {
//            return questions;
//        }
       

//        public Question GetQuestion(int questionId)
//        {
//            return questions.Where(q => q.Id == questionId).First();

//        }

//        void IDataService.AddQuestion(string title, string text)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
