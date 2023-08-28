using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quizzer.TriviaServer.Entities;

namespace Quizzer.TriviaServer.Pages.Questions
{
    public class CreateModel : PageModel
    {
        private readonly Quizzer.TriviaServer.Entities.TriviaContext _context;

        public CreateModel(Quizzer.TriviaServer.Entities.TriviaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Question Question { get; set; } = new Question { Answers = new List<Answer>() };

        public async Task<IActionResult> OnPostRemoveAnswerAsync(int answerIndex)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            if (answerIndex >= 0 && Question.Answers.Count > answerIndex)
            {
                Question.Answers.RemoveAt(answerIndex);

                for (int i = 0; i < Question.Answers.Count; i++)
                {
                    Question.Answers[i].Order = i;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAnswerAsync(int answerIndex)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");

            if(Question.Answers == null)
            {
                Question.Answers = new List<Answer>();
            }

            Question.Answers.Add(new Answer
            {
                Text = "",
                Order = Question.Answers.Count,
                QuestionId = Question.Id,
                Question = Question
            });

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid || _context.Questions == null || Question == null)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
                return Page();
            }

            _context.Questions.Add(Question);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
