﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiStaffService1.Data;

namespace WebApiStaffService1
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
            var connection = Configuration.GetConnectionString("DefaultConnectionStuffTest2"); //ProdConnectionStaff || TestConnectionStaff || DefaultConnectionStuffTest2 || DefaultConnection

            services.AddDbContext<EnterpriseStructDbContext>(options =>
            options.UseSqlServer(connection, b => b.MigrationsAssembly("DataLayer")));

            // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionTest2")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add Cors
            services.AddCors(o => o.AddPolicy("EnterpriseStructPolicy", builder => {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            TestData.Initialize(app);
        }
    }
}

