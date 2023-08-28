using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quizzer.TriviaServer.Entities;

namespace Quizzer.TriviaServer.Pages.Questions
{
    public class DeleteModel : PageModel
    {
        private readonly Quizzer.TriviaServer.Entities.TriviaContext _context;

        public DeleteModel(Quizzer.TriviaServer.Entities.TriviaContext context)
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

            var question = await _context
                .Questions
                .Include(q => q.Category)
                .Include(q => q.Answers)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (question == null)
            {
                return NotFound();
            }
            else 
            {
                Question = question;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }
            var question = await _context.Questions.FindAsync(id);

            if (question != null)
            {
                Question = question;
                _context.Questions.Remove(Question);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
