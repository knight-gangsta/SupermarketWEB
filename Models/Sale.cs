using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketWEB.Models
{
	public class Sale
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public decimal Total { get; set; }
		public string CustomerName { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
