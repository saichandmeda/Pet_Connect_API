using AngularAuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Context
{
	public class AppDbContext: DbContext, IDisposable
	{
		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{

		}

		public DbSet<User> Users { get; set; }

        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Prod> Prods { get; set; }

        //public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Hospital>().ToTable("hospitals");
            modelBuilder.Entity<Appointment>().ToTable("appointments");
            modelBuilder.Entity<Prod>().ToTable("prods");
            //modelBuilder.Entity<Rating>().ToTable("ratings");

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<User>().ToTable("users");
        //    modelBuilder.Entity<Hospitals>().ToTable("hospitals");
        //}

    }
}

