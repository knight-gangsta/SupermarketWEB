using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly SupermarketContext _context;

		public IndexModel(SupermarketContext context)
		{
			_context = context;
		}

		public IList<Product> Products { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			Products = await _context.Products
				.Include(p => p.Category)
				.ToListAsync();
			return Page();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var product = await _context.Products.FindAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
