using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                        .HasIndex(e => new
                        {
                            e.NIK,
                            e.Email,
                            e.PhoneNumber
                        }).IsUnique();

            //Many and One
            modelBuilder.Entity<University>()
                        .HasMany(university => university.Educations)
                        .WithOne(education => education.University)
                        .HasForeignKey(education => education.UniversityGuid);

            modelBuilder.Entity<Room>()
                        .HasMany(room => room.Bookings)
                        .WithOne(booking => booking.Room)
                        .HasForeignKey(booking => booking.RoomGuid);

            // Employee - Booking (One to Many)
            modelBuilder.Entity<Employee>()
                        .HasMany(employee => employee.Bookings)
                        .WithOne(booking => booking.Employee)
                        .HasForeignKey(booking => booking.EmployeeGuid);

            // Role - AccountRole (One to Many)
            modelBuilder.Entity<Role>()
                        .HasMany(role => role.AccountRoles)
                        .WithOne(accountrole => accountrole.Role)
                        .HasForeignKey(accountrole => accountrole.RoleGuid);

            // Account - AccountRole (One to Many)
            modelBuilder.Entity<Account>()
                        .HasMany(account => account.AccountRoles)
                        .WithOne(accountrole => accountrole.Account)
                        .HasForeignKey(accountrole => accountrole.AccountGuid);


            // Education - Employee (One to One)
            modelBuilder.Entity<Education>()
                        .HasOne(education => education.Employee)
                        .WithOne(employee => employee.Education)
                        .HasForeignKey<Education>(education => education.Guid);

            // Account - Employee (One to One)
            modelBuilder.Entity<Account>()
                       .HasOne(account => account.Employee)
                       .WithOne(employee => employee.Account)
                       .HasForeignKey<Account>(account => account.Guid);

        }
    }
}
