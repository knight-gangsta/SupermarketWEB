using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModes
{
	public class DeleteModel : PageModel
	{
		private readonly SupermarketContext _context;
		public DeleteModel(SupermarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public PayMode PayMode { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			PayMode = await _context.PayModes.FirstOrDefaultAsync(m => m.Id == id);

			if (PayMode == null)
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

			PayMode = await _context.PayModes.FindAsync(id);

			if (PayMode != null)
			{
				_context.PayModes.Remove(PayMode);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}

}
