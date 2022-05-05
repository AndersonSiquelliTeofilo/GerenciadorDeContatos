using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Interfaces;
using WebApp.Models;

namespace WebApp.Data.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppContext _context;

        public ContactRepository(AppContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            try
            {
                return await _context.Contacts.AsNoTracking()
                    .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Contacts.AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> IsCreatedAsync(int id)
        {
            try
            {
                return await _context.Contacts.AnyAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> IsCreatedAsync(string firstName, string lastName, string phoneNumber)
        {
            try
            {
                return await _context.Contacts
                    .AnyAsync(x => x.FirstName == firstName && x.LastName == lastName && x.PhoneNumber == phoneNumber);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> CreateAsync(Contact contact)
        {
            try
            {
                bool exist = await IsCreatedAsync(contact.FirstName, contact.LastName, contact.PhoneNumber);

                if (exist == false)
                {
                    await _context.Contacts.AddAsync(contact);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Contact contact)
        {
            try
            {
                bool exist = await IsCreatedAsync(contact.Id);

                if (exist)
                {
                    _context.Contacts.Update(contact);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                bool exist = await IsCreatedAsync(id);

                if (exist)
                {
                    Contact contact = await GetByIdAsync(id);
                    _context.Contacts.Remove(contact);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return false;
        }

        public async Task<int> GetCountYearAsync()
        {
            DateTime dateTime = DateTime.Now;

            return await _context.Contacts.CountAsync(x => x.CreationDate.Year >= dateTime.Year && x.CreationDate.Year < (dateTime.Year + 1));
        }

        public async Task<int> GetCountMouthAsync()
        {
            DateTime dateTime = DateTime.Now;

            return await _context.Contacts
                .CountAsync(x => x.CreationDate.Year >= dateTime.Year && x.CreationDate.Year < (dateTime.Year + 1) &&
                        x.CreationDate.Month >= dateTime.Month && x.CreationDate.Month < (dateTime.Month + 1));
        }

        public async Task<int> GetCountTodayAsync()
        {
            DateTime dateTime = DateTime.Now;
            DateTime dateStart = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);

            return await _context.Contacts.CountAsync(x => x.CreationDate >= dateStart && x.CreationDate <= dateTime);
        }
    }
}
