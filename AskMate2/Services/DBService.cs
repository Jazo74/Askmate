using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMate2;
using AskMate2.Models;

namespace AskMate2.Domain
{
    public class DBService : IDataService
    {
        public List<User> allUsers = new List<User>();
        public Answer MakeAnswer(string answerId, string qid, string text, string image)
        {
            Answer answer = new Answer(answerId, qid, text, image);
            return answer;
        }

        public Answer MakeAnswerWoId(string qid, string text, string image)
        {
            Answer answer = new Answer("fakeid", qid, text, image);
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
        //js time
        
        public void AddQuestion(Question question) 
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand( // no string concantination (SQL Injection Danger)
                 "INSERT INTO question (submission_time, view_number, vote_number, title, question_message, image) VALUES (@subtime, @viewnum, @votenum, @title, @quemess, @image)", conn))
                {
                    cmd.Parameters.AddWithValue("subtime", DateTime.Now);
                    cmd.Parameters.AddWithValue("viewnum", 0);
                    cmd.Parameters.AddWithValue("votenum", 0);
                    cmd.Parameters.AddWithValue("title", question.Title);
                    cmd.Parameters.AddWithValue("quemess", question.Text);
                    cmd.Parameters.AddWithValue("image", question.Image);
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
                    cmd.Parameters.AddWithValue("image", answer.Image);
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
                    answerList.Add(new Answer(answerId, qId, questionMessage.ToString(), image));
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

        public void ViewIncrement(string questionId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE question SET view_number = view_number + 1 WHERE question_id = @qid", conn))
                {
                    cmd.Parameters.AddWithValue("qid", Int32.Parse(questionId));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AnswerVote(string answerId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE answer SET vote_number = vote_number + 1 WHERE answer_id = @aid", conn))
                {
                    cmd.Parameters.AddWithValue("aid", Int32.Parse(answerId));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddCommentQuestion(string questionId, string komment)
        {
            DateTime subTime = DateTime.Now;
            int edit = 0;
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO komment(question_id, komment_message, submission_time, edited_number) VALUES (@qid, @komment, @subTime, @edit);", conn))
                {
                    cmd.Parameters.AddWithValue("qid", Int32.Parse(questionId));
                    cmd.Parameters.AddWithValue("komment", komment);
                    cmd.Parameters.AddWithValue("subTime", subTime);
                    cmd.Parameters.AddWithValue("edit", edit);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void AddCommentAnswer(string answerId, string komment)
        {
            DateTime subTime = DateTime.Now;
            int edit = 0;
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO komment(answer_id, komment_message, submission_time, edited_number) VALUES (@aid, @komment, @subTime, @edit)", conn))
                {
                    cmd.Parameters.AddWithValue("aid", Int32.Parse(answerId));
                    cmd.Parameters.AddWithValue("komment", komment);
                    cmd.Parameters.AddWithValue("subTime", subTime);
                    cmd.Parameters.AddWithValue("edit", edit);
                    cmd.ExecuteNonQuery(); //this has to be here'
                }
            }
        }







        public void AddImageToQuestion(string questionId, string image)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE question SET image=@image WHERE question_id = @qid", conn))
                {
                    cmd.Parameters.AddWithValue("qid", Int32.Parse(questionId));
                    cmd.Parameters.AddWithValue("image", image);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddImageToAnswer(string answerId, string image)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE answer SET image=@image WHERE answer_id = @aid", conn))
                {
                    cmd.Parameters.AddWithValue("aid", Int32.Parse(answerId));
                    cmd.Parameters.AddWithValue("image", image);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Question> GetQuestions(string word, int minVotes, DateTime from, DateTime to)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                List<Question> questionList = new List<Question>();
                conn.Open();
                word = "%" + word + "%";
                using (var cmd = new NpgsqlCommand("SELECT * FROM question WHERE " + 
                    "(question_message ILIKE @word OR title ILIKE @word) AND vote_number >= @minVotes " + "" +
                    "AND submission_time >= @from AND submission_time <= @to ORDER BY submission_time DESC", conn))
                {
                    cmd.Parameters.AddWithValue("word", word);
                    cmd.Parameters.AddWithValue("minVotes", minVotes);
                    cmd.Parameters.AddWithValue("from", from);
                    cmd.Parameters.AddWithValue("to", to);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var questionid = reader["question_id"].ToString();
                        var submissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var viewNumber = Convert.ToInt32(reader["view_number"]);
                        var voteNumber = Convert.ToInt32(reader["vote_number"]);
                        var title = reader["title"].ToString();
                        var questionMessage = reader["question_message"].ToString();
                        var image = reader["image"].ToString();
                        Question question = new Question(questionid, title.ToString(), questionMessage.ToString(), voteNumber, viewNumber, submissionTime, image);
                        questionList.Add(question);
                    }
                    
                    return questionList;
                }
            }
        }







        public void EditCommentAnswer(string answerId, string komment)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE komment SET komment_message = @komment,edited_number  = edited_number +1 WHERE answer_id = @aid", conn))
                {
                    cmd.Parameters.AddWithValue("aid", Int32.Parse(answerId));
                    cmd.Parameters.AddWithValue("komment",komment);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditCommentQuestion(string questionId, string komment)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();//or AND
                using (var cmd = new NpgsqlCommand("UPDATE komment SET komment_message  = @komment, edited_number  = edited_number +1 WHERE question_id = @qid", conn))
                {
                    cmd.Parameters.AddWithValue("qid", Int32.Parse(questionId));
                    cmd.Parameters.AddWithValue("komment", komment);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Question> GetQuestions(int latestX)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                List<Question> questionList = new List<Question>();
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM question " +
                    "ORDER BY submission_time DESC LIMIT @latestX", conn))
                {
                    cmd.Parameters.AddWithValue("latestX", latestX);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var questionid = reader["question_id"].ToString();
                        var submissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var viewNumber = Convert.ToInt32(reader["view_number"]);
                        var voteNumber = Convert.ToInt32(reader["vote_number"]);
                        var title = reader["title"].ToString();
                        var questionMessage = reader["question_message"].ToString();
                        var image = reader["image"].ToString();
                        Question question = new Question(questionid, title.ToString(), questionMessage.ToString(), voteNumber, viewNumber, submissionTime, image);
                        questionList.Add(question);
                    }

                    return questionList;
                }
            }
        }


        public void EditAnswer(string answerId, string message, string image)
        {
            DateTime subTime = DateTime.Now;
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE answer SET submission_time = @subTime, answer_message = @message, image = @img WHERE answer_id = @aid", conn))
                {
                    cmd.Parameters.AddWithValue("subTime", subTime);
                    cmd.Parameters.AddWithValue("message", message);
                    cmd.Parameters.AddWithValue("img", image);
                    cmd.Parameters.AddWithValue("aid", Int32.Parse(answerId));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCommentQuestion(string questionId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();//or AND
                using (var cmd = new NpgsqlCommand("DELETE FROM komment WHERE question_id = @qid", conn))
                {
                    cmd.Parameters.AddWithValue("qid", Int32.Parse(questionId));
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public QAC GetQuestionWithAnswers(string questionId)
        {
            QAC qac = new QAC();
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {

                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM question " +
                    "WHERE question_id = @questionId", conn))
                {
                    cmd.Parameters.AddWithValue("questionId", Int32.Parse(questionId));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var questionid = reader["question_id"].ToString();
                        var qSubmissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var qViewNumber = Convert.ToInt32(reader["view_number"]);
                        var qVoteNumber = Convert.ToInt32(reader["vote_number"]);
                        var qTitle = reader["title"].ToString();
                        var questionMessage = reader["question_message"].ToString();
                        var qImage = reader["image"].ToString();
                        QuestionModel qModel = new QuestionModel();
                        qModel.Qid = questionid;
                        qModel.Qtitle = qTitle.ToString();
                        qModel.Qtext = questionMessage.ToString();
                        qModel.Qvote = qVoteNumber;
                        qModel.Qview = qViewNumber;
                        qModel.QsubmissionTime = qSubmissionTime;
                        qModel.Qimage = qImage;
                        qac.qModelList.Add(qModel);
                    }
                    conn.Close();
                }
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM answer " +
                    "WHERE question_id = @questionId ORDER BY submission_time DESC", conn))
                {
                    cmd.Parameters.AddWithValue("questionId", Int32.Parse(questionId));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var answerid = reader["answer_id"].ToString();
                        var aSubmissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var aVoteNumber = Convert.ToInt32(reader["vote_number"]);
                        var answerMessage = reader["answer_message"].ToString();
                        var aImage = reader["image"].ToString();
                        AnswerModel aModel = new AnswerModel();
                        aModel.Aid = answerid;
                        aModel.Atext = answerMessage.ToString();
                        aModel.Avote = aVoteNumber;
                        aModel.AsubmissionTime = aSubmissionTime;
                        aModel.Aimage = aImage;
                        qac.aModelList.Add(aModel);
                    }
                    conn.Close();
                }
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM komment " +
                    "WHERE question_id = @questionId OR answer_id IN " +
                    "(SELECT answer_id FROM answer WHERE question_id = @questionId) " +
                    "ORDER BY submission_time DESC", conn))
                {
                    cmd.Parameters.AddWithValue("questionId", Int32.Parse(questionId));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var kommentId = reader["komment_id"].ToString();
                        var qId = reader["question_id"].ToString();
                        var aId = reader["answer_id"].ToString();
                        var cSubmissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var commentMessage = reader["komment_message"].ToString();
                        var cEditNr = Convert.ToInt32(reader["edited_number"]);
                        CommentModel cModel = new CommentModel();
                        cModel.Cid = kommentId;
                        cModel.Qid = qId;
                        cModel.Aid = aId;
                        cModel.Ctext = commentMessage;
                        cModel.CsubmissionTime = cSubmissionTime;
                        cModel.CeditNr = cEditNr;
                        qac.cModelList.Add(cModel);
                    }
                    return qac;
                }
            }
        }


        public List<User> GetAllUsers()
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM \"user\"", conn))
                {
                    List<User> userList = new List<User>();
                    string id = "";
                    string email = "";
                    string password = "";
                    int reputation = 0;

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        id = reader["user_id"].ToString();
                        email = reader["email"].ToString();
                        password = reader["password"].ToString();
                        reputation = Convert.ToInt32(reader["reputation"]);
                    }
                    allUsers.Add(new User(id, email, password, reputation));
                    return allUsers;
                }
            }
        }


        



    }
}

