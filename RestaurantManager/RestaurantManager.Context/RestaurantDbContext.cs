using Microsoft.EntityFrameworkCore;
using RestaurantManager.Entities.Order;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.SqlContext.Configuration.Orders;
using RestaurantManager.SqlContext.Configuration.Restaurants;

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
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new IngredientConfiguration());
            modelBuilder.ApplyConfiguration(new DishConfiguration());

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());

            modelBuilder
                .Entity<ShippingMethod>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<PaymentType>()
                .HasKey(x => x.Id);
        }
    }
}
