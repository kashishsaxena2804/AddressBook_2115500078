using System.Collections.Generic;
using ModelLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBookBL
    {
        List<AddressBookEntry> GetAllContacts();
        AddressBookEntry GetContactById(int id);
        AddressBookEntry AddContact(AddressBookEntry contact);
        AddressBookEntry UpdateContact(int id, AddressBookEntry contact);
        bool DeleteContact(int id);
    }
}
