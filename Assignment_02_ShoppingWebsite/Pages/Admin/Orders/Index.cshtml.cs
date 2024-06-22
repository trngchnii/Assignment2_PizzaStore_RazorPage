using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment_02_ShoppingWebsite.Models;

namespace Assignment_02_ShoppingWebsite.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Assignment_02_ShoppingWebsite.Models.PizzaStoreContext _context;

        public IndexModel(Assignment_02_ShoppingWebsite.Models.PizzaStoreContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                .Include(o => o.Customer).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (StartDate == null || EndDate == null)
            {
                Order = await _context.Orders
                .Include(o => o.Customer).ToListAsync();
            }
            else
            {
                Order = await _context.Orders
                    .Where(o => o.OrderDate >= StartDate && o.OrderDate <= EndDate)
                    .ToListAsync();
            }
            return Page();
        }
    }
}
