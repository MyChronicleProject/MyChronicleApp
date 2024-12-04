using MyChronicle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChronicle.Application.Persons
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? DeathPlace { get; set; }
        public Gender? Gender { get; set; }
        public string? Occupation { get; set; }
        public string? Note { get; set; }
        public Guid FamilyTreeId { get; set; }
    }
}
