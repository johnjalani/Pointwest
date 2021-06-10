using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Exam.Shared
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }

    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
