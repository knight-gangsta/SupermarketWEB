using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Providers
{
	public class IndexModel : PageModel
	{
		private readonly SupermarketContext _context;

		public IndexModel(SupermarketContext context)
		{
			_context = context;
		}

		public IList<Provider> Providers { get; set; }

		public async Task OnGetAsync()
		{
			Providers = await _context.Providers.ToListAsync();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var provider = await _context.Providers.FindAsync(id);

			if (provider == null)
			{
				return NotFound();
			}

			_context.Providers.Remove(provider);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
