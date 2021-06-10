using Microsoft.Extensions.DependencyInjection;
using Pointwest.Exam.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Exam.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
