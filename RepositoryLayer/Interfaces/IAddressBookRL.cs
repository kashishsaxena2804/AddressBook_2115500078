using ModelLayer.DTO;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressBookRL
    {
        List<AddressBookDTO> GetAllContacts();
        AddressBookDTO GetContactById(int id);
        AddressBookDTO AddContact(AddressBookDTO contact);
        AddressBookDTO UpdateContact(int id, AddressBookDTO contact);
        bool DeleteContact(int id);
    }
}
