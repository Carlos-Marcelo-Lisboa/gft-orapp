using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderApp.Model.Enum;

namespace RestaurantOrderApp.DAL
{
    public class RestaurantOrderAppContext : DbContext
    {
        public DbSet<OrderOption> OrderOptions { get; set; }

      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ASUS\SQLEXPRESS;Database=RestaurantOrderApp;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderOption>().HasKey(p => new { p.DishType, p.TimeOfDay });
            modelBuilder.Entity<OrderOption>().HasData(
                new OrderOption() { TimeOfDay = (int)TimeOfDay.morning, DishType = (int)DishType.entree, Name = "eggs", MaxQuantity = 1, MinQuantity = 1 },
                new OrderOption() { TimeOfDay = (int)TimeOfDay.morning, DishType = (int)DishType.side, Name = "toast", MaxQuantity = 1, MinQuantity = 1 },
                new OrderOption() { TimeOfDay = (int)TimeOfDay.morning, DishType = (int)DishType.drink, Name = "coffee", MaxQuantity = 100, MinQuantity = 1 },
                new OrderOption() { TimeOfDay = (int)TimeOfDay.night, DishType = (int)DishType.entree, Name = "steak", MaxQuantity = 1, MinQuantity = 1 },
                new OrderOption() { TimeOfDay = (int)TimeOfDay.night, DishType = (int)DishType.side, Name = "potato", MaxQuantity = 100, MinQuantity = 1 },
                new OrderOption() { TimeOfDay = (int)TimeOfDay.night, DishType = (int)DishType.drink, Name = "wine", MaxQuantity = 1, MinQuantity = 1 },
                new OrderOption() { TimeOfDay = (int)TimeOfDay.night, DishType = (int)DishType.dessert, Name = "cake", MaxQuantity = 1, MinQuantity = 1 }
                );
        }
    }

    public class OrderOption
    {
        [Required]
        public int DishType { get; set; }
        [Required]
        public int TimeOfDay { get; set; }
        [Required]
        public string Name { get; set; }
        [Required] 
        public int MaxQuantity { get; set; }
        [Required] 
        public int MinQuantity { get; set; }
    }
}
