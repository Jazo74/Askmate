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
        private NpgsqlConnection _conn = new NpgsqlConnection(Program.ConnectionString);
        public Answer MakeAnswer(string answerId, string qid, string text, string image)
        {
            Answer answer = new Answer(answerId, qid, text, image);
            return answer;
        }

        public Answer MakeAnswerWoId(string qid, string currentUser, string text, string image)
        {
            Answer answer = new Answer("fakeid", currentUser, qid, text, image);
            return answer;
        }

        public Question MakeQuestion(string questionId, string title, string text, int voteNumber, int viewNumber, DateTime submissionTime, string image)
        {
            Question question = new Question(questionId,  title, text, voteNumber, viewNumber, submissionTime, image);
            return question;
        }

        public Question MakeQuestionWoId(string title, string currentUser, string text, int voteNumber, int viewNumber, DateTime submissionTime, string image)
        {
            Question question = new Question("fakeid", currentUser, title, text, voteNumber, viewNumber, submissionTime, image);
            return question;
        }
        //js time
        
        public void AddQuestion(Question question) 
        {
            
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand( // no string concantination (SQL Injection Danger)
                 "INSERT INTO question (user_id, submission_time, view_number, vote_number, title, question_message, image) VALUES (@user_id, @subtime, @viewnum, @votenum, @title, @quemess, @image)", conn))
                {
                    cmd.Parameters.AddWithValue("user_id", question.UserId);
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
                 "INSERT INTO answer (submission_time, user_id, vote_number, question_id, answer_message, image) " +
                 "VALUES (@subtime, @user_id, @votenum, @qid, @answmess, @image)", conn))
                {
                    cmd.Parameters.AddWithValue("subtime", DateTime.Now);
                    cmd.Parameters.AddWithValue("user_id", answer.AUserId);
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
                        answerList.Add(new Answer(answerId, qId, questionMessage.ToString(), image));
                    }
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
                    var qUserId = "";
                    DateTime submissionTime = new DateTime();
                    var viewNumber = 0;
                    var voteNumber = 0;
                    var title = "";
                    var questionMessage = "";
                    var image = "";
                    while (reader.Read())
                    {
                        questionid = reader["question_id"].ToString();
                        qUserId = reader["user_id"].ToString();
                        submissionTime = Convert.ToDateTime(reader["submission_time"]);
                        viewNumber = Convert.ToInt32(reader["view_number"]);
                        voteNumber = Convert.ToInt32(reader["vote_number"]);
                        title = reader["title"].ToString();
                        questionMessage = reader["question_message"].ToString();
                        image = reader["image"].ToString();
                        questionList.Add(new Question(questionid, qUserId, title, questionMessage, voteNumber, viewNumber, submissionTime, image));
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
        public void DownVote(string questionId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE question SET vote_number = vote_number - 1 WHERE question_id = @qid", conn))
                {
                    cmd.Parameters.AddWithValue("qid", Int32.Parse(questionId));
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public string GetUserFromQuestion(string questionId)
        {
            int qid = Convert.ToInt32(questionId);
            string user_id = "";
            using (_conn)
            {
                _conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT user_id FROM question WHERE question_id = @qid", _conn))
                {
                    cmd.Parameters.AddWithValue("qid", qid);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user_id = reader["user_id"].ToString();
                    }

                }
            }
            return user_id;
        }

        public string GetUserFromAnswer(string answerId)
        {
            int aid = Convert.ToInt32(answerId);
            string user_id = "";
            using (_conn)
            {
                _conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT user_id FROM answer WHERE answer_id = @aid", _conn))
                {
                    cmd.Parameters.AddWithValue("aid", aid);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user_id = reader["user_id"].ToString();
                    }

                }
            }
            return user_id;
        }

        public void IncreaseReputation(string userId, int amount)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE \"user\" SET reputation = reputation + @amount WHERE user_id = @uid", conn))
                {
                    cmd.Parameters.AddWithValue("uid", userId);
                    cmd.Parameters.AddWithValue("amount", amount);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DecreaseReputation(string userId, int amount)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE \"user\" SET reputation = reputation - @amount WHERE user_id = @uid", conn))
                {
                    cmd.Parameters.AddWithValue("uid", userId);
                    cmd.Parameters.AddWithValue("amount", amount);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //this is 10 points, regular vote for answer UP
        //public void IncreaseReputationAnswer(string answerId) 
        //{
        //    int amount = 10;
        //    using (var conn = new NpgsqlConnection(Program.ConnectionString))
        //    {
        //        conn.Open();
        //        using (var cmd = new NpgsqlCommand("UPDATE \"user\" SET reputation = reputation + @amount WHERE answer_id = @aid", conn))
        //        {
        //            cmd.Parameters.AddWithValue("aid", answerId);
        //            cmd.Parameters.AddWithValue("amount", amount);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

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
        public void AnswerDownVote(string answerId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE answer SET vote_number = vote_number - 1 WHERE answer_id = @aid", conn))
                {
                    cmd.Parameters.AddWithValue("aid", Int32.Parse(answerId));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddCommentQuestion(string questionId, string komment, string currentUser)
        {
            DateTime subTime = DateTime.Now;
            int edit = 0;
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO komment(question_id, user_id, komment_message, submission_time, edited_number) VALUES (@qid, @currentUser, @komment, @subTime, @edit);", conn))
                {
                    cmd.Parameters.AddWithValue("qid", Int32.Parse(questionId));
                    cmd.Parameters.AddWithValue("currentUser",currentUser);
                    cmd.Parameters.AddWithValue("subTime", subTime);
                    cmd.Parameters.AddWithValue("komment", komment);
                    cmd.Parameters.AddWithValue("subTime", subTime);
                    cmd.Parameters.AddWithValue("edit", edit);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void AddCommentAnswer(string answerId, string komment, string currentUser)
        {
            DateTime subTime = DateTime.Now;
            int edit = 0;
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO komment(answer_id, user_id, komment_message, submission_time, edited_number) VALUES (@aid, @currentUser, @komment, @subTime, @edit)", conn))
                {
                    cmd.Parameters.AddWithValue("aid", Int32.Parse(answerId));
                    cmd.Parameters.AddWithValue("currentUser", currentUser);
                    cmd.Parameters.AddWithValue("komment", komment);
                    cmd.Parameters.AddWithValue("subTime", subTime);
                    cmd.Parameters.AddWithValue("edit", edit);
                    cmd.ExecuteNonQuery();
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
                using (var cmd = new NpgsqlCommand("UPDATE answer SET answer_message = @message, image = @img WHERE answer_id = @aid", conn))
                {
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
                        var qUserId = reader["user_id"].ToString();
                        var qSubmissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var qViewNumber = Convert.ToInt32(reader["view_number"]);
                        var qVoteNumber = Convert.ToInt32(reader["vote_number"]);
                        var qTitle = reader["title"].ToString();
                        var questionMessage = reader["question_message"].ToString();
                        var qImage = reader["image"].ToString();
                        QuestionModel qModel = new QuestionModel();
                        qModel.Qid = questionid;
                        qModel.QUserId = qUserId;
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
                        var aUserId = reader["user_id"].ToString();
                        var aSubmissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var aVoteNumber = Convert.ToInt32(reader["vote_number"]);
                        var answerMessage = reader["answer_message"].ToString();
                        var aImage = reader["image"].ToString();
                        var aAccepted = reader["accepted"].ToString();
                        AnswerModel aModel = new AnswerModel();
                        aModel.Aid = answerid;
                        aModel.AUserId = aUserId;
                        aModel.Atext = answerMessage.ToString();
                        aModel.Avote = aVoteNumber;
                        aModel.AsubmissionTime = aSubmissionTime;
                        aModel.Aimage = aImage;
                        aModel.Aaccepted = aAccepted;
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
                        var cUserId = reader["user_id"].ToString();                        
                        var qId = reader["question_id"].ToString();
                        var aId = reader["answer_id"].ToString();
                        var cSubmissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var commentMessage = reader["komment_message"].ToString();
                        var cEditNr = Convert.ToInt32(reader["edited_number"]);
                        CommentModel cModel = new CommentModel();
                        cModel.Cid = kommentId;
                        cModel.CUserid = cUserId;
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
                        allUsers.Add(new User(id, email, password, reputation));
                    }

                    return allUsers;
                }
            }
        }

        public string GetUserId(string email)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                var userId = "";
                if (email == null)
                {
                    email = "";
                }
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT user_id FROM \"user\" " +
                    "WHERE email = @email", conn))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userId = reader["user_id"].ToString();
                    }

                    return userId;
                }
            }
        }


        public void RegisterUser(string user_id, string email, string password)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO \"user\"(user_id, email, \"password\", reputation, registration_date) VALUES(@user_id, @email, @password, @reputation, @registration_date)"))
                {
                    int reputation = 0;
                    DateTime registration_date = DateTime.Now;
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@reputation", reputation);
                    cmd.Parameters.AddWithValue("@registration_date", registration_date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public QAC GetQACByUserId(string userId)
        {
            QAC qac = new QAC();
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {

                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM question " +
                    "WHERE user_id = @userId ORDER BY submission_time DESC", conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var questionid = reader["question_id"].ToString();
                        var qUserId = reader["user_id"].ToString();
                        var qSubmissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var qViewNumber = Convert.ToInt32(reader["view_number"]);
                        var qVoteNumber = Convert.ToInt32(reader["vote_number"]);
                        var qTitle = reader["title"].ToString();
                        var questionMessage = reader["question_message"].ToString();
                        var qImage = reader["image"].ToString();
                        QuestionModel qModel = new QuestionModel();
                        qModel.Qid = questionid;
                        qModel.QUserId = qUserId;
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
                    "WHERE user_id = @userId ORDER BY submission_time DESC", conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var answerid = reader["answer_id"].ToString();
                        var aUserId = reader["user_id"].ToString();
                        var aSubmissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var aVoteNumber = Convert.ToInt32(reader["vote_number"]);
                        var answerMessage = reader["answer_message"].ToString();
                        var aImage = reader["image"].ToString();
                        var aAccepted = reader["accepted"].ToString();
                        AnswerModel aModel = new AnswerModel();
                        aModel.Aid = answerid;
                        aModel.AUserId = aUserId;
                        aModel.Atext = answerMessage.ToString();
                        aModel.Avote = aVoteNumber;
                        aModel.AsubmissionTime = aSubmissionTime;
                        aModel.Aimage = aImage;
                        aModel.Aaccepted = aAccepted;
                        qac.aModelList.Add(aModel);
                    }
                    conn.Close();
                }
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM komment " +
                    "WHERE user_id = @userId ORDER BY submission_time DESC", conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var kommentId = reader["komment_id"].ToString();
                        var cUserId = reader["user_id"].ToString();
                        var qId = reader["question_id"].ToString();
                        var aId = reader["answer_id"].ToString();
                        var cSubmissionTime = Convert.ToDateTime(reader["submission_time"]);
                        var commentMessage = reader["komment_message"].ToString();
                        var cEditNr = Convert.ToInt32(reader["edited_number"]);
                        CommentModel cModel = new CommentModel();
                        cModel.Cid = kommentId;
                        cModel.CUserid = cUserId;
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

        public void AccepAnswer(string answerId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE answer " +
                    "SET accepted = 'yes' WHERE answer_id = @aid", conn))
                {
                    cmd.Parameters.AddWithValue("aid", Int32.Parse(answerId));
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public Answer GetAnswer(string aId)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM answer WHERE answer_id = @aid", conn))
                {
                    cmd.Parameters.AddWithValue("aid", int.Parse(aId));
                    var answerId = "";
                    var userId = "";
                    DateTime submission_time = new DateTime();
                    var voteNumber = 0;
                    var qId = "";
                    var questionMessage = "";
                    var image = "";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        answerId = reader["answer_id"].ToString();
                        userId = reader["user_id"].ToString();
                        submission_time = Convert.ToDateTime(reader["submission_time"]);
                        voteNumber = Convert.ToInt32(reader["vote_number"]);
                        qId = reader["question_id"].ToString();
                        questionMessage = reader["answer_message"].ToString();
                        image = reader["image"].ToString();
                        
                    }
                    Answer answer = new Answer(answerId, userId, qId, submission_time, questionMessage.ToString(), image);
                    return answer;
                }
            }
        }
    }
}

