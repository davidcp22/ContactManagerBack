using ContactManagerAPI.Models;
using ContactManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult> GetContacts()
        {
            return Ok( await _contactService.GetAsync());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetContactById(int id)
        {
            return Ok(await _contactService.GetByIdAsync(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] Contact contact)
        {
            var createdContact = await _contactService.CreateAsync(contact);

            return CreatedAtAction(nameof(GetContacts), new { id = createdContact.Id }, createdContact);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] Contact contact)
        {
            var updatedContact = await _contactService.UpdateAsync(contact);

            if (updatedContact == null)
            {
                return NotFound($"Contact with ID {contact.Id} not found.");
            }

            return Ok(updatedContact);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult>  DeleteContact(int id)
        {
            await _contactService.DeleteAsync(id);

            return Ok($"Contact with ID {id} has been deleted.");
        }
    }

}
