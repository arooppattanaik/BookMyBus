using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookMyBus.Models;

namespace BookMyBus.Pages
{
    public class CreateCustomerModel : PageModel
    {

         private readonly AppDbContext _context;

         public CreateCustomerModel(AppDbContext context)
        {
            _context = context;
        }
         [BindProperty]
         public Customer Customer { get; set; }
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

           

            var customer = new Customer();
            customer.TicketId=Customer.TicketId;
            customer.Name = Customer.Name;
            customer.PhoneNumber = Customer.PhoneNumber;
            customer.DateOfBirth = Customer.DateOfBirth;
            customer.Gender=Customer.Gender;
            //customer.TicketId=BookTicketForm.Gender.Value
        
            _context.Customer.Add(customer);
            _context.SaveChanges();
            return RedirectToPage("/DisplayTicket");



        }


        
        private void PopulateSelectLists() {
            // GET ACTIVE AGENTS
            
            var bus = _context.BusRoutes
                                 .OrderBy(x => x.Id)
                                 
                                 .ToList();

            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id");
            
           
        }



        
    }
}