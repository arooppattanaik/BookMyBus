using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BookMyBus.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookMyBus.Pages
{
    public class BookTicketModel : PageModel
    {
        
        private readonly AppDbContext _context;
        public BookTicketModel(AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        
        public Ticket Ticket {get;set;}
        public void OnGet()
        {

            PopulateSelectLists();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) {
                PopulateSelectLists();
                return Page();
            }

            var ticket = new Ticket();
            ticket.SeatNumber = Ticket.SeatNumber;
            ticket.Price = Ticket.Price;
            ticket.DateOfBooking = Ticket.DateOfBooking;
            ticket.DateOfJounery = Ticket.DateOfJounery;
            ticket.BusRouteId = Ticket.BusRouteId;


           
            //customer.TicketId=BookTicketForm.Gender.Value
            _context.Ticket.Add(ticket);
            _context.SaveChanges();


            return RedirectToPage("./CreateCustomer");

        }


        
        private void PopulateSelectLists() {
            // GET ACTIVE AGENTS

            var bus = _context.BusRoutes
                                 .OrderBy(x => x.Id)
                                 
                                 .ToList();
            ViewData["BusRouteId"] = new SelectList(_context.BusRoutes, "Id", "Id");

            
            
           
        }



        
    }
}