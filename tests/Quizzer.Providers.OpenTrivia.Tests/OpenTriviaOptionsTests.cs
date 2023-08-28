using Quizzer.Providers.OpenTrivia.Options;
namespace Quizzer.Providers.OpenTrivia.Tests
{
    public class OpenTriviaOptionsTests
    {
        [Theory]
        [InlineData(12, OpenTriviaOptions.OpenTriviaQuestionType.Any, OpenTriviaOptions.OpenTriviaDifficulty.Any, null, "amount=12")]
        [InlineData(13, OpenTriviaOptions.OpenTriviaQuestionType.MultipleChoice, OpenTriviaOptions.OpenTriviaDifficulty.Any, null, "amount=13&type=multiple")]
        [InlineData(13, OpenTriviaOptions.OpenTriviaQuestionType.TrueFalse, OpenTriviaOptions.OpenTriviaDifficulty.Any, null, "amount=13&type=boolean")]
        [InlineData(13, OpenTriviaOptions.OpenTriviaQuestionType.Any, OpenTriviaOptions.OpenTriviaDifficulty.Easy, null, "amount=13&difficulty=easy")]
        [InlineData(13, OpenTriviaOptions.OpenTriviaQuestionType.Any, OpenTriviaOptions.OpenTriviaDifficulty.Medium, null, "amount=13&difficulty=medium")]
        [InlineData(13, OpenTriviaOptions.OpenTriviaQuestionType.Any, OpenTriviaOptions.OpenTriviaDifficulty.Hard, null, "amount=13&difficulty=hard")]
        [InlineData(13, OpenTriviaOptions.OpenTriviaQuestionType.Any, OpenTriviaOptions.OpenTriviaDifficulty.Any, 42, "amount=13&category=42")]
        [InlineData(13, OpenTriviaOptions.OpenTriviaQuestionType.MultipleChoice, OpenTriviaOptions.OpenTriviaDifficulty.Medium, 49, "amount=13&type=multiple&difficulty=medium&category=49")]

        public void GetUrlParameters_ShouldReturnCorrentParameters(int amount, OpenTriviaOptions.OpenTriviaQuestionType type, OpenTriviaOptions.OpenTriviaDifficulty difficulty, int? categoryId, string expected)
        {
            // Arrange
            var options = new OpenTriviaOptions
            {
                QuestionAmount = amount,
                QuestionCategoryId = categoryId,
                QuestionDifficulty = difficulty,
                QuestionType = type,
            };

            // Act
            var urlParameters = options.GetUrlParameters();

            // Assert
            Assert.Equal(expected, urlParameters);
        }
    }
}
