using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMate2;

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
            Answer answer = new Answer("fakeid", qid, text);
            return answer;
        }

        public Question MakeQuestion(string questionId, string title, string text, int voteNumber, int viewNumber, DateTime submissionTime, string image)
        {
            Question question = new Question(questionId, title, text, voteNumber, viewNumber, submissionTime, image);
            return question;
        }

        public Question MakeQuestionWoId(string title, string text, int voteNumber, int viewNumber, DateTime submissionTime, string image)
        {
            Question question = new Question("fakeid", title, text, voteNumber, viewNumber, submissionTime, image);
            return question;
        }


        public void AddQuestion(Question question) 
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                 "INSERT INTO question (submission_time, view_number, vote_number, title, question_message, image) " +
                 "VALUES (@subtime, @viewnum, @votenum, @title, @quemess, @image)", conn))
                {
                    cmd.Parameters.AddWithValue("subtime", DateTime.Now);
                    cmd.Parameters.AddWithValue("viewnum", 0);
                    cmd.Parameters.AddWithValue("votenum", 0);
                    cmd.Parameters.AddWithValue("title", question.Title);
                    cmd.Parameters.AddWithValue("quemess", question.Text);
                    cmd.Parameters.AddWithValue("image", "index.hu");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddAnswer(Answer answer)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                 "INSERT INTO answer (submission_time, vote_number, question_id, answer_message, image) " +
                 "VALUES (@subtime, @votenum, @qid, @answmess, @image)", conn))
                {
                    cmd.Parameters.AddWithValue("subtime", DateTime.Now);
                    cmd.Parameters.AddWithValue("votenum", 0);
                    cmd.Parameters.AddWithValue("qid", Convert.ToInt32(answer.QId));
                    cmd.Parameters.AddWithValue("answmess", answer.Text);
                    cmd.Parameters.AddWithValue("image", "index.hu");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAnswer(string answerId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                 "DELETE FROM answer WHERE answer_id = @aid", conn))
                {
                    cmd.Parameters.AddWithValue("aid", Convert.ToInt32(answerId));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteQuestion(string questionId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                 "DELETE FROM question WHERE question_id = @qid", conn))
                {
                    cmd.Parameters.AddWithValue("qid", int.Parse(questionId));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Answer> GetAnswers(string questionId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM answer WHERE question_id = @qid", conn))
                {
                    cmd.Parameters.AddWithValue("qid", int.Parse(questionId));
                    List<Answer> answerList = new List<Answer>();
                    var answerId = "";
                    DateTime submission_time = new DateTime();
                    var voteNumber = 0;
                    var qId = "";
                    var questionMessage = "";
                    var image = "";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        answerId = reader["answer_id"].ToString();
                        submission_time = Convert.ToDateTime(reader["submission_time"]);
                        voteNumber = Convert.ToInt32(reader["vote_number"]);
                        qId = reader["question_id"].ToString();
                        questionMessage = reader["answer_message"].ToString();
                        image = reader["image"].ToString();
                    }
                    answerList.Add(new Answer(answerId, qId, questionMessage.ToString()));
                    return answerList;
                }
            }
        }

        public Question GetQuestion(string questionId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM question WHERE question_id = @qid", conn))
                {
                    cmd.Parameters.AddWithValue("qid", int.Parse(questionId));
                    
                    var questionid = "";
                    DateTime submissionTime = new DateTime();
                    var viewNumber = 0;
                    var voteNumber = 0;
                    var title = "";
                    var questionMessage = "";
                    var image = "";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        questionid = reader["question_id"].ToString();
                        submissionTime = Convert.ToDateTime(reader["submission_time"]);
                        viewNumber = Convert.ToInt32(reader["view_number"]);
                        voteNumber = Convert.ToInt32(reader["vote_number"]);
                        title = reader["title"].ToString();
                        questionMessage = reader["question_message"].ToString();
                        image = reader["image"].ToString();
                    }
                    Question question = new Question(questionid, title.ToString(), questionMessage.ToString(), voteNumber, viewNumber, submissionTime, image);
                    return question;
                }
            }
        }

        public List<Question> GetQuestions()
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM question", conn))
                {
                    List<Question> questionList = new List<Question>();
                    var reader = cmd.ExecuteReader();
                    var questionid = "";
                    DateTime submissionTime = new DateTime();
                    var viewNumber = 0;
                    var voteNumber = 0;
                    var title = "";
                    var questionMessage = "";
                    var image = "";
                    while (reader.Read())
                    {
                        questionid = reader["question_id"].ToString();
                        submissionTime = Convert.ToDateTime(reader["submission_time"]);
                        viewNumber = Convert.ToInt32(reader["view_number"]);
                        voteNumber = Convert.ToInt32(reader["vote_number"]);
                        title = reader["title"].ToString();
                        questionMessage = reader["question_message"].ToString();
                        image = reader["image"].ToString();
                        questionList.Add(new Question(questionid, title.ToString(), questionMessage.ToString(), voteNumber, viewNumber, submissionTime, image));
                    }
                    return questionList;
                }
            }
        }

        public void UpdateQuestion(string questionId, string title, string text)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                 "UPDATE question SET title = @title, question_message = @quemess WHERE question_id = @qid", conn))
                {
                    cmd.Parameters.AddWithValue("qid", Convert.ToInt32(questionId));
                    cmd.Parameters.AddWithValue("title", title);
                    cmd.Parameters.AddWithValue("quemess", text);
                    cmd.ExecuteNonQuery();
                }
            }
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

        public void Vote(string questionId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE question SET vote_number = vote_number + 1 WHERE question_id = @qid", conn))
                {
                    cmd.Parameters.AddWithValue("qid", Int32.Parse(questionId));
                    cmd.ExecuteNonQuery();

                }


            }
        }
    }
}
