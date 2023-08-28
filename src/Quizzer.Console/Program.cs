using Microsoft.Extensions.DependencyInjection;
using Quizzer.Core;
using Quizzer.Providers.OpenTrivia;

namespace Quizzer.Console
{
    internal class Program
    {
        private static int PromptUser(string prompt, int maxNumber)
        {
            int answeredNumber = 0;
            bool validAnswer = false;
            do
            {
                System.Console.Write(prompt);
                var userAnswer = System.Console.ReadLine();

                if (int.TryParse(userAnswer, out answeredNumber))
                {
                    if (answeredNumber > 0 && answeredNumber <= maxNumber)
                    {
                        validAnswer = true;
                    }
                }

                if (!validAnswer)
                {
                    System.Console.WriteLine($"Invalid answer. Must be a number in the range from 1 to {maxNumber}");
                }
            } while (!validAnswer);

            return answeredNumber;
        }

        static async Task Main(string[] args)
        {
            ServiceCollection sc = new ServiceCollection();
            sc.AddOpenTriviaProvider(options =>
            {
                options.QuestionAmount = 15;
                // Uncomment to use our implementation of the server instead
                // options.ApiUrl = "http://localhost:5204";
            });

            var services = sc.BuildServiceProvider();
            var provider = services.GetRequiredService<IQuestionsProvider>();
            var questions = await provider.GetQuestionsAsync();
            var quiz = new InteractiveQuiz(questions);

            var defaultColor = System.Console.ForegroundColor;

            while (quiz.HasQuestion)
            {
                string prompt = "Your answer: ";

                if (!quiz.SecondTry)
                {
                    System.Console.WriteLine(quiz.CurrentQuestion.Text);
                    for (int i = 0; i < quiz.CurrentQuestion.Answers.Count; i++)
                    {
                        System.Console.WriteLine($"{i + 1} - {quiz.CurrentQuestion.Answers[i]}");
                    }
                }
                else
                {
                    prompt = "Try once more: ";
                }

                int answeredNumber = PromptUser(prompt, quiz.CurrentQuestion.Answers.Count);
                var answerCorrect = quiz.Answer(answeredNumber - 1);
                if (answerCorrect)
                {
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Correct!");
                    System.Console.ForegroundColor = defaultColor;
                }
                else
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Incorrect!");
                    System.Console.ForegroundColor = defaultColor;
                }
            }

            System.Console.WriteLine($"You've scored {quiz.Points} points!");
            for (int i = 0; i < quiz.PointsPerQuestion.Count; i++)
            {
                System.Console.WriteLine($"Question #{i + 1} - {quiz.PointsPerQuestion[i]} points");
            }
        }
    }
}