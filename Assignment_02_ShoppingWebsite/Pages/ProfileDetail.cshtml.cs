using Assignment_02_ShoppingWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment_02_ShoppingWebsite.Pages
{
    public class ProfileDetailModel : PageModel
    {
        private readonly PizzaStoreContext context;

        public ProfileDetailModel(PizzaStoreContext context)
        {
            this.context = context;
        }
        [BindProperty]
        public Account Account { get; set; } = default!;

        public IList<Order> Order { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync()
        {
            var userName = GetCurrentUserName();
            var account = await context.Accounts.FirstOrDefaultAsync(m => m.UserName == userName);
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
            Order = await context.Orders
                .Where(o => o.CustomerId == account.AccountId).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //ktra du lieu nhap vao
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userInDb = context.Accounts.FirstOrDefault(a => a.UserName == GetCurrentUserName());
            if (userInDb == null)
            {
                return NotFound();
            }
            

            userInDb.FullName = Account.FullName;
            userInDb.Password = Account.Password;

            context.Update(userInDb);
            context.SaveChanges();
            TempData["SuccessMessage"] = "Update Profile successful!";

            return Page();
        }

        private string GetCurrentUserName()
        {
            return HttpContext.Session.GetString("UserName");
        }
    }
}
