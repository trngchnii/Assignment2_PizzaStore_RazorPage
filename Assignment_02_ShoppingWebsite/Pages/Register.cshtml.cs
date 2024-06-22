using Assignment_02_ShoppingWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment_02_ShoppingWebsite.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly PizzaStoreContext context;

        public RegisterModel(PizzaStoreContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingUser = context.Accounts.FirstOrDefault(a => a.UserName == Account.UserName);
            if (existingUser != null)
            {
                TempData["ErrorMessage"] = "Username is already taken.";
                //ModelState.AddModelError(string.Empty,"Username is already taken.");
                return Page();
            }

            Account.Type = 2;
            context.Accounts.Add(Account);
            await context.SaveChangesAsync();

            // Sử dụng TempData để lưu thông báo
            TempData["SuccessMessage"] = "Registration successful!";

            return Page();
        }
    }
}
