using StoreBLL.DTO;
using System.Collections.Generic;

namespace StoreBLL.Services
{
    /// <summary>
    /// This class describes describe contract of 
    /// service for products
    /// </summary>
    public partial interface IProductService
    {
        /// <summary>
        /// This method returns list of products
        /// </summary>
        /// <returns>Collection of products</returns>
        IEnumerable<ProductDto> ListOfProducts();

        /// <summary>
        /// This method searches product with specified name
        /// </summary>
        /// <param name="name">Product's name</param>
        /// <returns>Product with specified name</returns>
        ProductDto SearchProductByName(string name);

        /// <summary>
        /// This method add new product
        /// </summary>
        /// <param name="product">Will added product</param>
        /// <returns>Success of the operation (boolean)</returns>
        bool AddNewProduct(ProductDto product);

        /// <summary>
        /// This method update specified product
        /// </summary>
        /// <param name="product">Will updated product</param>
        /// <returns>Success of the operation (boolean)</returns>
        bool UpdateProduct(ProductDto product);
    }
}
