using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class AddressBookRL : IAddressBookRL
    {
        private readonly AddressBookDbContext _context;

        public AddressBookRL(AddressBookDbContext context)
        {
            _context = context;
        }

        public List<AddressBookEntry> GetAllContacts()
        {
            return _context.AddressBookEntries.ToList();
        }

        public AddressBookEntry GetContactById(int id)
        {
            return _context.AddressBookEntries.FirstOrDefault(c => c.Id == id);
        }

        public AddressBookEntry AddContact(AddressBookEntry contact)
        {
            _context.AddressBookEntries.Add(contact);
            _context.SaveChanges();
            return contact;
        }

        public AddressBookEntry UpdateContact(int id, AddressBookEntry contact)
        {
            var existingContact = _context.AddressBookEntries.FirstOrDefault(c => c.Id == id);
            if (existingContact != null)
            {
                existingContact.Name = contact.Name;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;
                existingContact.Address = contact.Address;
                _context.SaveChanges();
            }
            return existingContact;
        }

        public bool DeleteContact(int id)
        {
            var contact = _context.AddressBookEntries.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                _context.AddressBookEntries.Remove(contact);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
