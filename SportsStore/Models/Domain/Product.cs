using System;

namespace SportsStore.Models.Domain
{
    public class Product
    {
        #region Fields and Properties
        public int ProductId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool InStock { get; set; }
        public Category Category { get; set; }
        #endregion

        #region Constructors and methods
        public Product()
        {
            InStock = true;
        }

        public Product(string name, int price, Category category, string description = null) : this()
        {
            Name = name;
            Price = price;
            Description = description;
            Category = category;
        }

        public Product(int productId, string name, int price, Category category) : this(name, price, category)
        {
            ProductId = productId;
        }

        public override bool Equals(object obj)
        {
            Product p = obj as Product;
            return p != null && p.ProductId == ProductId;
        }

        public override int GetHashCode()
        {
            return ProductId;
        }
        #endregion
    }
}