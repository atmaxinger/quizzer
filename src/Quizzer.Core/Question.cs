namespace Quizzer.Core
{
    public class Question
    {
        public string Text { get; set; } = "";
        public List<string> Answers { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; } = 0;

        public void ShuffleAnswers(Random? random = null)
        {
            if (random is null)
            {
                random = Random.Shared;
            }

            var correctAnswer = Answers[CorrectAnswerIndex];
            Answers.RemoveAt(CorrectAnswerIndex);

            Answers = Answers.OrderBy(a => random.Next()).ToList();
            CorrectAnswerIndex = random.Next(0, Answers.Count + 1);
            Answers.Insert(CorrectAnswerIndex, correctAnswer);
        }
    }
}
