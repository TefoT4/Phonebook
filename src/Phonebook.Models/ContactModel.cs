using System.Collections.Generic;

namespace Phonebook.Models
{
    public class ContactModel : Model
    {
        public string Name { get; set; }
        
        public string City { get; set; }

        public string Email { get; set; }

        public string PictureUrl { get; set; }

        public string PhoneNumber { get; set; }
    }
}
