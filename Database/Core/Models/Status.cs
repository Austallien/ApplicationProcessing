using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Core.Models
{
    [Table(nameof(Status))]
    public record Status
    {
        // WARNINNG :
        //      DO NOT CHANGE VALUES (BUT YOU CAN ADD NEW)
        //         OR
        //      MADE DATABASE BACKUP BEFORE CHANGING
        //      AND THEN ACTUALIZE DATA
        public enum Statuses
        {
            New = 1,
            InProcessing = 2,
            Done = 3,
            Cancelled = 4
        }

        /// <summary>
        /// Get collection of possible <see cref="Status"/>s, which contains in database
        /// </summary>
        /// <returns>Collection of <see cref="Status"/>s</returns>
        public static IEnumerable<Status> GetAll()
        {
            var result = Enum.GetValues(typeof(Statuses))
                .Cast<Statuses>()
                .Select(status => new Status
                {
                    Id = (int)status,
                    Name = status.ToString()
                });

            return result;
        }

        [Key]
        public long Id { get; init; }

        public string Name { get; init; }

        public IReadOnlyCollection<Application> Applications { get; init; }
    }
}