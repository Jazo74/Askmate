using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public interface IDataService
    {
        // questions
        void AddQuestion(Question question);

        Question GetQuestion(string questionId);

        List<Question> GetQuestions();

        void AddVoteForQuestion(string questionId);

        int GetVoteForQuestion(string questionId);

        void DeleteQuestion(string questionId);


        // answers
        void AddAnswer(Answer answer);

        List<Answer> GetAnswers(string questionId);

        List<Answer> GetAllAnswers();

        void AddVoteForAnswer(string answerId);

        int GetVoteForAnswer(string answerId);

        void DeleteAnswer(string answerId)

        
        

        

        




    }
}
