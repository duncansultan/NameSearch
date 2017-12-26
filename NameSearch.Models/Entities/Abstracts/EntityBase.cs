using NameSearch.Models.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NameSearch.Models.Entities.Abstracts
{
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
