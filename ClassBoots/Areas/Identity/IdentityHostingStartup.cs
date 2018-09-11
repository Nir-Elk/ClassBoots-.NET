﻿using System;
using ClassBoots.Areas.Identity.Data;
using ClassBoots.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ClassBoots.Areas.Identity.IdentityHostingStartup))]
namespace ClassBoots.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UserContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ClassBootsContextConnection")));

                services.AddDefaultIdentity<User>()
                    //.AddRoles<IdentityRole>()
                      .AddEntityFrameworkStores<UserContext>();
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            });
        }
    }
}