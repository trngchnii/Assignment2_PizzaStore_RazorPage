using Assignment_02_ShoppingWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment_02_ShoppingWebsite.Pages
{
    public class LoginModel : PageModel
    {
        private readonly PizzaStoreContext _context;

        public LoginModel(PizzaStoreContext context)  
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Account == null || string.IsNullOrEmpty(Account.UserName) || string.IsNullOrEmpty(Account.Password))
            {
                ModelState.AddModelError(string.Empty,"Please enter a username and password.");
                return Page();
            }

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == Account.UserName && a.Password == Account.Password);

            if (account != null)
            {
                HttpContext.Session.SetInt32("AccountType",account.Type);
                HttpContext.Session.SetString("UserName",account.UserName);
                HttpContext.Session.SetString("FullName",account.FullName);

                if (account.Type == 1)
                {
                    return RedirectToPage("Index");
                }
                else if (account.Type == 2)
                {
                    return RedirectToPage("ProfileDetail");
                }
                else
                {
                    return RedirectToPage("Unauthorized");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty,"Invalid login attempt.");
                return Page();
            }
        }
    }
}
