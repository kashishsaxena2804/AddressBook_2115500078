using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using System.Collections.Generic;

namespace AddressBookSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookBL _addressBookBL;

        public AddressBookController(IAddressBookBL addressBookBL)
        {
            _addressBookBL = addressBookBL;
        }

        [HttpGet]
        public ActionResult<List<AddressBookDTO>> GetAllContacts()
        {
            return Ok(_addressBookBL.GetAllContacts());
        }

        [HttpGet("{id}")]
        public ActionResult<AddressBookDTO> GetContactById(int id)
        {
            var contact = _addressBookBL.GetContactById(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public ActionResult<AddressBookDTO> AddContact([FromBody] AddressBookDTO contact)
        {
            var newContact = _addressBookBL.AddContact(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = newContact.Id }, newContact);
        }

        [HttpPut("{id}")]
        public ActionResult<AddressBookDTO> UpdateContact(int id, [FromBody] AddressBookDTO contact)
        {
            var updatedContact = _addressBookBL.UpdateContact(id, contact);
            if (updatedContact == null) return NotFound();
            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteContact(int id)
        {
            if (!_addressBookBL.DeleteContact(id)) return NotFound();
            return NoContent();
        }
    }
}
