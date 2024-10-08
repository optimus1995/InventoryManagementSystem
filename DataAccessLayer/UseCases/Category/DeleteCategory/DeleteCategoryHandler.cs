﻿using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;

namespace ApplicationCore.UseCases.Category.DeleteCategory
{
    public class DeleteCategoryHandler: IRequestHandler<DeleteCategoryRequest,  DeleteCategoryResponse>
    //    {
    //    }
    //}
    { 
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<DeleteCategoryResponse> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteRecord(request.Id);

            return new DeleteCategoryResponse();
            
        }
    }
}

