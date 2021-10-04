
using AutoMapper;
using System.Linq;
using Phonebook.Core;
using Phonebook.Domains;
using Phonebook.Repository;
using System.Collections.Generic;
using Phonebook.Models;

namespace Phonebook.Services
{
    public class PhonebookService : IPhonebookService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        
        public PhonebookService(IMapper mapper, IRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ServiceResponse<List<Contact>> GetContacts()
        {
            var contacts =  _repository.GetAll();
            var mappedContacts = _mapper.Map<List<ContactModel>, List<Contact>>(contacts.ToList());
            return new ServiceResponse<List<Contact>> { Entity = mappedContacts };
        }

        public ServiceResponse<Contact> GetContactById(int id)
        {
            var contact = _repository.GetById(id);
            var mappedContact = _mapper.Map<ContactModel, Contact>(contact);
            return new ServiceResponse<Contact> { Entity = mappedContact};
        }
        
        public ServiceResponse<Contact> CreateContact(Contact contact)
        {
            var createResult = _repository.Add(_mapper.Map<Contact, ContactModel>(contact));
            var mappedContact = _mapper.Map<ContactModel, Contact>(createResult);
            return new ServiceResponse<Contact> { Entity = mappedContact };
        }

        public ServiceResponse<Contact> EditContact(int id, Contact contact)
        {
            var findResponse = _repository.Any(id);

            if (!findResponse)
            {
                var errorList = new List<string> {$"Contact with Id {id}, not found."};
                return new ServiceResponse<Contact> { IsSuccessful = false, Errors = errorList };
            }

            var updateResult = _repository.Update(_mapper.Map<Contact, ContactModel>(contact));
            var mappedContact = _mapper.Map<ContactModel, Contact>(updateResult);
            return new ServiceResponse<Contact> { Entity = mappedContact };
        }

        public ServiceResponse<bool> DeleteContactById(int id)
        {
            var foundContact = _repository.GetById(id);

            if (foundContact == null)
            {
                var errorList = new List<string> { $"Contact with Id {id}, not found." };
                return new ServiceResponse<bool> { IsSuccessful = false, Errors = errorList };
            }

            var deleteResult = _repository.Delete(foundContact);
            return new ServiceResponse<bool> { Entity = deleteResult };
        }
    }
}
