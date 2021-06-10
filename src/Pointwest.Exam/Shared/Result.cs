using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Exam.Shared
{
    public class Result<T>
    {
		public string Message { get; set; }
		public bool Succeeded { get; set; }
		public T Data { get; set; }
		public static Result<T> Fail(string message)
		{
			return new Result<T>
			{
				Succeeded = false,
				Message = message
			};
		}
		public static Task<Result<T>> FailAsync(string message)
		{
			return Task.FromResult(Fail(message));
		}
	}
}
