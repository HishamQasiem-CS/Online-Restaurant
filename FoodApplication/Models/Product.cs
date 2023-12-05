namespace FoodApplication.Models
{
	public class Product
	{
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Qty { get; set; }
        public string Description { get; set; }
        public bool isStock { get; set; }
        public Categorey Categorey { get; set; }
		public int CategoreyId { get; set; }
        public bool isAvavirtyCustomers { get; set; }

    }
}
