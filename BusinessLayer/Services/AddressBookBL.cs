using BusinessLayer.Interfaces;
using ModelLayer.Models;
using Newtonsoft.Json;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class AddressBookBL : IAddressBookBL
    {
        private readonly IAddressBookRL _addressBookRL;
        private readonly ICacheService _cacheService;
        private const string CacheKey = "AllContacts";

        public AddressBookBL(IAddressBookRL addressBookRL, ICacheService cacheService)
        {
            _addressBookRL = addressBookRL;
            _cacheService = cacheService;
        }

        public List<AddressBookEntry> GetAllContacts()
        {
            string cachedData = _cacheService.GetData(CacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonConvert.DeserializeObject<List<AddressBookEntry>>(cachedData);
            }

            var contacts = _addressBookRL.GetAllContacts();
            _cacheService.SetData(CacheKey, JsonConvert.SerializeObject(contacts), TimeSpan.FromMinutes(10));

            return contacts;
        }

        public AddressBookEntry GetContactById(int id)
        {
            return _addressBookRL.GetContactById(id);
        }

        public AddressBookEntry AddContact(AddressBookEntry contact)
        {
            var newContact = _addressBookRL.AddContact(contact);
            _cacheService.RemoveData(CacheKey);  // Invalidate cache
            return newContact;
        }

        public AddressBookEntry UpdateContact(int id, AddressBookEntry contact)
        {
            var updatedContact = _addressBookRL.UpdateContact(id, contact);
            _cacheService.RemoveData(CacheKey);  // Invalidate cache
            return updatedContact;
        }

        public bool DeleteContact(int id)
        {
            bool isDeleted = _addressBookRL.DeleteContact(id);
            if (isDeleted)
            {
                _cacheService.RemoveData(CacheKey);  // Invalidate cache
            }
            return isDeleted;
        }
    }
}
