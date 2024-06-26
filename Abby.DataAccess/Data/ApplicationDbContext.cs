﻿
using Abby.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AbbyWeb.DataAccess.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Foodtype> Foodtype { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public  DbSet<OrderHeader> OrderHeader { get; set; }
        public  DbSet<OrderDetails> OrderDetails { get; set; } 
        
    }
}
