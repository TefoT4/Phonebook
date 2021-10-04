using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Phonebook.Domains;
using Phonebook.Models;

namespace Phonebook.api.Profiles
{
    public class ContactsProfile : Profile
    {
        public ContactsProfile()
        {
            CreateMap<Contact, ContactModel>().ReverseMap();
        }
    }
}
