using StoreDAL.Models;
using System.Collections.Generic;

namespace StoreDAL.Data
{
    /// <summary>
    /// This class describes describe contract of 
    /// pattern Repositiry for products
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// This method returns all products
        /// </summary>
        /// <returns>Collection of products</returns>
        IEnumerable<Product> GetProducts();

        /// <summary>
        /// This method returns product with specified name
        /// </summary>
        /// <param name="name">Product's name</param>
        /// <returns>Product with specified name</returns>
        Product GetProductByName(string name);

        /// <summary>
        /// This method returns product with specified id
        /// </summary>
        /// <param name="productID">Product's id</param>
        /// <returns>Product with specified id</returns>
        Product GetProductByID(string productID);

        /// <summary>
        /// This method insert product to context
        /// </summary>
        /// <param name="product">Will inserted product</param>
        /// <returns>Success of the operation (boolean)</returns>
        bool InsertProduct(Product product);

        /// <summary>
        /// This method update specified product in context
        /// </summary>
        /// <param name="product">Will updated product</param>
        /// <returns>Success of the operation (boolean)</returns>
        bool UpdateProduct(Product product);
    }
}
