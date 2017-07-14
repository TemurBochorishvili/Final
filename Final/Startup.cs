using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Final.Models.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;

namespace Final
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Tempo");
                options.UseOpenIddict();
            });

            services.AddOpenIddict(options =>
            {
                options.AddEntityFrameworkCoreStores<ApplicationDbContext>();
                options.AddMvcBinders();
                options.EnableTokenEndpoint("/connect/token");
                options.UseJsonWebTokens();
                options.AllowPasswordFlow();
                options.AddSigningCertificate("CN=AuthSample");

            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseOpenIddict();

            app.UseIdentity();

            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        // Initialize some test roles. In the real world, these would be setup explicitly by a role manager
        private string[] roles = new[] { "Administrator", "User" };

        private async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var newRole = new IdentityRole(role);
                    await roleManager.CreateAsync(newRole);
                    // In the real world, there might be claims associated with roles
                    // _roleManager.AddClaimAsync(newRole, new )
                }
            }
        }
    }
}
