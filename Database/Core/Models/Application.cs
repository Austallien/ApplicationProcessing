using Database.Migrations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Core.Models
{
    [Table(nameof(Application))]
    public class Application
    {
        public static Application GetBlank() => new Application
        {
            Id = 0,
            Created = DateTime.MinValue,
            Updated = DateTime.MinValue,
            Title = string.Empty,
            Content = string.Empty,
            ClientId = 0,
            Client = null,
            OperatorId = null,
            Operator = null,
            StatusId = 0,
            Status = null,
            Tags = new Collection<Tag>(),
            History = null
        };

        [Key]
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public long ClientId { get; set; }

        public Person Client { get; set; }

        public long? OperatorId { get; set; }

        public Person? Operator { get; set; }

        public long StatusId { get; set; }

        public Status Status { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<History> History { get; set; }
    }
}