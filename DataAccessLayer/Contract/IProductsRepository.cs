using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Products.SaveImages;
using System.Linq;

namespace ApplicationCore.Contract
{
    public interface IProductsRepository
    {

        public Task<Products> CreateProducts(Products products);
        public Task<IEnumerable<Products>> ShowByCatID(int cat, string userid);
        public Task<Products> UpdateProducts(Products products);
        public Task<Products> GetrecordforUpdate(int id);
        public Task<IEnumerable<Products>> GetAll(string userid);
        public Task DeleteRecord(int id);
        public Task<(int ProductCount, int CategoryCount)> GetCount();
        public Task<IEnumerable<ProductCategoryGraph>> GetCountforChart();

        public Task<ProductImages> SaveImages(ProductImages productImages);

        public Task<IEnumerable<ProductImages>> DisplayImages(int productId);

    }
}
