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
    public class DetailsModel : PageModel
    {
        private readonly Quizzer.TriviaServer.Entities.TriviaContext _context;

        public DetailsModel(Quizzer.TriviaServer.Entities.TriviaContext context)
        {
            _context = context;
        }

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
                .Include(q => q.Answers.OrderBy(a => a.Order))
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
    }
}
