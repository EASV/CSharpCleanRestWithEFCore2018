using CustomerApp.Core.Entity;

namespace CustomerApp.Core.DomainService
{
    public interface IProductRepository
    {
        //Create Data
        //No Id when enter, but Id when exits
        Product Create(Product product);
        //Read Data
        Product ReadyById(int id);
        PagedList<Product> ReadAll(Filter filter = null);
        int Count();
        //Update Data
        Product Update(Product productUpdate);
        //Delete Data
        Product Delete(int id);
    }
}