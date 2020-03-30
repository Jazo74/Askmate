using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using AskMate2.Domain;

namespace AskMate2
{
	public class CsvDataService : IDataService
	{
		public void AddQuestion(Question question)
		{
			string filename = "Questions.csv";
			try
			{
				string id = (HighestID(filename) + 1).ToString();
				string c = ";";
				using (StreamWriter file = new StreamWriter(filename, true))
				{
					file.WriteLine(question.Id + c + question.Title + c + question.Text);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Problem occured: {e.Message}");
			}
		}

		public Question GetQuestion(string questionId)
		{
			List<Question> questions = GetQuestions();
			foreach (Question question in questions)
			{
				if (questionId == question.Id)
				{
					return question;
				}
			}
			return null;
		}

		public List<Question> GetQuestions()
		{
			string filename = "Questions.csv";
			List<Question> allQuestions = new List<Question>();
			string[] line = { };
			string id;
			string title;
			string text;
			try
			{
				using (StreamReader file = new StreamReader(filename))
				{
					while (!file.EndOfStream)
					{
						line = file.ReadLine().Split(";").ToArray();
						id = line[0];
						title = line[1];
						text = line[2];
						Question qst = new Question(id, title, text);
						allQuestions.Add(qst);
					}
				}
				return allQuestions;
			}
			catch (FileNotFoundException)
			{
				throw;
			}
		}

		public void AddAnswer(Answer answer)
		{
			string filename = "Answers.csv";
			try
			{
				int id = (HighestID("Answers.csv") + 1);
				string c = ";";
				using (StreamWriter file = new StreamWriter(filename, true))
				{
					file.WriteLine(answer.AId + c + answer.QId + c + answer.Text);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Problem occured: {e.Message}");
			}
		}

		public List<Answer> GetAnswers(string questionId)
		{
			string filename = "Answers.csv";
			List<Answer> allAnswers = new List<Answer>();
			string[] line = { };
			string qid = "error";
			string aid = "error";
			string text = "error";
			try
			{
				using (StreamReader file = new StreamReader(filename))
				{
					while (!file.EndOfStream)
					{
						line = file.ReadLine().Split(";").ToArray();
						if (line[1] == questionId)
						{
							aid = line[0];
							qid = line[1];
							text = line[2];
						}
						Answer qst = new Answer(aid, qid, text);
						allAnswers.Add(qst);
					}
				}
				return allAnswers;
			}
			catch (FileNotFoundException)
			{
				throw;
			}
		}

		public List<Answer> GetAllAnswers()
		{
			string filename = "Answers.csv";
			List<Answer> allAnswers = new List<Answer>();
			string[] line = { };
			string qid = "error";
			string aid = "error";
			string text = "error";
			try
			{
				using (StreamReader file = new StreamReader(filename))
				{
					while (!file.EndOfStream)
					{
						line = file.ReadLine().Split(";").ToArray();
						aid = line[0];
						qid = line[1];
						text = line[2];
						Answer qst = new Answer(aid, qid, text);
						allAnswers.Add(qst);
					}
				}
				return allAnswers;
			}
			catch (FileNotFoundException)
			{
				throw;
			}
		}

		public void AnswerWriteToCSV2Id(string aId, string qId, string text, string filename)
		{
			try
			{
				string c = ";";
				using (StreamWriter file = new StreamWriter(filename, true))
				{
					file.WriteLine(aId + c + qId + c + text);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Problem occured: {e.Message}");
			}
		}

		private int HighestID(string filename)
		{
			string[] line = { };
			List<int> idList = new List<int>();
			idList.Add(0);
			try
			{
				using (StreamReader file = new StreamReader(filename))
				{
					while (!file.EndOfStream)
					{
						line = file.ReadLine().Split(";").ToArray();
						idList.Add(Int32.Parse(line[0]));
					}
				}
				idList.Sort();
				return idList[idList.Count-1];
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("File not found");
				throw;
			}
		}
		
		public void DeleteAnswer(string answerId)
		{ 
			List<Answer> answers = GetAllAnswers();
			File.Delete("Answers.csv");

			for (int i = answers.Count - 1; i >= 0; i--)
			{
				if (answers[i].AId == answerId)
				{
					answers.Remove(answers[i]);
				}
				else
				{
					AddAnswer(answers[i]);
 				}
			}
		}
		public void DeleteQuestion(string id)
		{
			List<Question> questions = GetQuestions();
			File.Delete("Questions.csv");
			for (int i = questions.Count - 1; i >= 0; i--)
			{
				if (questions[i].Id == id)
				{
					questions.Remove(questions[i]);
				}
			}
			foreach (Question question in questions)
			{
				 (question.Id, question.Title, question.Text, "Questions.csv");
			}
		}

		public void QuestionWriteToCSVNoId(string id, string title, string message, string filename)
		{
			// NO Id
			try
			{

				string c = ";";
				using (StreamWriter file = new StreamWriter(filename, true))
				{
					file.WriteLine(id + c + title + c + message);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Problem occured: {e.Message}");
			}
		}

		public void EditQuestion(string id, string title, string text)
		{
			List<Question> questions = ReadFromQuestionsCSV("Questions.csv");
			Question qst = new Question(id,title,text);
			for (int i = questions.Count - 1; i >= 0; i--)
			{
				if (questions[i].Id == id)
				{
					questions[i].Id = id;
					questions[i].Title = title;
					questions[i].Text = text;
					DeleteQuestion(questions[i].Id);
					QuestionWriteToCSVNoId(questions[i].Id, questions[i].Title, questions[i].Text, "Questions.csv");

				}
			}
		}

		public void AddVoteForQuestion(int questionId)
		{
			throw new NotImplementedException();
		}

		public int GetVoteForQuestion(int questionId)
		{
			throw new NotImplementedException();
		}

		public void AddVoteForAnswer(int answerId)
		{
			throw new NotImplementedException();
		}

		public int GetVoteForAnswer(int answerId)
		{
			throw new NotImplementedException();
		}
	}
}
