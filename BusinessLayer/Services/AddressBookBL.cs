using BusinessLayer.Interfaces;
using ModelLayer.DTO;
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

        public List<AddressBookDTO> GetAllContacts()
        {
            return _addressBookRL.GetAllContacts();
        }

        public AddressBookDTO GetContactById(int id)
        {
            return _addressBookRL.GetContactById(id);
        }

        public AddressBookDTO AddContact(AddressBookDTO contact)
        {
            return _addressBookRL.AddContact(contact);
        }

        public AddressBookDTO UpdateContact(int id, AddressBookDTO contact)
        {
            return _addressBookRL.UpdateContact(id, contact);
        }

        public bool DeleteContact(int id)
        {
            return _addressBookRL.DeleteContact(id);
        }
    }
}
