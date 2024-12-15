using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Core.Models
{
    [Table(nameof(Userdata))]
    public class Userdata
    {
        [Key]
        public long Id { get; set; }

        public string Username { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public long PersonId { get; set; }

        public Person Person { get; set; }
    }
}