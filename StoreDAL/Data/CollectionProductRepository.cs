using StoreDAL.Context;
using StoreDAL.Models;
using System;
using System.Collections.Generic;

namespace StoreDAL.Data
{
    ///<inheritdoc cref="IProductRepository"/>
    public partial class CollectionProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        /// <summary>
        /// Initialize context
        /// </summary>
        /// <param name="context">Store's context</param>
        public CollectionProductRepository(StoreContext context)
        {
            this.context = context;
        }

        ///<inheritdoc cref="IProductRepository.GetProductByID(string)"/>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when isn't found product with specified <paramref name="productID" />
        /// </exception>
        public virtual Product GetProductByID(string productID)
        {
            var foundProduct = context.Products.Find(item => item.ID.ToString() == productID);
            if (foundProduct == null)
                throw new InvalidOperationException("Not found product with that ID");
            return foundProduct;
        }

        ///<inheritdoc cref="IProductRepository.GetProductByName(string)"/>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when isn't found product with specified <paramref name="name" />
        /// </exception>
        public virtual Product GetProductByName(string name)
        {
            var foundProduct = context.Products.Find(item => item.Name == name);
            if (foundProduct == null)
                throw new InvalidOperationException("Not found product with that name");
            return foundProduct;
        }

        ///<inheritdoc cref="IProductRepository.GetProducts"/>
        public virtual IEnumerable<Product> GetProducts()
        {
            return context.Products;
        }

        ///<inheritdoc cref="IProductRepository.InsertProduct(Product)"/>
        public virtual bool InsertProduct(Product product)
        {
            context.Products.Add(product);
            bool result = context.Products.Contains(product);
            return result;
        }

        ///<inheritdoc cref="IProductRepository.UpdateProduct(Product)"/>
        public virtual bool UpdateProduct(Product product)
        {
            var updatedProduct = GetProductByID(product.ID.ToString());
            updatedProduct.Name = product.Name;
            updatedProduct.Category = product.Category;
            updatedProduct.Description = product.Description;
            updatedProduct.Cost = product.Cost;
            bool result = context.Products.Contains(updatedProduct);
            return result;
        }
    }
}
