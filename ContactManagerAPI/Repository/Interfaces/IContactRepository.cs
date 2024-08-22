using ContactManagerAPI.Models;

namespace ContactManagerAPI.Repository.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> CreateAsync(Contact contact);
        Task<Contact> UpdateAsync(Contact contact);
        Task DeleteAsync(int id);
        Task<Contact> GetByIdAsync(int id);
        Task<List<Contact>> GetAsync();
    }
}
