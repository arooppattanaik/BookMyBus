using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookMyBus.Pages
{
    public class IndexModel : PageModel
    {
        private BookMyBus.Models.AppDbContext _context;
public int TicketsCount { get; set; }
      public int BusCount { get; set; }
      public int CustomerCount { get; set; }
        public int CitiesCount { get; set; }
        public IndexModel(BookMyBus.Models.AppDbContext context) {
            _context = context;
        }
        public void OnGet()
        {
 SearchCompleted = false;
 TicketsCount = _context.Ticket.Count();  
  BusCount = _context.BusRoutes.Count();
CustomerCount = _context.Customer.Count();
CitiesCount = _context.BusRoutes.Where(x => x.Destination != null).Distinct().Count();  
        }


[BindProperty]
        public string Search { get; set; }
        public bool SearchCompleted { get; set; }
         public ICollection<BookMyBus.Models.BusRoute> SearchResults { get; set; }


 public void OnPost() {
            // PERFORM SEARCH
            if (string.IsNullOrWhiteSpace(Search)) {
                // EXIT EARLY IF THERE IS NO SEARCH TERM PROVIDED
                return;
            }
          //  SearchResults = _context.BusRoutes.Where(x => x.BusNumber == int.Parse(Search)).ToList();
          //  if(SearchResults == null || SearchResults.Count<0){
            SearchResults = _context.BusRoutes.Where(x => x.Source.ToLower().Contains(Search.ToLower())).ToList();
            //}
            SearchCompleted = true;
        }
    }
}
