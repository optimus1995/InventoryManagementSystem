using ApplicationCore.DapperEntity;

namespace ApplicationCore.Contract
{
    public interface ICategoryRepository
    {

        public Task<Category> Create(Category category);

        public Task<Category> Update(Category category);
        public Task<Category> GetrecordforUpdate(int id);
        public Task<IEnumerable<Category>> GetAll();
        public Task DeleteRecord(int id);
    }
}
