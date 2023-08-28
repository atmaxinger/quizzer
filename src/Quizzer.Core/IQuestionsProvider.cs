namespace Quizzer.Core
{
    public interface IQuestionsProvider : IDisposable
    {
        Task<QuestionCollection> GetQuestionsAsync(CancellationToken cancellationToken = default);
    }
}
