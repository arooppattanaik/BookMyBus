using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookMyBus.Models;

namespace BookMyBus.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly BookMyBus.Models.AppDbContext _context;

        public IndexModel(BookMyBus.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; }

        public async Task OnGetAsync()
        {
            Customer = await _context.Customer
                .Include(c => c.Ticket).ToListAsync();
        }
    }
}
