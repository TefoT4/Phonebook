
using System.Collections.Generic;
using Phonebook.Models;

namespace Phonebook.DataAccess
{
    public interface IPhonebookDatabase
    {
        bool Delete(int id);

        List<ContactModel> GetAll();

        ContactModel GetContact(int id);

        ContactModel CreateContact(ContactModel contact);

        ContactModel UpdateContact(ContactModel contact);
        
    }
}