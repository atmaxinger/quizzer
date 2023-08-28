using Quizzer.TriviaServer.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quizzer.TriviaServer.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [Display(Name="Question Type")]
        public OpenTriviaQuestionType QuestionType { get; set; }
        public OpenTriviaDifficulty Difficulty { get; set; }

        [MinimumNumberOfItems(Number:2, ErrorMessage="Please add at least two answers")]
        public List<Answer>? Answers { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public enum OpenTriviaQuestionType
        {
            [Display(Name = "True/False")]
            TrueFalse,
            [Display(Name = "Multiple Choice")]
            MultipleChoice
        };

        public enum OpenTriviaDifficulty
        {
            Easy,
            Medium,
            Hard
        };
    }
}
