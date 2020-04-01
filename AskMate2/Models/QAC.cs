using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Models
{
    public class QAC
    {
        public List<QuestionModel> qModelList = new List<QuestionModel>();
        public List<AnswerModel> aModelList = new List<AnswerModel>();
        public List<CommentModel> cModelList = new List<CommentModel>();
    }
}
