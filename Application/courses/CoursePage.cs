using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Persistence.DapperConnection.pagination;

namespace Application.courses
{
    public class CoursePage{
        public class Execute : IRequest<PageModel>{
            public string Title { get; set; }
            public int Page { get; set; }
            public int pageSize { get; set; }
        }

        public class Handler : IRequestHandler<Execute, PageModel>
        {
            private readonly IPagination _pagination; 

            public Handler(IPagination pagination){
                _pagination = pagination;
            }
            public async Task<PageModel> Handle(Execute request, CancellationToken cancellationToken){
                var storeProcedure = "usp_get_course_page";
                var sortColumn = "Title";
                var parameters = new Dictionary<string, object>();
                parameters.Add("title", request.Title);

                return await _pagination.returnPage(storeProcedure, request.Page, request.pageSize, parameters, sortColumn);
            }
           
        }

    }
}