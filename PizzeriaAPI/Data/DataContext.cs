using Microsoft.EntityFrameworkCore;
using PizzeriaAPI.Models;


namespace PizzeriaAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Pizzeria> Pizzerias { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<OrderItemPizza> OrderItemPizzas { get; set; }
        public DbSet<OrderItemTopping> OrderItemToppings { get; set; }

        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.OrderId);  

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascade delete here

            // OrderItem to Pizza many-to-many relationship
            modelBuilder.Entity<OrderItemPizza>()
                .HasKey(op => new { op.OrderItemId, op.PizzaId });

            modelBuilder.Entity<OrderItemPizza>()
                .HasOne(op => op.OrderItem)
                .WithMany(oi => oi.OrderItemPizzas)
                .HasForeignKey(op => op.OrderItemId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete here

            modelBuilder.Entity<OrderItemPizza>()
                .HasOne(op => op.Pizza)
                .WithMany(p => p.OrderItemPizzas)
                .HasForeignKey(op => op.PizzaId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete here

            // OrderItem to Topping many-to-many relationship
            modelBuilder.Entity<OrderItemTopping>()
                .HasKey(ot => new { ot.OrderItemId, ot.ToppingId });

            modelBuilder.Entity<OrderItemTopping>()
                .HasOne(ot => ot.OrderItem)
                .WithMany(oi => oi.OrderItemToppings)
                .HasForeignKey(ot => ot.OrderItemId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete here

            modelBuilder.Entity<OrderItemTopping>()
                .HasOne(ot => ot.Topping)
                .WithMany(t => t.OrderItemToppings)
                .HasForeignKey(ot => ot.ToppingId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete here

            if (_configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                modelBuilder.Entity<Pizzeria>().HasData(new Pizzeria
                {
                    Id = 1,
                    Name = "Preston Pizzeria",
                    Location = "Preston",
                    IsActive = true
                });

                modelBuilder.Entity<Pizzeria>().HasData(new Pizzeria
                {
                    Id = 2,
                    Name = "Southbank Pizzeria",
                    Location = "Southbank",
                    IsActive = true
                });



                modelBuilder.Entity<Pizza>().HasData(new Pizza
                {
                    Id = 1,
                    Name = "Capricciosa",
                    Description = "Cheese, Ham, Mushrooms, Olives",
                    BasePrice = 20,
                    PizzeriaId = 1,
                    IsActive = true
                });

                modelBuilder.Entity<Pizza>().HasData(new Pizza
                {
                    Id = 2,
                    Name = "Mexicana",
                    Description = "Cheese, Salami, Capsicum, Chilli",
                    BasePrice = 18,
                    PizzeriaId = 1,
                    IsActive = true
                });

                modelBuilder.Entity<Pizza>().HasData(new Pizza
                {
                    Id = 3,
                    Name = "Margherita",
                    Description = "Cheese, Spinach, Ricotta, Cherry Tomatoes",
                    BasePrice = 22,
                    PizzeriaId = 1,
                    IsActive = true
                });

                modelBuilder.Entity<Pizza>().HasData(new Pizza
                {
                    Id = 4,
                    Name = "Capricciosa",
                    Description = " Cheese, Ham, Mushrooms, Olives",
                    BasePrice = 25,
                    PizzeriaId = 2,
                    IsActive = true
                });

                modelBuilder.Entity<Pizza>().HasData(new Pizza
                {
                    Id = 5,
                    Name = "Vegetarian",
                    Description = "Cheese, Mushrooms, Capsicum, Onion, Olives",
                    BasePrice = 17,
                    PizzeriaId = 2,
                    IsActive = true
                });

                modelBuilder.Entity<Topping>().HasData(new Topping
                {
                    Id = 1,
                    Name = "Cheese",
                    BasePrice = 1,
                    IsActive = true,
                });

                modelBuilder.Entity<Topping>().HasData(new Topping
                {
                    Id = 2,
                    Name = "Mushrooms",
                    BasePrice = 1,
                    IsActive = true,
                });

                modelBuilder.Entity<Topping>().HasData(new Topping
                {
                    Id = 3,
                    Name = "Capsicum",
                    BasePrice = 1,
                    IsActive = true,
                });

                modelBuilder.Entity<Topping>().HasData(new Topping
                {
                    Id = 4,
                    Name = "Onion",
                    BasePrice = 1,
                    IsActive = true,
                });

                modelBuilder.Entity<Topping>().HasData(new Topping
                {
                    Id = 5,
                    Name = "Olives",
                    BasePrice = 1,
                    IsActive = true,
                });
            }

        }
    }
}
