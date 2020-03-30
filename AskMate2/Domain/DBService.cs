using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public class DBService : IDataService
    {
        public Answer MakeAnswer(string answerId, string qid, string text)
        {
            Answer answer = new Answer(answerId, qid, text);
            return answer;
        }

        public Answer MakeAnswerWoId(string qid, string text)
        {
            throw new NotImplementedException();
            /*string answerId = (HighestID("Answers.csv") + 1).ToString();
            Answer answer = new Answer(answerId, qid, text);
            return answer;*/
        }

        public Question MakeQuestion(string questionId, string title, string text)
        {
            Question question = new Question(questionId, title, text);
            return question;
        }

        public Question MakeQuestionWoId(string title, string text)
        {
            throw new NotImplementedException();
            /*string questionId = (HighestID("Questions.csv") + 1).ToString();
            Question question = new Question(questionId, title, text);
            return question;*/
        }


        public void AddQuestion(Question question)
        {

            using (var cmd = new NpgsqlCommand(
                "INSERT INTO question (submission_time, view_number, vote_number, title, question_message, image) " +
                "VALUES ((@p)", conn))
            {
                cmd.Parameters.AddWithValue("p", "some_value");
                cmd.ExecuteNonQuery();
            }
            // throw new NotImplementedException();
        }


        public void AddAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }

        public void DeleteAnswer(string answerId)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuestion(string questionId)
        {
            throw new NotImplementedException();
        }

        public List<Answer> GetAllAnswers()
        {
            throw new NotImplementedException();
        }

        public List<Answer> GetAnswers(string questionId)
        {
            throw new NotImplementedException();
        }

        public Question GetQuestion(string questionId)
        {
            throw new NotImplementedException();
        }

        public List<Question> GetQuestions()
        {
            throw new NotImplementedException();
        }

        

        public void UpdateQuestion(string questionId, string title, string text)
        {
            throw new NotImplementedException();
        }

        /// VOTE
        
        public void AddVoteForAnswer(string answerId)
        {
            throw new NotImplementedException();
        }

        public void AddVoteForQuestion(string questionId)
        {
            throw new NotImplementedException();
        }

        public int GetVoteForAnswer(string answerId)
        {
            throw new NotImplementedException();
        }

        public int GetVoteForQuestion(string questionId)
        {
            throw new NotImplementedException();
        }
    }
}
