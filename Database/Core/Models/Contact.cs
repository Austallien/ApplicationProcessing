using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Core.Models
{
    [Table(nameof(Contact))]
    public class Contact
    {
        public long PersonId { get; set; }

        public Person Person { get; set; }

        public long MethodId { get; set; }

        public ContactMethod Method { get; set; }

        public string Value { get; set; }
    }
}