using ContactManagerAPI.Models;

namespace ContactManagerAPI.Services.Interfaces
{
    public interface IContactService
    {
        Task<Contact> CreateAsync(Contact contact);
        Task<Contact> UpdateAsync(Contact contact);
        Task DeleteAsync(int id);
        Task<Contact> GetByIdAsync(int id);
        Task<List<Contact>> GetAsync();
    }
}
