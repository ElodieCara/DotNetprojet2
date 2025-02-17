﻿using P2FixAnAppDotNetCode.Models.Repositories;
using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manage the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        public List<Product> GetAllProducts()
        {
            return new List<Product>(_productRepository.GetAllProducts());
        }

        /// <summary>
        /// Get a product from the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending on the ordered quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            foreach (var line in cart.Lines)
            {
                _productRepository.UpdateProductStocks(line.Product.Id, line.Quantity);
            }
        }       
    }
}
