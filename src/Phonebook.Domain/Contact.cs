using System.Collections.Generic;

namespace Phonebook.Domains
{
    public class Contact : Domain
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Email { get; set; }
        
        public string PictureUrl { get; set; }

        public string PhoneNumber { get; set; }
    }
}
