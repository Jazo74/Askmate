﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMate2.Models;

namespace AskMate2.Domain
{
    public interface IDataService
    {
        // questions

        Question MakeQuestion(string questionId, string title, string text, int voteNumber, int viewNumber, DateTime submissionTime, string image);

        Question MakeQuestionWoId(string title, string text, int voteNumber, int viewNumber, DateTime submissionTime, string image);

        void AddQuestion(Question question);

        Question GetQuestion(string questionId);

        List<Question> GetQuestions();

        void AddVoteForQuestion(string questionId);

        int GetVoteForQuestion(string questionId);

        void DeleteQuestion(string questionId);

        void UpdateQuestion(string questionId, string title, string text);

        List<Question> GetQuestions(string word, int minVotes, DateTime from, DateTime to);

        List<Question> GetQuestions(int latestX);




        // answers

        Answer MakeAnswer(string answerId, string questionId, string text);

        Answer MakeAnswerWoId(string questionId, string text);

        void AddAnswer(Answer answer);

        List<Answer> GetAnswers(string questionId);

        void AddVoteForAnswer(string answerId);

        void AddImageToAnswer(string answerId, string image);
        int GetVoteForAnswer(string answerId);

        void DeleteAnswer(string answerId);

        void Vote(string qestionId);

        QAC GetQuestionWithAnswers(string questionId);

        void ViewIncrement(string questionId);
        void AnswerVote(string answerId);

        void AddCommentQuestion(string questionId, string komment);

        void AddCommentAnswer(string answerId, string komment);

        void AddImageToQuestion(string questionId, string image);

        void EditCommentQuestion(string questionId, string komment);

        void EditCommentAnswer(string answernId, string komment);


        void DeleteCommentQuestion(string questionId);
        //void DeleteCommentAnswer(string answerId);


        void EditAnswer(string answerId, string message, string image);

    }
}
