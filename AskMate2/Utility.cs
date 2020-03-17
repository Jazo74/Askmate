using AskMate2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2
{
    public class Utility
    {

        public string IdGenerator()
        {
            List<string> letter = new List<string> { "q", "w", "e", "r", "y", "u", "i", "o", "h" }; //10
            List<string> num = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            List<string> special = new List<string> { "!", "@", "$", "%", "#", "(", ")", "*", "&" };
            Random r = new Random();
            string final = "";
            final += num[r.Next(0, 8)] + letter[r.Next(0, 8)] + special[r.Next(0, 8)] + letter[r.Next(0, 8)];
            return final;
        }

        public void IdChecker()
        {

        }
        public void DisplayAllQuestions(List<Question> allQuest)
        {
            foreach (var qst in allQuest)
            {
                Console.WriteLine(qst);
            }
        }

    }
}
