using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooBookSys.Exam.Extensions
{
	public static class SerilogBuilderExtension
	{
		public static void UseSerilog(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(
					logger: new LoggerConfiguration()
					  .ReadFrom.Configuration(configuration)
					  .CreateLogger(),
				dispose: true));

		}
	}
}
