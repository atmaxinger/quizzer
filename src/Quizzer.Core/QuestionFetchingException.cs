using System.Runtime.Serialization;

namespace Quizzer.Core
{
    public class QuestionFetchingException : ApplicationException
    {
        public QuestionFetchingException()
        {
        }

        public QuestionFetchingException(string? message) : base(message)
        {
        }

        public QuestionFetchingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected QuestionFetchingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
