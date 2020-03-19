using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using AskMate2.Domain;

namespace AskMate2
{
	public class CSV //ide a csv-s dolgok jojjenek
	{
		public void QuestionWriteToCSV(string title, string message, string filename)
		{
			try
			{
				string id = (HighestID("Questions.csv") + 1).ToString();
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

		public void AnswerWriteToCSV(string qId, string text, string filename)
		{
			try
			{
				int id = (HighestID("Answers.csv") + 1);
				string c = ";";
				using (StreamWriter file = new StreamWriter(filename, true))
				{
					file.WriteLine(id + c + qId + c + text);
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
		public List<Question> ReadFromQuestionsCSV(string filename)
		{
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

		public List<Answer> ReadFromAnswersCSV(string filename)
		{
			List<Answer> allAnswers = new List<Answer>();
			string[] line = { };
			string qid;
			string aid;
			string text;
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

		public void DeleteAnswer(string id)
		{
			List<Answer> answers = ReadFromAnswersCSV("Answers.csv");
			File.Delete("Answers.csv");

			for (int i = answers.Count - 1; i >= 0; i--)
			{
				if (answers[i].AId == id)
				{
					answers.Remove(answers[i]);
				}
				else
				{
					AnswerWriteToCSV2Id(answers[i].AId, answers[i].QId, answers[i].Text, "Answers.csv");

				}
			}
		}
		public void DeleteQuestion(string id)
		{
			List<Question> questions = ReadFromQuestionsCSV("Questions.csv");
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
				QuestionWriteToCSVNoId(question.Id, question.Title, question.Text, "Questions.csv");
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

	}
}
