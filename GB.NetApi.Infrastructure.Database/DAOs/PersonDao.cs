using GB.NetApi.Domain.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs
{
    /// <summary>
    /// Represents a <see cref="Person"/> entity as stored within a database
    /// </summary>
    [Table("PERSONS")]
    public sealed class PersonDao : BaseWritableDao<Person>
    {
        #region Properties

        [Column("firstname")]
        public string Firstname { get; set; }

        [Column("lastname")]
        public string Lastname { get; set; }

        [Column("birthdate")]
        public DateTime Birthdate { get; set; }

        #endregion

        public override void Fill(Person entity)
        {
            base.Fill(entity);
            Birthdate = entity.Birthdate;
            Firstname = entity.Firstname;
            Lastname = entity.Lastname;
        }

        public override Person Transform() => new Person()
        {
            Birthdate = Birthdate,
            Firstname = Firstname,
            ID = ID,
            Lastname = Lastname
        };
    }
}
