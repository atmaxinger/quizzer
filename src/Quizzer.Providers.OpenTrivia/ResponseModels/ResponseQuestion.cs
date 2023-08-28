using Quizzer.Core;
using System.Text.Json.Serialization;

namespace Quizzer.Providers.OpenTrivia.ResponseModels
{
    public class ResponseQuestion
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("difficulty")]
        public string Difficulty { get; set; }

        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("correct_answer")]
        public string CorrectAnswer { get; set; }

        [JsonPropertyName("incorrect_answers")]
        public List<string> IncorrectAnswers { get; set; }

        public Question ToCoreQuestion()
        {
            var question = new Question
            {
                Text = Question
            };

            question.Answers = new List<string>(IncorrectAnswers)
            {
                CorrectAnswer
            };
            question.CorrectAnswerIndex = question.Answers.Count - 1;
            return question;
        }
    }
}
