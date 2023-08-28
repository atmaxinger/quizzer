using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quizzer.TriviaServer.Entities;

namespace Quizzer.TriviaServer.Pages.Questions
{
    public class EditModel : PageModel
    {
        private readonly Quizzer.TriviaServer.Entities.TriviaContext _context;

        public EditModel(Quizzer.TriviaServer.Entities.TriviaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question Question { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var question =  await _context
                .Questions
                .Include(q => q.Category)
                .Include(q => q.Answers.OrderBy(a => a.Order))
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (question == null)
            {
                return NotFound();
            }
            Question = question;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAnswerAsync(int answerIndex)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            if(answerIndex >= 0 && Question.Answers.Count > answerIndex)
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

            if (Question.Answers == null)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
                return Page();
            }

            var oldAnswers = await _context
                .Answers
                .Where(a => a.QuestionId == Question.Id)
                .AsNoTracking()
                .ToListAsync();

            _context.Attach(Question).State = EntityState.Modified;

            foreach (var answer in Question.Answers)
            {
                var oldAnswer = oldAnswers.FirstOrDefault(a => a.Order == answer.Order);

                if(oldAnswer == null)
                {
                    _context.Attach(answer).State = EntityState.Added;
                }
                else
                {
                    _context.Attach(answer).State = EntityState.Modified;
                    oldAnswers.Remove(oldAnswer);
                }
            }

            foreach(var oldAnswer in oldAnswers)
            {
                _context.Remove(oldAnswer);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(Question.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool QuestionExists(int id)
        {
          return (_context.Questions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
