using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using ModelLayer.Models;
using Moq;
using NUnit.Framework;
using RepositoryLayer.Interfaces;
using System.Collections.Generic;

namespace AddressBookTests
{
    [TestFixture]
    public class AddressBookBLTest
    {
        private Mock<IAddressBookRL> _mockAddressBookRL;
        private Mock<ICacheService> _mockCacheService;
        private AddressBookBL _addressBookBL;

        [SetUp]
        public void Setup()
        {
            _mockAddressBookRL = new Mock<IAddressBookRL>();
            _mockCacheService = new Mock<ICacheService>();
            _addressBookBL = new AddressBookBL(_mockAddressBookRL.Object, _mockCacheService.Object);
        }

        // ✅ Test 1: Get All Contacts
        [Test]
        public void Test_GetAllContacts_ReturnsList()
        {
            // Arrange
            var contacts = new List<AddressBookEntry>
            {
                new AddressBookEntry { Id = 1, Name = "User1", Email = "user1@example.com" },
                new AddressBookEntry { Id = 2, Name = "User2", Email = "user2@example.com" }
            };

            _mockAddressBookRL.Setup(r => r.GetAllContacts()).Returns(contacts);

            // Act
            var result = _addressBookBL.GetAllContacts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        // ✅ Test 2: Get Contact by ID
        [Test]
        public void Test_GetContactById_ReturnsContact()
        {
            // Arrange
            var contactId = 1;
            var contact = new AddressBookEntry { Id = contactId, Name = "Test User", Email = "test@example.com" };

            _mockAddressBookRL.Setup(r => r.GetContactById(contactId)).Returns(contact);

            // Act
            var result = _addressBookBL.GetContactById(contactId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(contactId, result.Id);
        }

        // ✅ Test 3: Add Contact
        [Test]
        public void Test_AddContact_ReturnsNewContact()
        {
            // Arrange
            var contact = new AddressBookEntry { Id = 1, Name = "New User", Email = "new@example.com" };

            _mockAddressBookRL.Setup(r => r.AddContact(It.IsAny<AddressBookEntry>())).Returns(contact);

            // Act
            var result = _addressBookBL.AddContact(contact);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(contact.Name, result.Name);
        }

        // ✅ Test 4: Update Contact
        [Test]
        public void Test_UpdateContact_ReturnsUpdatedContact()
        {
            // Arrange
            var contactId = 1;
            var updatedContact = new AddressBookEntry { Id = contactId, Name = "Updated Name", Email = "updated@example.com" };

            _mockAddressBookRL.Setup(r => r.UpdateContact(contactId, It.IsAny<AddressBookEntry>())).Returns(updatedContact);

            // Act
            var result = _addressBookBL.UpdateContact(contactId, updatedContact);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedContact.Name, result.Name);
        }

        // ✅ Test 5: Delete Contact
        [Test]
        public void Test_DeleteContact_ReturnsTrue()
        {
            // Arrange
            var contactId = 1;
            _mockAddressBookRL.Setup(r => r.DeleteContact(contactId)).Returns(true);

            // Act
            var result = _addressBookBL.DeleteContact(contactId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
