using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace SupermarketWEB.Pages.Sales
{
	[Authorize]
	public class CreateModel : PageModel
	{
		private readonly SupermarketContext _context;

		public CreateModel(SupermarketContext context)
		{
			_context = context;
		}

		public IList<Product> ProductList { get; set; } 

		public async Task<IActionResult> OnGetAsync()
		{
			ProductList = await _context.Products.ToListAsync(); 
			ViewData["ProductId"] = new SelectList(ProductList, "Id", "Name");
			return Page();
		}

		[BindProperty]
		public Sale Sale { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				ProductList = await _context.Products.ToListAsync(); 
				ViewData["ProductId"] = new SelectList(ProductList, "Id", "Name");

			}

			_context.Sales.Add(Sale);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}