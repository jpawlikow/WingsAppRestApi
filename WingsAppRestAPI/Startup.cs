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
using WingsAppBLL;
using WingsAppBLL.BusinessObjects;
using WingsAppDAL.Context;

namespace WingsAppRestAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(o => o.AddPolicy("MyPolicy", builder => {
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                }));
            // services.AddEntityFrameworkNpgsql().
            // AddDbContext<WingsAppContext>(opt =>opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WingsAppRestAPI")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var facade = new BLLFacade();

                // var reporter = facade.UserProfileService.Create(new UserProfileBO() {FirstName="First", LastName="Last", JoinDate = DateTime.Now});
                // var assigner = facade.UserProfileService.Create(new UserProfileBO() {FirstName="Assi", LastName="Gner", JoinDate = DateTime.Now});
                // facade.UserProfileService.Create(new UserProfileBO() {FirstName="Second", LastName="LastSec", JoinDate = DateTime.Now.AddMonths(-1)});

                // facade.UserEventService.Create(new UserEventBO() {
                //     Title="First Title", 
                //     Description="Desc1", 
                //     ReporterId=reporter.Id, 
                //     AssignersIds = new List<int>() { assigner.Id }
                // });
                // facade.UserEventService.Create(new UserEventBO() {
                //     Title="Second Title", 
                //     Description="Desc2", 
                //     ReporterId=reporter.Id, 
                //     AssignersIds = new List<int>() { assigner.Id }
                // });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
