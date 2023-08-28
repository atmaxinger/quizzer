using System.Text;

namespace Quizzer.Providers.OpenTrivia.Options
{
    public class OpenTriviaOptions
    {
        public enum OpenTriviaQuestionType
        {
            Any,
            TrueFalse,
            MultipleChoice
        };

        public enum OpenTriviaDifficulty
        {
            Any,
            Easy,
            Medium,
            Hard
        };

        public int QuestionAmount { get; set; } = 10;
        public OpenTriviaQuestionType QuestionType { get; set; } = OpenTriviaQuestionType.MultipleChoice;
        public OpenTriviaDifficulty QuestionDifficulty { get; set; } = OpenTriviaDifficulty.Any;
        public int? QuestionCategoryId { get; set; }
        public string ApiUrl { get; set; } = "https://opentdb.com";

        internal string GetUrlParameters()
        {
            var sb = new StringBuilder();
            sb.Append($"amount={QuestionAmount}");
            if (QuestionType != OpenTriviaQuestionType.Any)
            {
                sb.Append("&type=");
                if (QuestionType == OpenTriviaQuestionType.TrueFalse)
                {
                    sb.Append("boolean");
                }
                else if (QuestionType == OpenTriviaQuestionType.MultipleChoice)
                {
                    sb.Append("multiple");
                }
                else
                {
                    sb.Append("<unknown>");
                }
            }

            if (QuestionDifficulty != OpenTriviaDifficulty.Any)
            {
                sb.Append("&difficulty=");
                if (QuestionDifficulty == OpenTriviaDifficulty.Easy)
                {
                    sb.Append("easy");
                }
                else if (QuestionDifficulty == OpenTriviaDifficulty.Medium)
                {
                    sb.Append("medium");
                }
                else if (QuestionDifficulty == OpenTriviaDifficulty.Hard)
                {
                    sb.Append("hard");
                }
                else
                {
                    sb.Append("<unknown>");
                }
            }

            if (QuestionCategoryId.HasValue)
            {
                sb.Append($"&category={QuestionCategoryId}");
            }

            return sb.ToString();
        }
    }
}
