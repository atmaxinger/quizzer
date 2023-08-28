using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quizzer.TriviaServer.Entities;

namespace Quizzer.TriviaServer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TriviaContext _context;

        public List<Category> Categories { get; set; }

        public IndexModel(TriviaContext context, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Categories = await _context
                .Categories
                .AsNoTracking()
                .ToListAsync();
        }
    }
}