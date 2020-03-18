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
		public void AnswerWriteToCSV(string qID, string answer, string filename)
		{
			try
			{
				string id = (HighestID("Answers.csv") + 1).ToString();
				string c = ";";
				using (StreamWriter file = new StreamWriter(filename, true))
				{
					file.WriteLine(id + c + qID + c + answer);
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
		public List<Question> ReadFromCSV(string filename)
		{
			List<Question> allQuestions = new List<Question>();
			string[] line = { };
			int id;
			string title;
			string text;
			try
			{
				using (StreamReader file = new StreamReader(filename))
				{
					while (!file.EndOfStream)
					{
						line = file.ReadLine().Split(";").ToArray();
						id = Int32.Parse(line[0]);
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
	}
}
