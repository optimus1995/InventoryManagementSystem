using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;
using AutoMapper;

namespace ApplicationCore.UseCases.Category.CreateCategory
{
    public class CreateCategoryHandler: IRequestHandler<CreateCategoryRequest,  CreateCategoryResponse>
    //    {
    //    }
    //}
    { 
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public CreateCategoryHandler(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            //var category = new ApplicationCore.DapperEntity.Category
            //{
            //    Name = request.Name,
            //    IsActive = request.IsActive
            //};


            var category = _mapper.Map<DapperEntity.Category>(request);
            var createdCategory = await _categoryRepository.Create(category);

            return new CreateCategoryResponse();
            
        }
    }
}

