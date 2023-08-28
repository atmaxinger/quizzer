using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizzer.Providers.OpenTrivia.ResponseModels;
using Quizzer.TriviaServer.Entities;

namespace Quizzer.TriviaServer.Controllers
{
    [Route("/api.php")]
    public class TriviaApiController : ControllerBase
    {
        private readonly TriviaContext _context;

        private Dictionary<string, Question.OpenTriviaDifficulty> _difficultyMapper = new Dictionary<string, Question.OpenTriviaDifficulty>();
        private Dictionary<Question.OpenTriviaDifficulty, string> _reverseDifficultyMapper = new Dictionary<Question.OpenTriviaDifficulty, string>();

        private Dictionary<string, Question.OpenTriviaQuestionType> _typeMapper = new Dictionary<string, Question.OpenTriviaQuestionType>();
        private Dictionary<Question.OpenTriviaQuestionType, string> _reverseTypeMapper = new Dictionary<Question.OpenTriviaQuestionType, string>();

        private void InitDifficultyMapper(string str, Question.OpenTriviaDifficulty difficulty)
        {
            _difficultyMapper[str] = difficulty;
            _reverseDifficultyMapper[difficulty] = str;
        }

        private void InitTypeMapper(string str, Question.OpenTriviaQuestionType type)
        {
            _typeMapper[str] = type;
            _reverseTypeMapper[type] = str;
        }

        public TriviaApiController(TriviaContext context)
        {
            InitDifficultyMapper("easy", Question.OpenTriviaDifficulty.Easy);
            InitDifficultyMapper("medium", Question.OpenTriviaDifficulty.Medium);
            InitDifficultyMapper("hard", Question.OpenTriviaDifficulty.Hard);

            InitTypeMapper("boolean", Question.OpenTriviaQuestionType.TrueFalse);
            InitTypeMapper("multiple", Question.OpenTriviaQuestionType.MultipleChoice);

            _context = context;
        }

        private IActionResult TdbInvalidParameter()
        {
            // https://opentdb.com/api_config.php 
            // returns OK even on non-ok results, but sets response code accordingly
            return Ok(new ResponseWrapper
            {
                ResponseCode = 2,
                Results = new List<ResponseQuestion>()
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] int amount, [FromQuery] int? category, [FromQuery] string? difficulty, [FromQuery] string? type)
        {
            var query = _context
                .Questions
                .Include(q => q.Answers.OrderBy(a => a.Order))
                .Include(q => q.Category)
                .AsNoTracking()
                .Take(amount);

            if (category.HasValue)
            {
                query = query.Where(q => q.CategoryId == category.Value);
            }

            if (difficulty != null)
            {
                var harmonizedDifficulty = difficulty.Trim().ToLower();

                if(!_difficultyMapper.ContainsKey(harmonizedDifficulty))
                {
                    return TdbInvalidParameter();
                }

                query = query.Where(q => q.Difficulty == _difficultyMapper[harmonizedDifficulty]);
            }

            if (type != null)
            {
                var harmonizedType = type.Trim().ToLower();

                if (!_typeMapper.ContainsKey(harmonizedType))
                {
                    return TdbInvalidParameter();
                }

                query = query.Where(q => q.QuestionType == _typeMapper[harmonizedType]);
            }

            var questions = await query.ToListAsync();
            if (!questions.Any())
            {
                return Ok(new ResponseWrapper
                {
                    ResponseCode = 1,
                    Results = new List<ResponseQuestion>()
                });
            }

            var response = new ResponseWrapper
            {
                ResponseCode = 0,
                Results = new List<ResponseQuestion>()
            };

            foreach (var question in questions)
            {
                var responseQuestion = new ResponseQuestion
                {
                    Category = question.Category.Name,
                    Difficulty = _reverseDifficultyMapper[question.Difficulty],
                    Type = _reverseTypeMapper[question.QuestionType],
                    Question = question.Text,
                    CorrectAnswer = question.Answers.First(a => a.IsCorrect).Text,
                    IncorrectAnswers = question.Answers.Where(a => !a.IsCorrect).Select(a => a.Text).ToList()
                };

                response.Results.Add(responseQuestion);
            }

            return Ok(response);
        }
    }
}
