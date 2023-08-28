using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Quizzer.Providers.OpenTrivia.Options;
using Quizzer.Providers.OpenTrivia.ResponseModels;
using System.Text.Json;

namespace Quizzer.Providers.OpenTrivia.Tests
{
    public class OpenTriviaProviderTests
    {
        [Fact]
        public async Task GetQuestionsAsync_ShouldCallApi()
        {
            // Arrange
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(new ResponseWrapper
                {
                    ResponseCode = 0,
                    Results = new List<ResponseQuestion>
                    {
                        new ResponseQuestion
                        {
                            Question = "Question?",
                            CorrectAnswer = "Correct",
                            IncorrectAnswers = new List<string>
                            {
                                "Incorrect 1",
                                "Incorrect 2",
                                "Incorrect 3",
                            }
                        }
                    }
                }))
            };

            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            var httpClient = new HttpClient(httpMessageHandler.Object);

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            var options = new OptionsWrapper<OpenTriviaOptions>(new OpenTriviaOptions());

            var provider = new OpenTriviaProvider(options, httpClientFactory.Object);

            // Act
            var result = await provider.GetQuestionsAsync();

            // Assert
            httpClientFactory.Verify(v => v.CreateClient("OpenTriviaProvider"), Times.Exactly(1));
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
