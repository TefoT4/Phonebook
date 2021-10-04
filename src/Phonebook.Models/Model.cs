using System;

namespace Phonebook.Models
{
    public abstract class Model
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }
        
        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}