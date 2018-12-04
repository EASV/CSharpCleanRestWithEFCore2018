using System.Collections.Generic;
using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.Data.Repositories
{
    public class ProductRepository: IProductRepository
    {
        readonly CustomerAppContext _ctx;

        public ProductRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }
        
        public Product Create(Product product)
        {
            _ctx.Attach(product).State = EntityState.Added;
            _ctx.SaveChanges();
            return product;
        }

        public Product ReadyById(int id)
        {
            return _ctx.Products
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Order)
                .FirstOrDefault(o => o.Id == id);
        }

        public PagedList<Product> ReadAll(Filter filter)
        {
            var query = _ctx.Set<Product>();
            
            if (filter == null)
            {
                return new PagedList<Product>() {
                    Items = _ctx.Products.ToList(), 
                    Count = _ctx.Products.Count()
                };
            }

            var page = query.Select(e => e)
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage)
                .GroupBy(e => new { Total = query.Count() })
                .FirstOrDefault();
            
            if (page != null)
            {
                var total = page.Key.Total;
                var items = page.Select(e => e).ToList();
                return new PagedList<Product>() {Items = items, Count = total};
            }
            return new PagedList<Product>()
            {
                Items = new List<Product>(), 
                Count = 0
            };
        }

        public int Count()
        {
            return _ctx.Products.Count();
        }

        public Product Update(Product productUpdate)
        {
            //Clone orderlines to new location in memory, so they are not overridden on Attach
            var newOrderLines = new List<OrderLine>(productUpdate.OrderLines);
            //Attach product so basic properties are updated
            _ctx.Attach(productUpdate).State = EntityState.Modified;
            //Remove all orderlines with updated order information
            _ctx.OrderLines.RemoveRange(
                _ctx.OrderLines.Where(ol => ol.ProductId == productUpdate.Id)
            );
            //Add all orderlines with updated order information
            foreach (var ol in newOrderLines)
            {
                _ctx.Entry(ol).State = EntityState.Added;
            }
            // Save it
            _ctx.SaveChanges();
            //Return it
            return productUpdate;
        }

        public Product Delete(int id)
        {
            var removed = _ctx.Remove(new Product {Id = id}).Entity;
            _ctx.SaveChanges();
            return removed;
        }
    }
}