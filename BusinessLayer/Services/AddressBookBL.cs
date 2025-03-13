using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class AddressBookBL : IAddressBookBL
    {
        private readonly IAddressBookRL _addressBookRL;

        public AddressBookBL(IAddressBookRL addressBookRL)
        {
            _addressBookRL = addressBookRL;
        }

        public List<AddressBookEntry> GetAllContacts()
        {
            return _addressBookRL.GetAllContacts();
        }

        public AddressBookEntry GetContactById(int id)
        {
            return _addressBookRL.GetContactById(id);
        }

        public AddressBookEntry AddContact(AddressBookEntry contact)
        {
            return _addressBookRL.AddContact(contact);
        }

        public AddressBookEntry UpdateContact(int id, AddressBookEntry contact)
        {
            return _addressBookRL.UpdateContact(id, contact);
        }

        public bool DeleteContact(int id)
        {
            return _addressBookRL.DeleteContact(id);
        }
    }
}
