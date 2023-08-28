using Microsoft.Extensions.Options;
using Quizzer.Core;
using Quizzer.Providers.OpenTrivia.Options;
using Quizzer.Providers.OpenTrivia.ResponseModels;
using System.Net.Http.Json;

namespace Quizzer.Providers.OpenTrivia
{
    public class OpenTriviaProvider : IQuestionsProvider
    {
        private readonly HttpClient _httpClient;
        private readonly OpenTriviaOptions _options;
        private bool _disposed = false;
        
        public OpenTriviaProvider (IOptions<OpenTriviaOptions> options, IHttpClientFactory httpClientFactory)
        {
            _options = options.Value;
            _httpClient = httpClientFactory.CreateClient("OpenTriviaProvider");
        }

        public async Task<QuestionCollection> GetQuestionsAsync(CancellationToken cancellationToken = default)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            cancellationToken.ThrowIfCancellationRequested();

            ResponseWrapper? result;

            try
            {
                result = await _httpClient
                    .GetFromJsonAsync<ResponseWrapper>($"{_options.ApiUrl}/api.php?{_options.GetUrlParameters()}", cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new QuestionFetchingException("An error occured while fetching questions from OpenTriviaDB", ex);
            }

            if (result is null)
            {
                throw new QuestionFetchingException("Did not receive a valid response from OpenTriviaDB");
            }

            return new QuestionCollection(result
                .Results
                .Select(q => q.ToCoreQuestion()));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _httpClient.Dispose();
            }

            _disposed = true;
        }
    }
}