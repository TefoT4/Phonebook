
using System.Linq;
using Phonebook.DataAccess;
using System.Collections.Generic;
using Phonebook.Models;

namespace Phonebook.Repository
{
    public class Repository : IRepository
    {
        private readonly IPhonebookDatabase _phonebookDatabase;

        public Repository(IPhonebookDatabase phonebookDatabase)
        {
            _phonebookDatabase = phonebookDatabase;
        }
        
        public bool Any(int id)
        {
            return _phonebookDatabase
                .GetAll()
                .Any(x => x.Id == id);
        }

        public List<ContactModel> GetAll()
        {
            return _phonebookDatabase
                .GetAll();
        }

        public ContactModel GetById(int id)
        {
            return _phonebookDatabase
                .GetAll()
                .Find(x => x.Id == id);
        }

        public ContactModel Add(ContactModel entity)
        {
            return _phonebookDatabase
                .CreateContact(entity);
        }

        public bool Delete(ContactModel entity)
        {
            return _phonebookDatabase
                .Delete(entity.Id);
        }

        public ContactModel Update(ContactModel entity)
        {
            return _phonebookDatabase
                .UpdateContact(entity);
        }
        
    }
}
