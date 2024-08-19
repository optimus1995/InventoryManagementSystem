using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;

namespace ApplicationCore.UseCases.Category.Create
{
    public class CreateCategoryHandler: IRequestHandler<CreateCategoryRequest,  CreateCategoryResponse>
    //    {
    //    }
    //}
    { 
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = new ApplicationCore.DapperEntity.Category
            {
                Name = request.Name,
                IsActive = request.IsActive
            };

            var createdCategory = await _categoryRepository.Create(category);

            return new CreateCategoryResponse
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name,
                IsActive = createdCategory.IsActive
            };
        }
    }
}

