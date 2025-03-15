using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace AddressBookSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookBL _addressBookBL;
        private readonly ICacheService _cacheService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public AddressBookController(IAddressBookBL addressBookBL, ICacheService cacheService, IHttpContextAccessor httpContextAccessor, IRabbitMQProducer rabbitMQProducer)
        {
            _addressBookBL = addressBookBL;
            _cacheService = cacheService;
            _httpContextAccessor = httpContextAccessor;
            _rabbitMQProducer = rabbitMQProducer;
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

            // Convert object to JSON and publish to RabbitMQ
            var message = JsonConvert.SerializeObject(newContact);
            _rabbitMQProducer.PublishMessage("contactQueue", message);

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

        [HttpGet("all")]
        public IActionResult GetAllContactsWithCache()
        {
            string cacheKey = "AllContacts";
            var cachedData = _cacheService.GetData(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
                return Ok(JsonConvert.DeserializeObject<List<AddressBookEntry>>(cachedData));

            var contacts = _addressBookBL.GetAllContacts();
            _cacheService.SetData(cacheKey, JsonConvert.SerializeObject(contacts), TimeSpan.FromMinutes(10));

            return Ok(contacts);
        }

        [HttpPost("store-session")]
        public IActionResult StoreSession(string key, string value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, value);
            return Ok("Session data stored.");
        }

        [HttpGet("retrieve-session")]
        public IActionResult RetrieveSession(string key)
        {
            string value = _httpContextAccessor.HttpContext.Session.GetString(key);
            return Ok(value ?? "No data found.");
        }

        [HttpPost("publish")]
        public IActionResult PublishMessage([FromBody] RabbitMQMessageModel messageModel)
        {
            _rabbitMQProducer.PublishMessage(messageModel.QueueName, messageModel.Message);
            return Ok(new { message = "Message published successfully!" });
        }

        public class RabbitMQMessageModel
        {
            public string QueueName { get; set; }
            public string Message { get; set; }
        }

    }
}
