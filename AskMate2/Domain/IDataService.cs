using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public interface IDataService
    {
        List<Question> GetQuestions();

        Question GetQuestion(int questionId);

        void AddQuestion(string title, string text);

        //List<Answer> GetAnswers();




    }
}
