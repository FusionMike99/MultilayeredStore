using System;

namespace StoreDAL.Models
{
    /// <summary>
    /// Represent base entity
    /// </summary>
    /// <remarks>
    /// Represent ID for entities of data base.
    /// </remarks>
    public class BaseEntity
    {
        /// <value>
        /// The <c>ID</c> property represents id
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="ID"/> is a
        /// that you use for id.
        /// </remarks>
        public Guid ID { get; set; }

        /// <summary>
        /// Initialize new ID
        /// </summary>
        public BaseEntity()
        {
            ID = Guid.NewGuid();
        }
    }
}
