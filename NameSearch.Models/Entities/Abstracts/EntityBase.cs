using NameSearch.Models.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NameSearch.Models.Entities.Abstracts
{
    /// <summary>
    /// Entity Base Class
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.AuditableEntityBase" />
    /// <seealso cref="NameSearch.Models.Entities.Interfaces.IEntity{TEntity}" />
    public abstract class EntityBase<TEntity> : AuditableEntityBase, IEntity<TEntity>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// See http://www.aaronstannard.com/overriding-equality-in-dotnet/
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract bool Equals(TEntity other);
    }
}
