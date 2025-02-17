﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models.Repositories
{
    /// <summary>
    /// The class that manages product data
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products;

        public ProductRepository()
        {
            if (_products == null)
            {
                _products = new List<Product>();
                GenerateProductData();
            }
        }

        /// <summary>
        /// Generate the default list of products
        /// </summary>
        private void GenerateProductData()
        {
            int id = 0;
            _products.Add(new Product(++id, 10, 92.50, "Echo Dot", "(2nd Generation) - Black"));
            _products.Add(new Product(++id, 20, 9.99, "Anker 3ft / 0.9m Nylon Braided", "Tangle-Free Micro USB Cable"));
            _products.Add(new Product(++id, 30, 69.99, "JVC HAFX8R Headphone", "Riptidz, In-Ear"));
            _products.Add(new Product(++id, 40, 32.50, "VTech CS6114 DECT 6.0", "Cordless Phone"));
            _products.Add(new Product(++id, 50, 895.00, "NOKIA OEM BL-5J", "Cell Phone "));
        }

        /// <summary>
        /// Get all products from the inventory
        /// </summary>
        public Product[] GetAllProducts()
        {
            List<Product> list = _products.Where(p => p.Stock > 0).OrderBy(p => p.Name).ToList();
            return list.ToArray();
        }

        /// <summary>
        /// Retrieves a product from the inventory by its id.
        /// </summary>
        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Update the stock of a product in the inventory by its id
        /// </summary>
        public void UpdateProductStocks(int productId, int quantityToRemove)
        {
            Product product = _products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                product.Stock -= quantityToRemove;

                if (product.Stock <= 0)
                {
                    _products.Remove(product);
                }
            }
        }

        /// <summary>
        /// Updates a product in the inventory
        /// </summary>
        public void UpdateProduct(Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (product != null)
            {
                // Update properties of the product
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                product.Stock = updatedProduct.Stock;
                // Add any other properties that need to be updated
            }
        }
    }
}
