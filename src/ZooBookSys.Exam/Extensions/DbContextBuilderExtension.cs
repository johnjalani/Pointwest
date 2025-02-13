﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZooBookSys.Exam.Domain;
using ZooBookSys.Exam.Domain.Models;
using ZooBookSys.Exam.Domain.Repositories;
using ZooBookSys.Exam.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooBookSys.Exam.Extensions
{
    public static class DbContextBuilderExtension
    {
        public static void AddDbContextInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            services.AddTransient<IDateTimeService, SystemDateTimeService>();
            services.AddScoped<ApplicationContext>();
        }

        public static void SetupRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
