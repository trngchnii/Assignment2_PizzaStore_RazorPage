using Assignment_02_ShoppingWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment_02_ShoppingWebsite.Pages.Admin.Orders
{
    public class OrderDetailModel : PageModel
    {
        private readonly PizzaStoreContext context;

        public OrderDetailModel(PizzaStoreContext context)
        {
            this.context = context;
        }
        public List<OrderDetail> OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || context.OrderDetails == null)
            {
                return NotFound();
            }

            var order = await context.OrderDetails.Where(m => m.OrderId == id).ToListAsync();
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                OrderDetail = order;
            }
            return Page();
        }
    }
}
