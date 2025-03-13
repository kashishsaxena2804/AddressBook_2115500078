using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
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

        public List<AddressBookDTO> GetAllContacts()
        {
            return _context.AddressBookEntries.ToList();
        }

        public AddressBookDTO GetContactById(int id)
        {
            return _context.AddressBookEntries.Find(id);
        }

        public AddressBookDTO AddContact(AddressBookDTO contact)
        {
            _context.AddressBookEntries.Add(contact);
            _context.SaveChanges();
            return contact;
        }

        public AddressBookDTO UpdateContact(int id, AddressBookDTO contact)
        {
            var existingContact = _context.AddressBookEntries.Find(id);
            if (existingContact == null) return null;

            existingContact.Name = contact.Name;
            existingContact.Email = contact.Email;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.Address = contact.Address;

            _context.SaveChanges();
            return existingContact;
        }

        public bool DeleteContact(int id)
        {
            var contact = _context.AddressBookEntries.Find(id);
            if (contact == null) return false;

            _context.AddressBookEntries.Remove(contact);
            _context.SaveChanges();
            return true;
        }
    }
}
