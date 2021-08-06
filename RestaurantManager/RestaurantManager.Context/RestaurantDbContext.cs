using Microsoft.EntityFrameworkCore;
using RestaurantManager.Entities.Restaurants;

namespace RestaurantManager.Context
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Restaurant>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Menu>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Dish>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Ingredient>()
                .HasKey(x => x.Id);

        }
    }
}
