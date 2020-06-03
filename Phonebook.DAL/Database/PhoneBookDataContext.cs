using Microsoft.EntityFrameworkCore;
using Phonebook.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.DAL.Database
{
    public class PhoneBookDataContext : DbContext
    {
        public PhoneBookDataContext(DbContextOptions<PhoneBookDataContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneNumber>()
                .HasOne(s => s.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
