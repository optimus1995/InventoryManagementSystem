using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contract
{
    public interface ICustomersRepository
    {

        public Task<Customers> CreateRecord(Customers customers);

        public  Task<Customers> Update(Customers customers);
        public Task<Customers> GetrecordforUpdate(int id);
        public Task<IEnumerable<Customers>> GetAll(string userid);
        public Task DeleteRecord(int id);
    }
}
