﻿using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Employee.UpdateRoleEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contract
{
    public interface IEmployeesRepository

    {
        public  Task<IEnumerable<AspNetRoles>> GetAllRoles();
         public Task<IEnumerable<AspNetUsers>> GetAll();

        //   public Task<IEnumerable<AspNetUsers>> GetRoleData(string userid);
         public Task<AspNetUsers> GetRoleData(string id);
        public Task<UpdateRoleRequest> UpdateRoles(UpdateRoleRequest Asproles);
       // object UpdateRoles(UpdateRoleRequest request);
    }
}
