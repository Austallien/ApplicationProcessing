using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Core.Models
{
    [Table(nameof(History))]
    public record History
    {
        [Key]
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Description { get; set; }

        public long ApplicationId { get; set; }

        public Application Application { get; set; }

        public long OperatorId { get; set; }

        public Person Operator { get; set; }

        public long OperationId { get; set; }

        public Operation Operation { get; set; }
    }
}