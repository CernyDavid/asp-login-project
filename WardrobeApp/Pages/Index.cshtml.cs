using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WardrobeApp.Models;

namespace WardrobeApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public void OnGet()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                return;
            }
            IList<string> Roles = _userManager.GetRolesAsync(user).Result;

            foreach (string role in Roles)
            {
                Console.WriteLine(role);
            }
        }
    }
}
