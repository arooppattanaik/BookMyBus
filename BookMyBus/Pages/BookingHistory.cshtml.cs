using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookMyBus.Pages
{
    public class BookingHistoryModel : PageModel
    {
         private BookMyBus.Models.AppDbContext _context;
          public BookingHistoryModel(BookMyBus.Models.AppDbContext context) {
            _context = context;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public string Search { get; set; }
        public bool SearchCompleted { get; set; }
         public ICollection<BookMyBus.Models.Ticket> SearchResults { get; set; }

        public void OnPost() {
            // PERFORM SEARCH
            if (string.IsNullOrWhiteSpace(Search)) {
                // EXIT EARLY IF THERE IS NO SEARCH TERM PROVIDED
                return;
            }
          //  SearchResults = _context.BusRoutes.Where(x => x.BusNumber == int.Parse(Search)).ToList();
          //  if(SearchResults == null || SearchResults.Count<0){
           SearchResults = _context.Ticket.Include(x => x.Customer).Include(y => y.BusRoute).Where(z => z.Customer.Name.ToLower().Contains(Search.ToLower())).ToList();
            //}
            SearchCompleted = true;
        }
    }
}