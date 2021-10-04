using System.Collections.Generic;
using Phonebook.Models;

namespace Phonebook.Repository
{
    public interface IRepository
    {
        bool Any(int id);

        List<ContactModel> GetAll();

        ContactModel GetById(int id);

        ContactModel Add(ContactModel entity);
        
        bool Delete(ContactModel entity);

        ContactModel Update(ContactModel entity);
    }
}