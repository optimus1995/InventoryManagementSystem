using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;

namespace ApplicationCore.UseCases.Category.Update
{
    public class FetchCategoryHandler: IRequestHandler<FetchCategoryRequest,  FetchCategoryResponse>
    //    {
    //    }
    //}
    { 
        private readonly ICategoryRepository _categoryRepository;

        public FetchCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<FetchCategoryResponse> Handle(FetchCategoryRequest request, CancellationToken cancellationToken)
        {

            var category  = await _categoryRepository.GetrecordforUpdate(request.Id);

            return category;

        }
    }
}

