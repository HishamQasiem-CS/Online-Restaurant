namespace FoodApplication.Models
{
	public class Categorey
	{
        public int CategoreyId { get; set; }
        public string CategoreyName { get; set; }
        public List<Product> Products { get; set; }
    }
}
