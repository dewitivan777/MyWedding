﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Email.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Email.Services;
using Microsoft.EntityFrameworkCore;
using TestProject;
using MyWedding.Repository;

namespace MyWedding
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IEmailService, EmailService>();

            services.AddDbContext<Identitydbcontext>(options =>
     options.UseSqlite("Data Source=users.sqlite",
         optionsBuilder => optionsBuilder.MigrationsAssembly("MyWedding")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<Identitydbcontext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<AppDbContext>(options =>
              options.UseSqlite("Data Source=guests.sqlite",
               optionsBuilder => optionsBuilder.MigrationsAssembly("MyWedding")));

            services.AddScoped(typeof(IGuestRepository<>), typeof(GuestRepository<>));

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
       
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
