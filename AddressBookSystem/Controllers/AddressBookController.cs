using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace AddressBookSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookBL _addressBookBL;
        private readonly IValidator<AddressBookEntry> _validator;

        public AddressBookController(IAddressBookBL addressBookBL, IValidator<AddressBookEntry> validator)
        {
            _addressBookBL = addressBookBL;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<List<AddressBookEntry>> GetAllContacts()
        {
            return Ok(_addressBookBL.GetAllContacts());
        }

        [HttpGet("{id}")]
        public ActionResult<AddressBookEntry> GetContactById(int id)
        {
            var contact = _addressBookBL.GetContactById(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public ActionResult<AddressBookEntry> AddContact([FromBody] AddressBookEntry contact)
        {
            ValidationResult validationResult = _validator.Validate(contact);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var newContact = _addressBookBL.AddContact(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = newContact.Id }, newContact);
        }

        [HttpPut("{id}")]
        public ActionResult<AddressBookEntry> UpdateContact(int id, [FromBody] AddressBookEntry contact)
        {
            ValidationResult validationResult = _validator.Validate(contact);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

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
