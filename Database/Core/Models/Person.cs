using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Core.Models
{
    [Table(nameof(Person))]
    public class Person
    {
        public static readonly Person Default = new Person
        {
            Id = 1,
            FirstName = "Default",
            MiddleName = "",
            LastName = "",
            Birth = DateTime.MinValue,
            RoleId = 1
        };

        [Key]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime Birth { get; set; }

        public long RoleId { get; set; }

        public Role Role { get; set; }

        public ICollection<ContactMethod> Contacts { get; set; }

        public Userdata User { get; set; }
    }
}