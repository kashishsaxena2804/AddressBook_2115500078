using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
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
        public ActionResult<IEnumerable<AddressBookEntry>> GetAllContacts()
        {
            var contacts = _addressBookBL.GetAllContacts();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public ActionResult<AddressBookEntry> GetContactById(int id)
        {
            var contact = _addressBookBL.GetContactById(id);
            if (contact == null)
                return NotFound(new { message = "Contact not found" });

            return Ok(contact);
        }

        [HttpPost]
        public ActionResult<AddressBookEntry> AddContact([FromBody] AddressBookEntry contact)
        {
            var newContact = _addressBookBL.AddContact(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = newContact.Id }, newContact);
        }

        [HttpPut("{id}")]
        public ActionResult<AddressBookEntry> UpdateContact(int id, [FromBody] AddressBookEntry contact)
        {
            var updatedContact = _addressBookBL.UpdateContact(id, contact);
            if (updatedContact == null)
                return NotFound(new { message = "Contact not found" });

            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteContact(int id)
        {
            bool isDeleted = _addressBookBL.DeleteContact(id);
            if (!isDeleted)
                return NotFound(new { message = "Contact not found" });

            return NoContent();
        }
    }
}
