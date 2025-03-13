using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBookSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookBL _addressBookBL;

        public AddressBookController(IAddressBookBL addressBookBL)
        {
            _addressBookBL = addressBookBL;
        }

        [HttpGet]
        public async Task<ActionResult<List<AddressBookDTO>>> GetAllContacts()
        {
            return Ok(await _addressBookBL.GetAllContacts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressBookDTO>> GetContactById(int id)
        {
            var contact = await _addressBookBL.GetContactById(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult<AddressBookDTO>> AddContact([FromBody] AddressBookDTO contact)
        {
            var newContact = await _addressBookBL.AddContact(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = newContact.Id }, newContact);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AddressBookDTO>> UpdateContact(int id, [FromBody] AddressBookDTO contact)
        {
            var updatedContact = await _addressBookBL.UpdateContact(id, contact);
            if (updatedContact == null) return NotFound();
            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            if (!await _addressBookBL.DeleteContact(id)) return NotFound();
            return NoContent();
        }
    }
}
