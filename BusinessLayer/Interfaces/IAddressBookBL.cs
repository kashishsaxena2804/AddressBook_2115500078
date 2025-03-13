using ModelLayer.Models;
using System.Collections.Generic;

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
