using System;
using System.Linq;
using E1.Web.DataAccess;
using E1.Web.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace E1.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("app"));
            });

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                SeedDatabase(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void SeedDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    dbContext.Database.EnsureCreated();
                    
                    var adminGroup = new Group {Name = "Administrators"};
                    var teachersGroup = new Group {Name = "Teachers"};
                    var studentsGroup = new Group {Name = "Students"};
                    CreateGroups(dbContext, new[]
                    {
                        adminGroup,
                        teachersGroup,
                        studentsGroup
                    });
                    CreatePersons(dbContext, new[]
                    {
                        new Person {Name = "John Smith", CreatedTimestamp = DateTime.UtcNow, Group = adminGroup},
                        new Person {Name = "John Boss", CreatedTimestamp = DateTime.UtcNow, Group = adminGroup},
                        new Person {Name = "May Schwarz", CreatedTimestamp = DateTime.UtcNow, Group = teachersGroup},
                        new Person {Name = "Mufasa", CreatedTimestamp = DateTime.UtcNow, Group = studentsGroup},
                        new Person {Name = "Simba", CreatedTimestamp = DateTime.UtcNow, Group = studentsGroup},
                        new Person {Name = "Nala", CreatedTimestamp = DateTime.UtcNow, Group = studentsGroup}
                    });

                    dbContext.SaveChanges();
                }
            }
        }

        private static void CreateGroups(AppDbContext dbContext, Group[] groups)
        {
            foreach (var group in groups)
            {
                var existingGroup = dbContext.Groups.SingleOrDefault(g => g.Name == group.Name);

                if (existingGroup == null)
                {
                    dbContext.Groups.Add(group);
                }
            }
        }

        private static void CreatePersons(AppDbContext dbContext, Person[] persons)
        {
            foreach (var person in persons)
            {
                var existingPerson = dbContext.Groups.SingleOrDefault(g => g.Name == person.Name);

                if (existingPerson == null)
                {
                    dbContext.Persons.Add(person);
                }
            }
        }
    }
}
