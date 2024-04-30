using AngularAuthAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using static AngularAuthAPI.Models.Forum;

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

        public DbSet<Product> Products { get; set; }

        public DbSet<Center> Centers { get; set; }

        public DbSet<Pet> Pets { get; set; }

        //public DbSet<Forum> Forums { get; set; }

        //public DbSet<ForumUser> ForumUsers { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public DbSet<ForumLike> ForumLikes { get; set; }
        public DbSet<ForumPostList> ForumPostLists { get; set; }
        public DbSet<ForumPrivateChat> ForumPrivateChats { get; set; }


        //public IQueryable<ForumPostList> getAllPostForUserId(int query)
        //{
        //    SqlParameter search = new SqlParameter("@userId", query);
        //    return this.ForumPostLists.FromSqlRaw("EXECUTE getAllPostsByUserId @userId", search);
        //}
        //public IQueryable<ForumPostList> getAllPostForUserIdByPostId(int userId, int postId)
        //{
        //    SqlParameter search = new SqlParameter("@userId", userId);
        //    SqlParameter search2 = new SqlParameter("@postId", postId);
        //    return this.ForumPostLists.FromSqlRaw("EXECUTE getAllPostsByUserIdByPostId @userId,@postId", search, search2);
        //}

        public IQueryable<ForumPostList> getAllPostForUserId(int userId)
        {
            // Prepare the parameter for the stored procedure
            MySqlParameter search = new MySqlParameter("@userId", userId);

            // Call the stored procedure using FromSqlRaw
            return this.ForumPostLists.FromSqlRaw("CALL getAllPostsByUserId(@userId)", search);
        }

        public IQueryable<ForumPostList> getAllPostForUserIdByPostId(int userId, int postId)
        {
            // Prepare the parameters for the stored procedure
            MySqlParameter search = new MySqlParameter("@userId", userId);
            MySqlParameter search2 = new MySqlParameter("@postId", postId);

            // Call the stored procedure using FromSqlRaw
            return this.ForumPostLists.FromSqlRaw("CALL getAllPostsByUserIdByPostId(@userId, @postId)", search, search2);
        }


        //public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Hospital>().ToTable("hospitals");
            modelBuilder.Entity<Appointment>().ToTable("appointments");
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Center>().ToTable("centers");
            modelBuilder.Entity<Pet>().ToTable("pets");

            //modelBuilder.Entity<Forum>().ToTable("forums");
            //modelBuilder.Entity<ForumUser>().ToTable("forumusers");
            modelBuilder.Entity<ForumPost>().ToTable("forumPost");
            modelBuilder.Entity<ForumComment>().ToTable("forumComments");
            modelBuilder.Entity<ForumLike>().ToTable("forumLikes");
            //modelBuilder.Entity<ForumPostList>().ToTable("forumpostlists");
            modelBuilder.Entity<ForumPrivateChat>().ToTable("forumPrivateChat");

            //modelBuilder.Entity<Rating>().ToTable("ratings");

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<User>().ToTable("users");
        //    modelBuilder.Entity<Hospitals>().ToTable("hospitals");
        //}

    }
}

