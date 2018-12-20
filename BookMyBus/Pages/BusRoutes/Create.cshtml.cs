using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookMyBus.Models;

namespace BookMyBus.Pages.BusRoutes
{
    public class CreateModel : PageModel
    {
        private readonly BookMyBus.Models.AppDbContext _context;

        public CreateModel(BookMyBus.Models.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BusRoute BusRoute { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BusRoutes.Add(BusRoute);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}