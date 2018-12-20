using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyBus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookMyBus.Pages
{
    public class ShowTicketModel : PageModel
    {
        private readonly AppDbContext _context;

        public ShowTicketModel(AppDbContext context)
        {
            _context = context;
        }
        public List<Ticket> Ticket { get; set; }
        public IActionResult OnGet(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            Ticket = _context.Ticket
                                    .Where(s => s.BusRouteId == id)
                                    .ToList();

            if(Ticket==null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
    }
}