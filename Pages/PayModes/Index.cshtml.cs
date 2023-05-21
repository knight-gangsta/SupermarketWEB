using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModes
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly SupermarketContext _context;

		public IndexModel(SupermarketContext context)
		{
			_context = context;
		}

		public IList<PayMode> PayModes { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			PayModes = await _context.PayModes.ToListAsync();
			return Page();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var payMode = await _context.PayModes.FindAsync(id);

			if (payMode == null)
			{
				return NotFound();
			}

			_context.PayModes.Remove(payMode);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}

