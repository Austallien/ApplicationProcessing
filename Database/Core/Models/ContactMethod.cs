using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Core.Models
{
    [Table(nameof(ContactMethod))]
    public class ContactMethod
    {
        // WARNINNG :
        //      DO NOT CHANGE VALUES (BUT YOU CAN ADD NEW)
        //         OR
        //      MADE DATABASE BACKUP BEFORE CHANGING
        //      AND THEN ACTUALIZE DATA
        public enum Methods
        {
            Phone = 1,
            Email = 2
        }

        /// <summary>
        /// Get collection of possible <see cref="ContactMethod"/>s, which contains in database
        /// </summary>
        /// <returns>Collection of <see cref="ContactMethod"/>s</returns>
        public static IEnumerable<ContactMethod> GetAll()
        {
            var result = Enum.GetValues(typeof(Methods))
                .Cast<Methods>()
                .Select(role => new ContactMethod
                {
                    Id = (long)role,
                    Name = role.ToString()
                });

            return result;
        }

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<Person> Persons { get; set; }
    }
}