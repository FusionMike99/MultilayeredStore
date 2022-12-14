using System;
using System.Collections.Generic;

namespace StoreBLL.DTO
{
    /// <summary>
    /// Represent data transform object - OrderItem
    /// </summary>
    public partial class OrderItemDto
    {
        /// <value>
        /// The <c>Product</c> property represents product
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Product"/> is a
        /// that you use for product.
        /// </remarks>
        public ProductDto Product { get; set; }

        /// <value>
        /// The <c>Amount</c> property represents amount
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Amount"/> is a
        /// that you use for amount.
        /// </remarks>
        public int Amount { get; set; }

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
        public OrderItemDto()
        {
        }

        /// <summary>
        /// Initialize <see cref="Product"/>, <see cref="Amount"/>
        /// </summary>
        public OrderItemDto(ProductDto product, int amount)
        {
            Product = product;
            Amount = amount;
            Cost = Product.Cost * Amount;
        }

        /// <summary>
        /// This method using for comparison
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>Returns boolean</returns>
        public override bool Equals(object obj)
        {
            return obj is OrderItemDto item &&
                   EqualityComparer<ProductDto>.Default.Equals(Product, item.Product) &&
                   Amount == item.Amount &&
                   Cost == item.Cost;
        }

        /// <summary>
        /// This method calculate hash code
        /// </summary>
        /// <returns>Returns integer</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Product, Amount, Cost);
        }
    }
}
