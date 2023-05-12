using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarketWEB.Pages.Products
{
	public class CreateModel : PageModel
	{
		private readonly SupermarketContext _context;

		public CreateModel(SupermarketContext context)
		{
			_context = context;
		}

		public List<Category> CategoryList { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			CategoryList = await _context.Categories.ToListAsync();
			return Page();
		}

		[BindProperty]
		public Product Product { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || Product == null || _context.Products == null)
			{
				CategoryList = await _context.Categories.ToListAsync();
				return Page();
			}

			// Convertir el valor de Price a decimal
			if (decimal.TryParse(Product.Price, out decimal price))
			{
				Product.Price = price;
			}
			else
			{
				ModelState.AddModelError("Product.Price", "El precio no es válido.");
				CategoryList = await _context.Categories.ToListAsync();
				return Page();
			}

			_context.Products.Add(Product);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
