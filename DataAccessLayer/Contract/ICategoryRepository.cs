using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Category.Update;

namespace ApplicationCore.Contract
{
    public interface ICategoryRepository
    {

        public Task<Category> Create(Category category);

        public Task<Category> Update(Category category);
        public Task<FetchCategoryResponse> GetrecordforUpdate(int id);
        public Task<IEnumerable<Category>> GetAll();
        public Task DeleteRecord(int id);
        public Task<IEnumerable<Category>> GetAllCategories();
    }
}
