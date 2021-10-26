using System;

namespace StoreDAL.Models
{
    /// <summary>
    /// Represent entity of data base - Product
    /// </summary>
    public partial class Product : BaseEntity
    {
        /// <value>
        /// The <c>Name</c> property represents name
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Name"/> is a
        /// that you use for name.
        /// </remarks>
        public string Name { get; set; }

        /// <value>
        /// The <c>Category</c> property represents category
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Category"/> is a
        /// that you use for category.
        /// </remarks>
        public string Category { get; set; }

        /// <value>
        /// The <c>Decsription</c> property represents description
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Description"/> is a
        /// that you use for description.
        /// </remarks>
        public string Description { get; set; }

        /// <value>
        /// The <c>Cost</c> property represents cost
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Cost"/> is a
        /// that you use for cost.
        /// </remarks>
        public float Cost { get; set; }

        /// <summary>
        /// Empty constructor for mapping
        /// </summary>
        public Product()
        {
        }

        /// <summary>
        /// Initialize <see cref="Name"/>, <see cref="Category"/>, <see cref="Description"/>,
        /// <see cref="Cost"/>
        /// </summary>
        public Product(string name, string category, string description, float cost)
        {
            Name = name;
            Category = category;
            Description = description;
            Cost = cost;
        }

        /// <summary>
        /// Initialize <see cref="BaseEntity.ID"/>, <see cref="Name"/>, <see cref="Category"/>, 
        /// <see cref="Description"/>, <see cref="Cost"/>
        /// </summary>
        public Product(string id, string name, string category, string description, float cost)
        {
            ID = Guid.Parse(id);
            Name = name;
            Category = category;
            Description = description;
            Cost = cost;
        }

        /// <summary>
        /// This method using for comparison
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>Returns boolean</returns>
        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   ID == product.ID &&
                   Name == product.Name &&
                   Category == product.Category &&
                   Description == product.Description &&
                   Cost == product.Cost;
        }

        /// <summary>
        /// This method calculate hash code
        /// </summary>
        /// <returns>Returns integer</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Name, Category, Description, Cost);
        }
    }
}
