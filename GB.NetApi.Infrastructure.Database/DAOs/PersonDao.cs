using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs
{
    /// <summary>
    /// Represents a <see cref="Person"/> entity as stored within a database
    /// </summary>
    [Table("PERSONS")]
    public sealed class PersonDao : BaseDao, ITransformable<Person>
    {
        #region Properties

        [Column("firstname")]
        public string Firstname { get; set; }

        [Column("lastname")]
        public string Lastname { get; set; }

        [Column("birthdate")]
        public DateTime Birthdate { get; set; }

        #endregion

        public Person Transform() => new()
        {
            Birthdate = Birthdate,
            Firstname = Firstname,
            ID = ID,
            Lastname = Lastname
        };
    }
}
