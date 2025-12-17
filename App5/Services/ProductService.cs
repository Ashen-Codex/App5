using System.Collections.Generic;
using App5.Models;
using App5.Repositories;

namespace App5.Services
{
    public class ProductService
    {
        private ProductRepository _repository = new ProductRepository();

        public List<Product> GetProducts() => _repository.GetAll();

        public void AddProduct(Product product)
        {
            _repository.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _repository.Delete(product);
        }
    }
}
