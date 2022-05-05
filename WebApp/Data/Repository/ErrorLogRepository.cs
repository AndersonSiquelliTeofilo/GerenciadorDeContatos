using System;
using System.Threading.Tasks;
using WebApp.Data.Interfaces;
using WebApp.Models;

namespace WebApp.Data.Repository
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly AppContext _context;

        public ErrorLogRepository(AppContext context)
        {
            _context = context;
        }

        public void SaveException(string className, string action, Exception e)
        {
            ErrorLog log = new ErrorLog()
            {
                DateOccurred = DateTime.Now,
                ClasseName = className,
                Action = action,
                StackTrace = e?.StackTrace,
                InnerException = e?.InnerException?.Message
            };

            _context.ErrorLogs.Add(log);
            _context.SaveChanges();
        }

        public async Task SaveExceptionAsync(string className, string action, Exception e)
        {
            ErrorLog log = new ErrorLog()
            {
                DateOccurred = DateTime.Now,
                ClasseName = className,
                Action = action,
                StackTrace = e?.StackTrace,
                InnerException = e?.InnerException?.Message
            };

            await _context.ErrorLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
