using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace Client.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public IndexModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

        public IList<BookStore.Models.Books> Books { get;set; }

        public async Task OnGetAsync()
        {
            Books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category).ToListAsync();
        }
    }
}
