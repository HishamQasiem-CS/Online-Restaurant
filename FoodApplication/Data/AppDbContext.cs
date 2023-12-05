using FoodApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Data
{
	public class AppDbContext: IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
		
		}
		public DbSet<Categorey>categoreys { get; set; }
		public DbSet<Product>products { get; set; }
        public DbSet<Book_Table> book_Tables { get; set; }
        public DbSet<Contact> contacts { get; set; }
		public DbSet<CartViewModel> cart { get; set; }	
    }
}
