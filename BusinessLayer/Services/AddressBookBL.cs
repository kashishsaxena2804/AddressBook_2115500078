using BusinessLayer.Interfaces;
using ModelLayer.DTO;
using RepositoryLayer.Interfaces;


namespace BusinessLayer.Services
{
    public class AddressBookBL : IAddressBookBL
    {
        private readonly IAddressBookRL _addressBookRL;

        public AddressBookBL(IAddressBookRL addressBookRL)
        {
            _addressBookRL = addressBookRL;
        }

        public async Task<List<AddressBookDTO>> GetAllContacts()
        {
            return await _addressBookRL.GetAllContacts();
        }

        public async Task<AddressBookDTO> GetContactById(int id)
        {
            return await _addressBookRL.GetContactById(id);
        }

        public async Task<AddressBookDTO> AddContact(AddressBookDTO contact)
        {
            return await _addressBookRL.AddContact(contact);
        }

        public async Task<AddressBookDTO> UpdateContact(int id,AddressBookDTO contact)
        {
            return await _addressBookRL.UpdateContact(id, contact);
        }

        public async Task<bool> DeleteContact(int id)
        {
            return await _addressBookRL.DeleteContact(id);
        }
    }
}
