using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Core.Models
{
    [Table(nameof(Operation))]
    public record Operation
    {
        // WARNINNG :
        //      DO NOT CHANGE VALUES (BUT YOU CAN ADD NEW)
        //         OR
        //      MADE DATABASE BACKUP BEFORE CHANGING
        //      AND THEN ACTUALIZE DATA
        public enum Operations
        {
            Create = 1,
            Delete = 2,
            Update = 3,
            Cancel = 4
        }

        /// <summary>
        /// Get collection of possible <see cref="Operation"/>s, which contains in database
        /// </summary>
        /// <returns>Collection of <see cref="Operation"/>s</returns>
        public static IEnumerable<Operation> GetAll()
        {
            var result = Enum.GetValues(typeof(Operations))
                .Cast<Operations>()
                .Select(role => new Operation
                {
                    Id = (long)role,
                    Name = role.ToString()
                });
            return result;
        }

        [Key]
        public long Id { get; init; }

        public string Name { get; init; }

        public IReadOnlyCollection<History> History { get; init; }
    }
}