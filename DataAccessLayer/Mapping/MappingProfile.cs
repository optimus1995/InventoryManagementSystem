using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Category.CreateCategory;
using ApplicationCore.UseCases.Category.ReadCategory;
using ApplicationCore.UseCases.Customers.CreateCustomers;
using ApplicationCore.UseCases.Customers.ReadCustomers;
using ApplicationCore.UseCases.Customers.UpdateCustomers;
using ApplicationCore.UseCases.Employee.GetRoles;
using ApplicationCore.UseCases.Employee.ReadEmployee;
using ApplicationCore.UseCases.Employee.UpdateRoleEmployee;
using ApplicationCore.UseCases.Orders.CreateOrders;
using ApplicationCore.UseCases.Products.CreateProducts;
using ApplicationCore.UseCases.Products.UpdateProducts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
                        //           --------------    Customers Use Cases Mapping                 --------
            CreateMap<SaveCustomersRequest, ApplicationCore.DapperEntity.Customers>();

            // CreateMap<Products, ProductViewModel>()
            //.ForMember(dest => dest.Categories, opt => opt.Ignore()); // Ignore Categories as it is a separate list
            CreateMap<List<Customers>, ReadCustomersResponse>()
        .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src));
            CreateMap<ApplicationCore.DapperEntity.Customers, FetchCustomersResponse>();
            CreateMap<UpdateCustomersRequest,Customers>();

            //      -------------            Category Use Cases Mapping          ---------------
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<List<Category>, ReadCategoryResponse>()
                .ForMember(destionation=> destionation.Categories,opt=>opt.MapFrom(src=>src));

            //          -         -----------       Employees Use Cases Mapping \
            CreateMap<List<AspNetUsers>, ReadEmployeesResponse>()
       .ForMember(destination => destination.AspNetUsers, opt => opt.MapFrom(src => src));

            CreateMap<List< AspNetRoles>, GetRolesResponse>()
                 .ForMember(dest => dest.AspNetRoles, opt => opt.MapFrom(src => src)); ;

            CreateMap<AspNetUsers, GetRoleResponse>()
                    .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.AspNetUserRoles.RoleId))  
                       .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.AspNetRoles.NormalizedName));
            //      ------------------   Orders Use Cases Mapping         -         -------------- 
            CreateMap<CustomerProductView, CreateOrdersResponse>()
    .ForMember(destination => destination.Products, opt => opt.MapFrom(src => src.Products))
    .ForMember(destination => destination.Customers, opt => opt.MapFrom(src => src.Customers))
    .ForMember(destination => destination.Categories, opt => opt.MapFrom(src => src.Categories));

            CreateMap<CreateProductsRequest, Products>();

            CreateMap<UpdateProductsRequest, Products>();
        }
    }
}
