using eTickets.Data.Static;
using Microsoft.AspNetCore.Identity;

namespace eTickets.Models.Data
{
    public   class AppDbInitializer
    {
        //Seed Database Users and Roles.
        private readonly RequestDelegate _next;
        public AppDbInitializer(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await _next(context);
        }


        public static async Task SeedUserRolesAsync(IApplicationBuilder applicationBuilder)
        {
            try
            {
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    //var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                    //context.Database.EnsureCreated();
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    //var roleManager = applicationBuilder.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
                    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                    if (!await roleManager.RoleExistsAsync(UserRoles.User))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                    
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    //var userManger = applicationBuilder.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();
                    var adminUser = await userManager.FindByEmailAsync("admin@etickets.com");
                    if (adminUser == null)
                    {
                        var newAdminUser = new ApplicationUser()
                        {
                            Id =  roleManager.FindByIdAsync(UserRoles.Admin).Id.ToString(),
                            FullName = "Admin-User",
                            Email = "admin@etickets.com",
                            UserName = "Admin",
                            EmailConfirmed = true
                        };
                        await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                    }


                    var appUser = await userManager.FindByEmailAsync("appUser@etickets.com");
                    if (appUser == null)
                    {
                        var newAppUser = new ApplicationUser()
                        {
                            Id = roleManager.FindByIdAsync(UserRoles.User).Id.ToString(),
                            FullName = "Application User",
                            Email = "appUser@etickets.com",
                            UserName = "app-user",
                            EmailConfirmed = true
                        };
                        await userManager.CreateAsync(newAppUser, "Coding@1234?");
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                    }


                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            
        }

       
    }
}
