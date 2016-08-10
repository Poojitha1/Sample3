using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sample3.Models
{
    public class ProductRepository : IProductRepository
    {
        ProductEntities db = new ProductEntities();
        

        public ProductRepository()
        {
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public void Add(Product item)
        {
                db.Products.Add(item);
                db.SaveChanges();
        }

        public void Remove(int id)
        {
            Product product = db.Products.Where(x => x.Id == id).SingleOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public bool Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }







       
    }
}