namespace Quizzer.Core
{
    public class InteractiveQuiz
    {
        private QuestionCollection Questions { get; set; }
        private int _currentQuestionIndex = 0;
        private bool _shuffleAnswers;

        public double Points { get; private set; } = 0;
        public List<double> PointsPerQuestion { get; private set; } = new List<double>();
        public int NumberOfQuestions => Questions.Count;
        public int CurrentQuestionNumber => _currentQuestionIndex + 1;
        public Question CurrentQuestion { get; private set; }
        public bool SecondTry { get; private set; } = false;
        public bool HasQuestion { get; private set; } = false;
        
        public InteractiveQuiz (QuestionCollection questions, bool shuffleAnswers = true, bool shuffleQuestions = false) 
        { 
            if (shuffleQuestions)
            {
                questions.ShuffleQuestions();
            }

            Questions = questions;
            CurrentQuestion = questions[0];
            HasQuestion = true;
            _currentQuestionIndex = 0;

            if (shuffleAnswers)
            {
                CurrentQuestion.ShuffleAnswers();
            }
            _shuffleAnswers = shuffleAnswers;
        }

        public bool Answer (int answer)
        {
            bool success = false;
            bool nextQuestion = false;

            if (answer == CurrentQuestion.CorrectAnswerIndex)
            {
                success = true;
                nextQuestion = true;
                double pointsForQuestion = 1;
                if (SecondTry)
                {
                    pointsForQuestion = 0.5;
                }

                Points += pointsForQuestion;
                PointsPerQuestion.Add(pointsForQuestion);
            }
            else
            {
                if (SecondTry)
                {
                    nextQuestion = true;
                }
                else
                {               
                    SecondTry = true;
                }
            }

            if (nextQuestion)
            {
                SecondTry = false;
                _currentQuestionIndex++;
                HasQuestion = _currentQuestionIndex < Questions.Count;

                if (HasQuestion)
                {
                    CurrentQuestion = Questions[_currentQuestionIndex];
                    CurrentQuestion.ShuffleAnswers();
                }
                else
                {
                    _currentQuestionIndex--;
                }
            }

            return success;
        }
    }
}
