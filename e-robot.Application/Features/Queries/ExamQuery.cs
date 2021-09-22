using AutoMapper;
using e_robot.Application.Contracts.Persistence;
using e_robot.Application.Features.DtoModels;
using e_robot.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace e_robot.Application.Features.Queries
{
    public class ExamQuery : IRequest<ExamDto>
    {        
        public int Id { get; set; }
        public string Guid { get; set; }
    }

    public class ExamQueryHandler : IRequestHandler<ExamQuery, ExamDto>
    {        
        private readonly IAsyncRepository repository;

        public ExamQueryHandler(IAsyncRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ExamDto> Handle(ExamQuery request, CancellationToken cancellationToken)
        {
            var exam = (await repository.GetAllAsync<Exam, ExamDto>(a => a.Id == request.Id)).FirstOrDefault();

            if(exam == null) throw new EntryPointNotFoundException();

            if (request.Guid != null)
            {
                var solutions = await repository.GetAllAsync<Solution, SolutionListDto>(a => a.User.Guid == request.Guid, includeProperties: "User");

                exam.Solutions = solutions;
            }           

            return exam;
        }
    }
}
