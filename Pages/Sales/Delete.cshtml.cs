using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Threading.Tasks;

namespace SupermarketWEB.Pages.Sales
{
	public class DeleteModel : PageModel
	{
		private readonly SupermarketContext _context;

		public DeleteModel(SupermarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Sale Sale { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Sale = await _context.Sales
				.Include(s => s.Product)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (Sale == null)
			{
				return NotFound();
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Sale = await _context.Sales.FindAsync(id);

			if (Sale != null)
			{
				_context.Sales.Remove(Sale);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
