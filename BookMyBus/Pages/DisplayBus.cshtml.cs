using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyBus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookMyBus.Pages
{
    public class DisplayBusModel : PageModel
    {
      private AppDbContext _context;

        public DisplayBusModel(AppDbContext context){
            _context = context;
            
            }

            public List<BusRoute> BusRoute { get; set; }


            public void  OnGet()
        {


            BusRoute = _context.BusRoutes
                                    .ToList();
            
        }
    }
}