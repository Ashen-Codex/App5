using System.Collections.Generic;
using App5.Models;

namespace App5.Repositories
{
    public class ProductRepository
    {
        private List<Product> _products = new List<Product>();

        public List<Product> GetAll() => _products;

        public void Add(Product product) => _products.Add(product);

        public void Delete(Product product) => _products.Remove(product);
    }
}
