using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookMyBus.Models;

namespace BookMyBus.Pages.BusRoutes
{
    public class EditModel : PageModel
    {
        private readonly BookMyBus.Models.AppDbContext _context;

        public EditModel(BookMyBus.Models.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BusRoute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusRouteExists(BusRoute.Id))
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

        private bool BusRouteExists(int id)
        {
            return _context.BusRoutes.Any(e => e.Id == id);
        }
    }
}
