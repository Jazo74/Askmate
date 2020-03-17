using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

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

		public void ReadFromCSV(string filename)
		{
			try
			{
				using (System.IO.StreamReader reader = new System.IO.StreamReader(filename))
				{

				}
			}
			catch (Exception)
			{

				throw;
			}
		}



    }
}
