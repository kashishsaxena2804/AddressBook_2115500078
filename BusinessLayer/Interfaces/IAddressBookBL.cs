using ModelLayer.DTO;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBookBL
    {
        List<AddressBookDTO> GetAllContacts();
        AddressBookDTO GetContactById(int id);
        AddressBookDTO AddContact(AddressBookDTO contact);
        AddressBookDTO UpdateContact(int id, AddressBookDTO contact);
        bool DeleteContact(int id);
    }
}
