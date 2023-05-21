using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Sales
{
	[Authorize]
	public class EditModel : PageModel
	{
		private readonly SupermarketContext _context;

		public EditModel(SupermarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Sale Sale { get; set; }

		public SelectList ProductList { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Sale = await _context.Sales.FirstOrDefaultAsync(m => m.Id == id);

			if (Sale == null)
			{
				return NotFound();
			}

			ProductList = new SelectList(await _context.Products.ToListAsync(), "Id", "Name");

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				//return Page();
			}

			_context.Attach(Sale).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!SaleExists(Sale.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool SaleExists(int id)
		{
			return _context.Sales.Any(e => e.Id == id);
		}
	}
}
