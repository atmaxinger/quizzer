namespace Quizzer.Core.Tests
{
    public class InteractiveQuizTests
    {
        private QuestionCollection GetDemoQuestionCollection ()
        {
            return new QuestionCollection(new List<Question>
            {
                new Question
                {
                    Text = "What does AD stand for in relation to Windows Operating Systems?",
                    Answers = new List<string>
                    {
                        "Alternative Drive",
                        "Arrested Development",
                        "Active Directory",
                        "Alternate Dimension"
                    },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "A scalene triangle has two sides of equal length.",
                    Answers = new List<string>
                    {
                        "False",
                        "True"
                    },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "Which of the following Japanese islands is the biggest?",
                    Answers = new List<string>
                    {
                        "Hokkaido",
                        "Honshu",
                        "Shikoku",
                        "Kyushu"
                    },
                    CorrectAnswerIndex = 1
                }
            });
        }

        [Fact]
        public void Constructor_ShouldInitPublicFieldsAccordingly()
        {
            // Arrange & Act
            var quiz = new InteractiveQuiz(GetDemoQuestionCollection(), shuffleAnswers: false);

            // Assert
            Assert.True(quiz.HasQuestion);
            Assert.False(quiz.SecondTry);
            Assert.Equal("What does AD stand for in relation to Windows Operating Systems?", quiz.CurrentQuestion.Text);
            Assert.Equal(1, quiz.CurrentQuestionNumber);
            Assert.Equal(3, quiz.NumberOfQuestions);
            Assert.Equal(0, quiz.Points);
            Assert.Empty(quiz.PointsPerQuestion);
        }

        [Fact]
        public void Answer_CorrectAnswer_ShouldGoToNextQuestion()
        {
            // Arrange
            var quiz = new InteractiveQuiz(GetDemoQuestionCollection(), shuffleAnswers: false);

            // Act
            var result = quiz.Answer(2);

            // Assert
            Assert.True(result);
            Assert.False(quiz.SecondTry);
            Assert.Equal(2, quiz.CurrentQuestionNumber);
            Assert.Equal(1, quiz.Points);
            Assert.True(quiz.HasQuestion);
            Assert.Equal("A scalene triangle has two sides of equal length.", quiz.CurrentQuestion.Text);
            Assert.Single(quiz.PointsPerQuestion);
            Assert.Equal(1, quiz.PointsPerQuestion[0]);
        }

        [Fact]
        public void Answer_SecondTimeCorrectly_ShouldGiveHalfPoint()
        {
            // Arrange
            var quiz = new InteractiveQuiz(GetDemoQuestionCollection(), shuffleAnswers: false);

            // Act & Assert
            var result = quiz.Answer(1);
            Assert.False(result);
            Assert.True(quiz.SecondTry);
            Assert.Equal(1, quiz.CurrentQuestionNumber);
            Assert.Equal(0, quiz.Points);
            Assert.True(quiz.HasQuestion);
            Assert.Equal("What does AD stand for in relation to Windows Operating Systems?", quiz.CurrentQuestion.Text);
            Assert.Empty(quiz.PointsPerQuestion);

            result = quiz.Answer(2);
            Assert.True(result);
            Assert.False(quiz.SecondTry);
            Assert.Equal(2, quiz.CurrentQuestionNumber);
            Assert.Equal(0.5, quiz.Points);
            Assert.True(quiz.HasQuestion);
            Assert.Equal("A scalene triangle has two sides of equal length.", quiz.CurrentQuestion.Text);
            Assert.Single(quiz.PointsPerQuestion);
            Assert.Equal(0.5, quiz.PointsPerQuestion[0]);
        }

        [Fact]
        public void Answer_SecondTimeIncorrectly_ShouldGiveNoPointsAndJumpToNextQuestion()
        {
            // Arrange
            var quiz = new InteractiveQuiz(GetDemoQuestionCollection(), shuffleAnswers: false);

            // Act & Assert
            var result = quiz.Answer(1);
            Assert.False(result);
            Assert.True(quiz.SecondTry);
            Assert.Equal(1, quiz.CurrentQuestionNumber);
            Assert.Equal(0, quiz.Points);
            Assert.True(quiz.HasQuestion);
            Assert.Equal("What does AD stand for in relation to Windows Operating Systems?", quiz.CurrentQuestion.Text);
            Assert.Empty(quiz.PointsPerQuestion);

            result = quiz.Answer(3);
            Assert.False(result);
            Assert.False(quiz.SecondTry);
            Assert.Equal(2, quiz.CurrentQuestionNumber);
            Assert.Equal(0, quiz.Points);
            Assert.True(quiz.HasQuestion);
            Assert.Equal("A scalene triangle has two sides of equal length.", quiz.CurrentQuestion.Text);
            Assert.Empty(quiz.PointsPerQuestion);
        }
    }
}