using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookMyBus.Models;
using Microsoft.EntityFrameworkCore;
namespace BookMyBus.Pages
{
    public class DisplayTicketModel : PageModel
    {
        private AppDbContext _context;

        public DisplayTicketModel(AppDbContext context){
            _context = context;
            
            }

            public List<BusRoute> BusRoute { get; set; }
                    public List<Ticket> Ticket { get; set; }




        
        
        public void  OnGet()
        {

            Ticket = _context.Ticket
                                    .Include( est => est.Customer)
                                    .ToList();

            BusRoute = _context.BusRoutes
                                    .Include( est => est.Tickets)
                                    .ToList();
            
        }
    }
}