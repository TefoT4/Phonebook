using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Phonebook.Models;

namespace Phonebook.DataAccess
{
    public class PhonebookDatabase : IPhonebookDatabase
    {
        readonly string DatabasePath = Path.Combine(Directory.GetParent(
            Directory.GetCurrentDirectory()).FullName, "Phonebook.DataAccess", "Database");

        readonly string phonebookJson;

        public PhonebookDatabase()
        {
            phonebookJson = Path.Combine(DatabasePath, "phonebook.json");
        }
        
        public List<ContactModel> GetAll()
        {
            var contacts = LoadDatabase()
                .Where(x => !x.Deleted).ToList();

            return contacts;
        }

        public ContactModel GetContact(int id)
        {
            var foundContact = LoadDatabase()
                .FirstOrDefault(x => !x.Deleted && x.Id == id);

            return foundContact;
        }

        public ContactModel CreateContact(ContactModel contact)
        {
            var contacts = LoadDatabase();
            
            DateTime now = DateTime.Now;
            contact.Created = now;
            contact.Updated = now;
            contact.Id = contacts.Count() + 1;

            contacts.Add(new ContactModel
            {
                Id = contact.Id,
                Name = contact.Name,
                City = contact.City,
                Email = contact.Email,
                Created = contact.Created,
                Updated = contact.Updated,
                PictureUrl = contact.PictureUrl,
                PhoneNumber = contact.PhoneNumber
            });

            SaveToDatabase(contacts);

            return contact;
        }

        public ContactModel UpdateContact(ContactModel contact)
        {
            var contacts = LoadDatabase();
            
            contacts.Where(x => x.Id == contact.Id).ToList().ForEach(x =>
            {
                x.Name = contact.Name;
                x.City = contact.City;
                x.Email = contact.Email;
                x.Updated = DateTime.Now;
                x.PictureUrl = contact.PictureUrl;
                x.PhoneNumber = contact.PhoneNumber;
            });
            
            SaveToDatabase(contacts);

            return contact;
        }
        
        public bool Delete(int id)
        {
            var contacts = LoadDatabase();

            contacts.Where(x => x.Id == id).ToList().ForEach(x =>
            {
                x.Deleted = true;
            });

            SaveToDatabase(contacts);

            return true;
        }
        
        private List<ContactModel> LoadDatabase()
        {
            var contactsJson = File.ReadAllText(phonebookJson);
            var contacts = JsonConvert.DeserializeObject<List<ContactModel>>(contactsJson).ToList();

            return contacts.ToList().Where(x => !x.Deleted).ToList();
        }

        private void SaveToDatabase(List<ContactModel> contacts)
        {
            string contactToWrite = JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(phonebookJson, contactToWrite);
        }
    }
}
