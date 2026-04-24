using Microsoft.EntityFrameworkCore;
using Swadify_API.Entities;

namespace Swadify_API.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<RestaurantCategory> RestaurantCategories => Set<RestaurantCategory>();
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<DeliveryPartnerProfile> DeliveryPartnerProfiles => Set<DeliveryPartnerProfile>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<DeliveryTracking> DeliveryTrackings => Set<DeliveryTracking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(e =>
            {
                e.HasIndex(u => u.Email).IsUnique();
                e.HasIndex(u => u.Username).IsUnique();
                e.HasIndex(u => u.PhoneNumber).IsUnique();
                e.Property(u => u.Role).HasConversion<int>();
            });

            // Restaurant
            modelBuilder.Entity<Restaurant>(e =>
            {
                e.HasOne(r => r.Owner)
                 .WithMany(u => u.OwnedRestaurants)
                 .HasForeignKey(r => r.OwnerId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(r => r.Category)
                 .WithMany(c => c.Restaurants)
                 .HasForeignKey(r => r.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.Property(r => r.Status).HasConversion<int>();
                e.Property(r => r.DeliveryFee).HasColumnType("decimal(18,2)");
                e.Property(r => r.MinimumOrderAmount).HasColumnType("decimal(18,2)");
            });

            // MenuItem
            modelBuilder.Entity<MenuItem>(e =>
            {
                e.HasOne(m => m.Restaurant)
                 .WithMany(r => r.MenuItems)
                 .HasForeignKey(m => m.RestaurantId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(m => m.Category)
                 .WithMany(c => c.MenuItems)
                 .HasForeignKey(m => m.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.Property(m => m.Price).HasColumnType("decimal(18,2)");
                e.Property(m => m.DiscountedPrice).HasColumnType("decimal(18,2)");
            });

            // Cart
            modelBuilder.Entity<Cart>(e =>
            {
                e.HasOne(c => c.Customer)
                 .WithMany()
                 .HasForeignKey(c => c.CustomerId)
                 .OnDelete(DeleteBehavior.Cascade);

                // One cart per customer per restaurant
                e.HasIndex(c => new { c.CustomerId, c.RestaurantId }).IsUnique();
            });

            // CartItem
            modelBuilder.Entity<CartItem>(e =>
            {
                e.HasOne(ci => ci.Cart)
                 .WithMany(c => c.CartItems)
                 .HasForeignKey(ci => ci.CartId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(ci => ci.MenuItem)
                 .WithMany(m => m.CartItems)
                 .HasForeignKey(ci => ci.MenuItemId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.Property(ci => ci.UnitPrice).HasColumnType("decimal(18,2)");
            });

            // Order
            modelBuilder.Entity<Order>(e =>
            {
                e.HasOne(o => o.Customer)
                 .WithMany(u => u.Orders)
                 .HasForeignKey(o => o.CustomerId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(o => o.Restaurant)
                 .WithMany(r => r.Orders)
                 .HasForeignKey(o => o.RestaurantId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(o => o.DeliveryPartner)
                 .WithMany()
                 .HasForeignKey(o => o.DeliveryPartnerId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.Property(o => o.Status).HasConversion<int>();
                e.Property(o => o.PaymentStatus).HasConversion<int>();
                e.Property(o => o.PaymentMethod).HasConversion<int>();
                e.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
                e.Property(o => o.DeliveryFee).HasColumnType("decimal(18,2)");
                e.Property(o => o.TaxAmount).HasColumnType("decimal(18,2)");
                e.Property(o => o.DiscountAmount).HasColumnType("decimal(18,2)");
                e.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");

                e.HasIndex(o => o.OrderNumber).IsUnique();
            });

            // OrderItem
            modelBuilder.Entity<OrderItem>(e =>
            {
                e.HasOne(oi => oi.Order)
                 .WithMany(o => o.OrderItems)
                 .HasForeignKey(oi => oi.OrderId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(oi => oi.MenuItem)
                 .WithMany(m => m.OrderItems)
                 .HasForeignKey(oi => oi.MenuItemId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
                e.Property(oi => oi.TotalPrice).HasColumnType("decimal(18,2)");
            });

            // Payment
            modelBuilder.Entity<Payment>(e =>
            {
                e.HasOne(p => p.Order)
                 .WithOne(o => o.Payment)
                 .HasForeignKey<Payment>(p => p.OrderId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.Property(p => p.Status).HasConversion<int>();
                e.Property(p => p.Method).HasConversion<int>();
                e.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            });

            // Review
            modelBuilder.Entity<Review>(e =>
            {
                e.HasOne(r => r.Customer)
                 .WithMany(u => u.Reviews)
                 .HasForeignKey(r => r.CustomerId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(r => r.Order)
                 .WithOne(o => o.Review)
                 .HasForeignKey<Review>(r => r.OrderId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(r => r.Restaurant)
                 .WithMany(rest => rest.Reviews)
                 .HasForeignKey(r => r.RestaurantId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(r => r.MenuItem)
                 .WithMany(m => m.Reviews)
                 .HasForeignKey(r => r.MenuItemId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // DeliveryPartnerProfile
            modelBuilder.Entity<DeliveryPartnerProfile>(e =>
            {
                e.HasOne(dp => dp.User)
                 .WithOne(u => u.DeliveryProfile)
                 .HasForeignKey<DeliveryPartnerProfile>(dp => dp.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // Notification
            modelBuilder.Entity<Notification>(e =>
            {
                e.HasOne(n => n.User)
                 .WithMany(u => u.Notifications)
                 .HasForeignKey(n => n.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.Property(n => n.Type).HasConversion<int>();
            });

            // Seed default data
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Restaurant Categories
            modelBuilder.Entity<RestaurantCategory>().HasData(
                new RestaurantCategory { Id = 1, Name = "Café", Description = "Coffee and light bites", DisplayOrder = 1 },
                new RestaurantCategory { Id = 2, Name = "Bar & Grill", Description = "Grills, BBQ and drinks", DisplayOrder = 2 },
                new RestaurantCategory { Id = 3, Name = "Fine Dining", Description = "Premium restaurant experience", DisplayOrder = 3 },
                new RestaurantCategory { Id = 4, Name = "Fast Food", Description = "Quick service restaurants", DisplayOrder = 4 },
                new RestaurantCategory { Id = 5, Name = "Pizzeria", Description = "Pizza and Italian food", DisplayOrder = 5 },
                new RestaurantCategory { Id = 6, Name = "Chinese", Description = "Chinese cuisine", DisplayOrder = 6 },
                new RestaurantCategory { Id = 7, Name = "Indian", Description = "Indian cuisine", DisplayOrder = 7 },
                new RestaurantCategory { Id = 8, Name = "Bakery", Description = "Breads, cakes and pastries", DisplayOrder = 8 }
            );

            // Seed Menu Categories
            modelBuilder.Entity<MenuCategory>().HasData(
                new MenuCategory { Id = 1, Name = "Starters", DisplayOrder = 1 },
                new MenuCategory { Id = 2, Name = "Main Course", DisplayOrder = 2 },
                new MenuCategory { Id = 3, Name = "Rice & Biryani", DisplayOrder = 3 },
                new MenuCategory { Id = 4, Name = "Breads", DisplayOrder = 4 },
                new MenuCategory { Id = 5, Name = "Soups & Salads", DisplayOrder = 5 },
                new MenuCategory { Id = 6, Name = "Desserts", DisplayOrder = 6 },
                new MenuCategory { Id = 7, Name = "Beverages", DisplayOrder = 7 },
                new MenuCategory { Id = 8, Name = "Fast Food", DisplayOrder = 8 },
                new MenuCategory { Id = 9, Name = "Combo Meals", DisplayOrder = 9 }
            );
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
