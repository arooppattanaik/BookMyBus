using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookMyBus.Models;

namespace BookMyBus.Pages.BusRoutes
{
    public class DetailsModel : PageModel
    {
        private readonly BookMyBus.Models.AppDbContext _context;

        public DetailsModel(BookMyBus.Models.AppDbContext context)
        {
            _context = context;
        }

        public BusRoute BusRoute { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BusRoute = await _context.BusRoutes.FirstOrDefaultAsync(m => m.Id == id);

            if (BusRoute == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
