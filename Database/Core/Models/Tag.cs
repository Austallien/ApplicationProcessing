using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Core.Models
{
    [Table(nameof(Tag))]
    public record Tag
    {
        // WARNINNG :
        //      DO NOT CHANGE VALUES (BUT YOU CAN ADD NEW)
        //         OR
        //      MADE DATABASE BACKUP BEFORE CHANGING
        //      AND THEN ACTUALIZE DATA
        public enum Tags
        {
            Null = -1,
            HighPriority = 1,
            MediumPriority = 2,
            LowPriority = 3
        }

        /// <summary>
        /// Get collection of possible <see cref="Tag"/>s, which contains in database
        /// </summary>
        /// <returns>Collection of <see cref="Tag"/>s</returns>
        public static IEnumerable<Tag> GetAll()
        {
            var result = Enum.GetValues(typeof(Tags))
                .Cast<Tags>()
                .Select(tag => new Tag
                {
                    Id = (long)tag,
                    Name = tag.ToString()
                });

            return result;
        }

        [Key]
        public long Id { get; init; }

        public string Name { get; init; }

        public IReadOnlyCollection<Application> Applications { get; init; }
    }
}