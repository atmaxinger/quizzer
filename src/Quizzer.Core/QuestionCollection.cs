using System.Collections;

namespace Quizzer.Core
{
    public class QuestionCollection : IReadOnlyList<Question>
    {
        private List<Question> _questions;
        public QuestionCollection(IEnumerable<Question> questions)
        { 
            _questions = questions.ToList();
        }

        public int Count => _questions.Count;

        public Question this[int index] => _questions[index];

        public IEnumerator<Question> GetEnumerator()
        {
            return _questions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _questions.GetEnumerator();
        }

        public void ShuffleQuestions(Random? random = null)
        {
            if (random is null)
            {
                random = Random.Shared;
            }

            _questions = _questions
                .OrderBy(q => random.Next())
                .ToList();
        }
    }
}
