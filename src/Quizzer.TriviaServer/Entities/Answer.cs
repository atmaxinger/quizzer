using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzer.TriviaServer.Entities
{
    public class Answer
    {
        public int Order { get; set; }

        public string Text { get; set; }

        [Display(Name = "Correct Answer")]
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
