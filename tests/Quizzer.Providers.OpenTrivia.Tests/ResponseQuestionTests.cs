using Quizzer.Providers.OpenTrivia.ResponseModels;

namespace Quizzer.Providers.OpenTrivia.Tests
{
    public class ResponseQuestionTests
    {
        [Fact]
        public void ToCoreQuestion_ShouldConvertSuccessfully()
        {
            // Arrange
            var responseQuestion = new ResponseQuestion
            {
                Question = "What does AD stand for in relation to Windows Operating Systems?",
                CorrectAnswer = "Active Directory",
                IncorrectAnswers = new List<string>
                {
                    "Alternative Drive",
                    "Automated Database",
                    "Active Department"
                }
            };

            // Act
            var coreQuestion = responseQuestion.ToCoreQuestion();

            // Assert
            Assert.Equal("What does AD stand for in relation to Windows Operating Systems?", coreQuestion.Text);
            Assert.Equal(4, coreQuestion.Answers.Count);
            Assert.Equal(3, coreQuestion.CorrectAnswerIndex);
            Assert.Equal("Alternative Drive", coreQuestion.Answers[0]);
            Assert.Equal("Automated Database", coreQuestion.Answers[1]);
            Assert.Equal("Active Department", coreQuestion.Answers[2]);
            Assert.Equal("Active Directory", coreQuestion.Answers[3]);
        }
    }
}