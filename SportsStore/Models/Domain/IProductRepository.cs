﻿using System.Collections.Generic;

namespace SportsStore.Models.Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int productId);
        IEnumerable<Product> GetByCategory(int categoryId);
        void Add(Product product);
        void Delete(Product product);
        void SaveChanges();
    }
}
