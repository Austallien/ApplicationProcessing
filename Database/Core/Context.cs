using Database.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace Database.Core
{
    public class Context : DbContext
    {
        public Context()
        {
            /*if (!(this.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                Database.Migrate();*/
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string server_address = ".\\SQLEXPRESS";
            string database_name = "Application";
            bool trusted_connection = true;

            optionsBuilder.UseSqlServer($"Server={server_address};Database={database_name};Trusted_Connection={trusted_connection};");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .HasData(Role.GetAll());

            builder.Entity<ContactMethod>()
                .HasData(ContactMethod.GetAll());

            builder.Entity<Person>()
                .HasData(Person.Default);

            builder.Entity<Status>()
                .HasData(Status.GetAll());

            builder.Entity<Tag>()
                .HasData(Tag.GetAll());

            builder.Entity<Operation>()
                .HasData(Operation.GetAll());

            // Person <=-> Role
            builder.Entity<Person>()
                .HasOne(p => p.Role)
                .WithMany(r => r.Persons)
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Contact composite key
            builder.Entity<Contact>()
                .HasKey(c => new { c.PersonId, c.MethodId });

            // Person <==> ContactMethod
            builder.Entity<Person>()
                .HasMany(p => p.Contacts)
                .WithMany(c => c.Persons)
                .UsingEntity(typeof(Contact));

            // Userdata <--> Person
            builder.Entity<Userdata>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<Userdata>(u => u.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Application <=-> Client (Person)
            builder.Entity<Application>()
                .HasOne(a => a.Client)
                .WithMany()
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Application <=-> Operator (Person)
            builder.Entity<Application>()
                .HasOne(a => a.Operator)
                .WithMany()
                .HasForeignKey(c => c.OperatorId)
                .OnDelete(DeleteBehavior.SetNull);

            // Application <=-> Status
            builder.Entity<Application>()
                .HasOne(a => a.Status)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Application <==> Tag
            builder.Entity<Application>()
                .HasMany(a => a.Tags)
                .WithMany(t => t.Applications)
                .UsingEntity("ApplicationTags");

            // History <=-> Application
            builder.Entity<History>()
                .HasOne(h => h.Application)
                .WithMany(a => a.History)
                .HasForeignKey(h => h.ApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            // History <=-> Operator (Person)
            builder.Entity<History>()
                .HasOne(h => h.Operator)
                .WithMany()
                .HasForeignKey(h => h.OperatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // History <=-> Operation
            builder.Entity<History>()
                .HasOne(h => h.Operation)
                .WithMany(o => o.History)
                .HasForeignKey(h => h.OperationId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ContactMethod> ContactMethods { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Userdata> Users { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<History> History { get; set; }
    }
}