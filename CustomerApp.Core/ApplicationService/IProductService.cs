using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService
{
    public interface IProductService
    {
        //Create //POST
        Product Create(Product product);
        //Read //GET
        Product FindById(int id);
        FilteredList<Product> GetAll();
        FilteredList<Product> GetAllFiltered(Filter filter);
        //Update //PUT
        Product Update(Product orderUpdate);
        
        //Delete //DELETE
        Product Delete(int id);
    }
}