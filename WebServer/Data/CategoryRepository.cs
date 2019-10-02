using BookStore.Models;
using System;
using System.Linq;

namespace WebServer.Data
{
    public class CategoryRepository
    {
        public BookStoreContext Context { get; }

        public CategoryRepository(BookStoreContext context)
        {
            Context = context;
        }

        //public ProductCategory[] Get()
        //{
        //    return Context.ProductCategory.Take(10).ToArray();
        //}

        //public ProductCategory Get(int id)
        //{
        //    return Context.ProductCategory.Find(id);
        //}
    }
}
