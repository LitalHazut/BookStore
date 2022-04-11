using BookStore.Data.Interfaces;
using BookStore.Model;
namespace BookStore.Data
{
    public class RepositoryFactory
    {
        public static IRepository<ProductBase> GetProductRepository()
        {
            return FileSystemDataContext.Instance;
        }
    }
}
