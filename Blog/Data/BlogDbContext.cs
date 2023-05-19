using System;
using Blog.Models;
using Blog.Models.Comments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
	public class BlogDbContext: IdentityDbContext
	{     

        public BlogDbContext()
		{           
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<MainComment> mainComments { get; set; }
        public DbSet<SubComment> subComments { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);      
        }        
    }
}

