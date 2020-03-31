//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.IO;
//using System.Threading.Tasks;
//using AskMate2.Domain;

//namespace AskMate2
//{
//	public class CsvDataService : IDataService
//	{
//		public void AddQuestion(Question question)
//		{
//			string filename = "Questions.csv";
//			try
//			{
//				string id = (HighestID(filename) + 1).ToString();
//				string c = ";";
//				using (StreamWriter file = new StreamWriter(filename, true))
//				{
//					file.WriteLine(question.Id + c + question.Title + c + question.Text);
//				}
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine($"Problem occured: {e.Message}");
//			}
//		}

//		public Question GetQuestion(string questionId)
//		{
//			List<Question> questions = GetQuestions();
//			foreach (Question question in questions)
//			{
//				if (questionId == question.Id)
//				{
//					return question;
//				}
//			}
//			return null;
//		}

//		public List<Question> GetQuestions()
//		{
//			string filename = "Questions.csv";
//			List<Question> allQuestions = new List<Question>();
//			string[] line = { };
//			string id;
//			string title;
//			string text;
//			try
//			{
//				using (StreamReader file = new StreamReader(filename))
//				{
//					while (!file.EndOfStream)
//					{
//						line = file.ReadLine().Split(";").ToArray();
//						id = line[0];
//						title = line[1];
//						text = line[2];
//						Question qst = new Question(id, title, text,);
//						allQuestions.Add(qst);
//					}
//				}
//				return allQuestions;
//			}
//			catch (FileNotFoundException)
//			{
//				throw;
//			}
//		}

//		public void AddAnswer(Answer answer)
//		{
//			string filename = "Answers.csv";
//			try
//			{
//				string c = ";";
//				using (StreamWriter file = new StreamWriter(filename, true))
//				{
//					file.WriteLine(answer.AId + c + answer.QId + c + answer.Text);
//				}
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine($"Problem occured: {e.Message}");
//			}
//		}

//		public List<Answer> GetAnswers(string questionId)
//		{
//			string filename = "Answers.csv";
//			List<Answer> allAnswers = new List<Answer>();
//			string[] line = { };
//			string qid = "error";
//			string aid = "error";
//			string text = "error";
//			try
//			{
//				using (StreamReader file = new StreamReader(filename))
//				{
//					while (!file.EndOfStream)
//					{
//						line = file.ReadLine().Split(";").ToArray();
//						if (line[1] == questionId)
//						{
//							aid = line[0];
//							qid = line[1];
//							text = line[2];
//						}
//						Answer qst = new Answer(aid, qid, text);
//						allAnswers.Add(qst);
//					}
//				}
//				return allAnswers;
//			}
//			catch (FileNotFoundException)
//			{
//				throw;
//			}
//		}

//		public List<Answer> GetAllAnswers()
//		{
//			string filename = "Answers.csv";
//			List<Answer> allAnswers = new List<Answer>();
//			string[] line = { };
//			string qid = "error";
//			string aid = "error";
//			string text = "error";
//			try
//			{
//				using (StreamReader file = new StreamReader(filename))
//				{
//					while (!file.EndOfStream)
//					{
//						line = file.ReadLine().Split(";").ToArray();
//						aid = line[0];
//						qid = line[1];
//						text = line[2];
//						Answer qst = new Answer(aid, qid, text);
//						allAnswers.Add(qst);
//					}
//				}
//				return allAnswers;
//			}
//			catch (FileNotFoundException)
//			{
//				throw;
//			}
//		}

//		public void AnswerWriteToCSV2Id(string aId, string qId, string text, string filename)
//		{
//			try
//			{
//				string c = ";";
//				using (StreamWriter file = new StreamWriter(filename, true))
//				{
//					file.WriteLine(aId + c + qId + c + text);
//				}
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine($"Problem occured: {e.Message}");
//			}
//		}

//		private int HighestID(string filename)
//		{
//			string[] line = { };
//			List<int> idList = new List<int>();
//			idList.Add(0);
//			try
//			{
//				using (StreamReader file = new StreamReader(filename))
//				{
//					while (!file.EndOfStream)
//					{
//						line = file.ReadLine().Split(";").ToArray();
//						idList.Add(Int32.Parse(line[0]));
//					}
//				}
//				idList.Sort();
//				return idList[idList.Count-1];
//			}
//			catch (FileNotFoundException)
//			{
//				Console.WriteLine("File not found");
//				throw;
//			}
//		}
		
//		public void DeleteAnswer(string answerId)
//		{ 
//			List<Answer> answers = GetAllAnswers();
//			File.Delete("Answers.csv");

//			for (int i = answers.Count - 1; i >= 0; i--)
//			{
//				if (answers[i].AId == answerId)
//				{
//					answers.Remove(answers[i]);
//				}
//				else
//				{
//					AddAnswer(answers[i]);
// 				}
//			}
//		}
//		public void DeleteQuestion(string id)
//		{
//			List<Question> questions = GetQuestions();
//			File.Delete("Questions.csv");
//			for (int i = questions.Count - 1; i >= 0; i--)
//			{
//				if (questions[i].Id == id)
//				{
//					questions.Remove(questions[i]);
//				}
//			}
//			foreach (Question question in questions)
//			{
//				 AddQuestionWOId(question);
//			}
//		}

//		private void AddQuestionWOId(Question question)
//		{
//			// NO Id
//			string filename = "Questions.csv";
//			try
//			{
//				string c = ";";
//				using (StreamWriter file = new StreamWriter(filename, true))
//				{
//					file.WriteLine(question.Id + c + question.Title + c + question.Text);
//				}
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine($"Problem occured: {e.Message}");
//			}
//		}

//		public void UpdateQuestion(string qid, string title, string text)
//		{
//			string filename = "Questions.csv";
//			List<Question> questions = GetQuestions();
//			File.Delete(filename);
//			for (int i = questions.Count - 1; i >= 0; i--)
//			{
//				if (questions[i].Id == qid)
//				{
//					questions[i].Title = title;
//					questions[i].Text = text;
//					AddQuestionWOId(questions[i]);
//				}
//				else
//				{
//					AddQuestionWOId(questions[i]);
//				}
//			}
//		}

//		public void AddVoteForQuestion(string questionId)
//		{
//			throw new NotImplementedException();
//		}

//		public int GetVoteForQuestion(string questionId)
//		{
//			throw new NotImplementedException();
//		}

//		public void AddVoteForAnswer(string answerId)
//		{
//			throw new NotImplementedException();
//		}

//		public int GetVoteForAnswer(string answerId)
//		{
//			throw new NotImplementedException();
//		}

//		public Answer MakeAnswer(string answerId, string qid, string text)
//		{
//			Answer answer = new Answer(answerId, qid, text);
//			return answer;
//		}

//		public Answer MakeAnswerWoId(string qid, string text)
//		{
//			string answerId = (HighestID("Answers.csv") + 1).ToString();
//			Answer answer = new Answer(answerId, qid, text);
//			return answer;
//		}

//		public Question MakeQuestion(string questionId, string title, string text)
//		{
//			Question question = new Question(questionId, title, text);
//			return question;
//		}

//		public Question MakeQuestionWoId(string title, string text)
//		{
//			string questionId = (HighestID("Questions.csv") + 1).ToString();
//			Question question = new Question(questionId, title, text);
//			return question;
//		}
//	}
//}
