using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Sales
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly SupermarketContext _context;

		public IndexModel(SupermarketContext context)
		{
			_context = context;
		}

		public IList<Sale> sales { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			sales = await _context.Sales
				.Include(s => s.Product)
				.ToListAsync();

			return Page();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var sale = await _context.Sales.FindAsync(id);

			if (sale == null)
			{
				return NotFound();
			}

			_context.Sales.Remove(sale);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}