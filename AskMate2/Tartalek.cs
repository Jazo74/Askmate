//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AskMate2
//{
//    public class Tartalek
//    {

//		public void QuestionWriteToCSVNoId(int id, string title, string message, string filename)
//		{
//			// NO Id
//			try
//			{

//				string c = ";";
//				using (StreamWriter file = new StreamWriter(filename, true))
//				{
//					file.WriteLine(id + c + title + c + message);
//				}
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine($"Problem occured: {e.Message}");
//			}
//		}

//		public void DeleteQuestion(int id)
//		{
//			List<Question> questions = ReadFromCSV("Questions.csv");
//			File.Delete("Questions.csv");
//			for (int i = questions.Count - 1; i >= 0; i--)
//			{
//				if (questions[i].Id == id)
//				{
//					questions.Remove(questions[i]);
//				}
//				else
//				{
//					QuestionWriteToCSVNoId(questions[i].Id, questions[i].Title, questions[i].Text, "Questions.csv");

//				}
//			}
//		}
//1;How to lose weight?;I hate eating bird food, how can I lose weight without eating crap meal?
//2;How to not lose muscle?;I am thin enough because of jogging, but I lost muscle too.How to keep muscle and loose fat?
//3;How many meals to have a day?;What is the ideal amount of meal per day?
//4;Are you mad?;What is wrong with you?
//5;How to see the truth?;Is the truth is out of there?6



//	}
//}
