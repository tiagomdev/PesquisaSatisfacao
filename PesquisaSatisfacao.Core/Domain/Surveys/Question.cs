using PesquisaSatisfacao.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PesquisaSatisfacao.Core.Domain.Surveys
{
    public class Question
    {
        public Question()
        {

        }

        public Question(string description, int surveyId, int categoryId)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Descricao nao enviada");
            if (categoryId == default(int)) throw new ArgumentException("Categoria nao enviada");

            Description = description;
            SurveyId = surveyId;
            CategoryId = categoryId;
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public Survey Survey { get; set; }
        public int SurveyId { get; set; }

        public QuestionCategory Category { get; set; }
        public int CategoryId { get; set; }
    }
}
