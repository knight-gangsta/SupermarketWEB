using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarketWEB.Pages.Products
{
	[Authorize]
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
			
			Console.WriteLine(Product);
			if (!ModelState.IsValid || Product == null || _context.Products == null)
			{
				
				CategoryList = await _context.Categories.ToListAsync();
				
			}

			
			_context.Products.Add(Product);
			await _context.SaveChangesAsync();

			
			return RedirectToPage("./Index");
		}
	}
}
