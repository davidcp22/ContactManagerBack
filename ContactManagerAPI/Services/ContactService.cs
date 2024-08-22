using ContactManagerAPI.Models;
using ContactManagerAPI.Repository.Interfaces;
using ContactManagerAPI.Services.Interfaces;

namespace ContactManagerAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> CreateAsync(Contact contact)
        {
            return await _contactRepository.CreateAsync(contact);
        }

        public Task DeleteAsync(int id)
        {
            return _contactRepository.DeleteAsync(id);
        }

        public Task<List<Contact>> GetAsync()
        {
            return _contactRepository.GetAsync();
        }

        public Task<Contact> GetByIdAsync(int id)
        {
           return _contactRepository.GetByIdAsync(id);
        }

        public async Task<Contact> UpdateAsync(Contact contact)
        {
            return await _contactRepository.UpdateAsync(contact);
        }
    }
}
