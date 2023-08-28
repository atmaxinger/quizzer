using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quizzer.TriviaServer.Entities;

namespace Quizzer.TriviaServer.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Quizzer.TriviaServer.Entities.TriviaContext _context;

        public IndexModel(Quizzer.TriviaServer.Entities.TriviaContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Categories != null)
            {
                Category = await _context.Categories.ToListAsync();
            }
        }
    }
}
