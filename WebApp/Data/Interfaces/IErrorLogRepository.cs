using System;
using System.Threading.Tasks;

namespace WebApp.Data.Interfaces
{
    public interface IErrorLogRepository
    {
        void SaveException(string className, string action, Exception e);
        Task SaveExceptionAsync(string className, string action, Exception e);
    }
}
