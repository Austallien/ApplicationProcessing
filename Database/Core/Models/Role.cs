using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Core.Models
{
    [Table(nameof(Role))]
    public record Role
    {
        // WARNINNG :
        //      DO NOT CHANGE VALUES (BUT YOU CAN ADD NEW)
        //         OR
        //      MADE DATABASE BACKUP BEFORE CHANGING
        //      AND THEN ACTUALIZE DATA
        public enum Roles
        {
            None = -1,
            Client = 1,
            Operator = 2
        }

        /// <summary>
        /// Get collection of possible <see cref="Role"/>s, which contains in database
        /// </summary>
        /// <returns>Collection of <see cref="Role"/>s</returns>
        public static IEnumerable<Role> GetAll()
        {
            var result = Enum.GetValues(typeof(Roles))
                .Cast<Roles>()
                .Select(role => new Role
                {
                    Id = (long)role,
                    Name = role.ToString()
                });

            return result;
        }

        [Key]
        public long Id { get; init; }

        public string Name { get; init; }

        public IReadOnlyCollection<Person> Persons { get; init; }
    }
}