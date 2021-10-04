using Phonebook.Core;
using Phonebook.Domains;
using System.Collections.Generic;

namespace Phonebook.Services
{
    public interface IPhonebookService
    {
        ServiceResponse<List<Contact>> GetContacts();

        ServiceResponse<Contact> GetContactById(int id);

        ServiceResponse<bool> DeleteContactById(int id);

        ServiceResponse<Contact> CreateContact(Contact contact);

        ServiceResponse<Contact> EditContact(int i, Contact contact);
        
    }
}