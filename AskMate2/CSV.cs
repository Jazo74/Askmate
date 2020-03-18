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
		public int HighestID(string filename)
		{
			string[] line = { };
			List<int> idList = new List<int>();
			int id;
			try
			{
				using (StreamReader file = new StreamReader(filename))
				{
					while (file.EndOfStream)
					{
						line = file.ReadLine().Split(",").ToArray();
						idList.Add(Int32.Parse(line[0]));
					}
					idList.Sort();
				}
				return idList[-1];
			}
			catch (Exception)
			{
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
					while (file.EndOfStream)
					{
						line = file.ReadLine().Split(",").ToArray();
						id = Int32.Parse(line[0]);
						title = line[1];
						text = line[2];
						Question qst = new Question(id, title, text);
						allQuestions.Add(qst);
					}
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
