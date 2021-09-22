using AutoMapper;
using e_robot.Application.Contracts.Persistence;
using e_robot.Application.Features.DtoModels;
using e_robot.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace e_robot.Application.Features.Queries
{
    public class ExamListQuery : IRequest<List<ExamListDto>>
    {       
    }

    public class ExamListQueryHandler : IRequestHandler<ExamListQuery, List<ExamListDto>>
    {
        
        private readonly IAsyncRepository repository;


        public ExamListQueryHandler(IAsyncRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<ExamListDto>> Handle(ExamListQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync<Exam, ExamListDto>();
        }
    }
}
