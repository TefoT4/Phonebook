using System;

namespace Phonebook.Domains
{
    public abstract class Domain
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}