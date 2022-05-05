using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();

        Task<Contact> GetByIdAsync(int id);

        Task<bool> IsCreatedAsync(int id);

        Task<bool> IsCreatedAsync(string firstName, string lastName, string phoneNumber);

        Task<bool> CreateAsync(Contact contact);

        Task<bool> UpdateAsync(Contact contact);

        Task<bool> DeleteAsync(int id);

        Task<int> GetCountYearAsync();

        Task<int> GetCountMouthAsync();

        Task<int> GetCountTodayAsync();
    }
}
