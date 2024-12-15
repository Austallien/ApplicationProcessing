using Database.Core.Models;

namespace ApplicationProcessing.Core.Models
{
    /// <summary>
    /// Authorized user model
    /// </summary>
    public record User
    {
        /// <summary>
        /// (<see cref="Person.Id"/>) Identificator of authorized user
        /// </summary>
        public long Id { get; init; }

        /// <summary>
        /// (<see cref="Person.FirstName"/>) First name of authorized user
        /// </summary>
        public string FirstName { get; init; }

        /// <summary>
        /// (<see cref="Person.MiddleName"/>) First name of authorized user
        /// </summary>
        public string MiddleName { get; init; }

        /// <summary>
        /// (<see cref="Person.LastName"/>) First name of authorized user
        /// </summary>
        public string LastName { get; init; }

        /// <summary>
        /// (<see cref="Userdata.Username"/>) First name of authorized user
        /// </summary>
        public string Username { get; init; }
    }
}