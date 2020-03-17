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
		public void WriteToCSV(string title, string message, string filename)
		{
			Utility util = new Utility();
			try
			{
				string id = util.IdGenerator();
				string c = ",";
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, true))
				{
					file.WriteLine(id + c + title + c + message);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Problem occured: {e.Message}");
			}
		}

		public List<Question> ReadFromCSV(List<Question> allQuestions, string filename)
		{
			Question qst;
			string[] tempArr = { };
			int id;
			string title;
			string text;
			try
			{
				using (System.IO.StreamReader file = new System.IO.StreamReader(filename))
				{

					string[] emptyArr = { };
					emptyArr = file.ReadLine().Split(",").ToArray();
					tempArr = emptyArr;
					id = Int32.Parse(tempArr[0]);
					title = tempArr[1];
					text = tempArr[2];
					qst = new Question(id, title, text);
					allQuestions.Add(qst);
				}
				return allQuestions;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
