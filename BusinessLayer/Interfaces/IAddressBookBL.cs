using ModelLayer.DTO;


namespace BusinessLayer.Interfaces
{
    public interface IAddressBookBL
    {
        Task<List<AddressBookDTO>> GetAllContacts();
        Task<AddressBookDTO > GetContactById(int id);
        Task<AddressBookDTO > AddContact(AddressBookDTO contact);
        Task<AddressBookDTO> UpdateContact(int id,AddressBookDTO contact);
        Task<bool> DeleteContact(int id);
    }
}
