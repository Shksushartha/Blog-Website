using Blog.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            var host = CreateWebHostBuilder(args).Build();

            var scope = host.Services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            ctx.Database.EnsureCreated();


            var adminRole = new IdentityRole("Admin");
            if (!ctx.Roles.Any())
            {
                //create a role
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
            }

            if (!ctx.Users.Any(u => u.UserName == "Admin"))
            {
                //create a admin
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };
                userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                //add role to user
                userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
            }
            
            

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}