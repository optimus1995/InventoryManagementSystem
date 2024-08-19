using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;

namespace ApplicationCore.UseCases.Category.Update
{
    public class UpdateCategoryHandler: IRequestHandler<UpdateCategoryRequest,  UpdateCategoryResponse>
    //    {
    //    }
    //}
    { 
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var cat = new ApplicationCore.DapperEntity.Category
            {
                Id = request.Id,
                Name = request.Name,
            };
            var UpdatedCategory = await _categoryRepository.Update(cat);

            return new UpdateCategoryResponse
            {
                Id = UpdatedCategory.Id,
                Name = UpdatedCategory.Name,
            };
        }
    }
}

