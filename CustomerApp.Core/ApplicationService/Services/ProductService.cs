using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService.Services
{
    public class ProductService: IProductService
    {
        readonly IProductRepository _productRepo;
        
        public ProductService(IProductRepository _productRepo)
        {
            this._productRepo = _productRepo;
        }
        
        public Product Create(Product product)
        {
            return _productRepo.Create(product);
        }

        public Product FindById(int id)
        {
            return _productRepo.ReadyById(id);
        }

        public FilteredList<Product> GetAll()
        {
            return _productRepo.ReadAll();
        }

        public FilteredList<Product> GetAllFiltered(Filter filter)
        {
            if (filter == null || (filter.ItemsPrPage == 0 && filter.CurrentPage == 0))
            {
                return GetAll();
            }
            return _productRepo.ReadAll(filter);
        }

        public Product Update(Product productUpdate)
        {
            return _productRepo.Update(productUpdate);
        }

        public Product Delete(int id)
        {
            return _productRepo.Delete(id);
        }
    }
}