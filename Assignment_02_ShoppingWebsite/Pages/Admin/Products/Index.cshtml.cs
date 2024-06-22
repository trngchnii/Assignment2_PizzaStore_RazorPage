using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment_02_ShoppingWebsite.Models;

namespace Assignment_02_ShoppingWebsite.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly Assignment_02_ShoppingWebsite.Models.PizzaStoreContext _context;

        public IndexModel(Assignment_02_ShoppingWebsite.Models.PizzaStoreContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        [BindProperty]
        public string SearchProductId { get; set; }

        [BindProperty]
        public string SearchProductName { get; set; }

        [BindProperty]
        public string SearchUnitPrice { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            IQueryable<Product> query = _context.Products;
            if (!string.IsNullOrEmpty(SearchProductId) && int.TryParse(SearchProductId,out var productId))
            {
                query = query.Where(p => p.ProductId == productId);
            }
            if (!string.IsNullOrEmpty(SearchProductName))
            {
                query = query.Where(p=>p.ProductName.Contains(SearchProductName));
            }
            if (!string.IsNullOrEmpty(SearchUnitPrice) && decimal.TryParse(SearchUnitPrice,out var unitPrice))
            {
                query = query.Where(p=>p.UnitPrice == unitPrice);
            }
            Product = await query.Include(p => p.Category).Include(p=>p.Supplier).ToListAsync();
            return Page();
        }
    }
}
