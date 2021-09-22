using e_robot.Application.Features.DtoModels;
using e_robot.Application.Handlers;
using e_robot.Application.Contracts.Persistence;
using e_robot.Domain.Entities;
using e_robot.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using e_robot.Application.Exceptions;
using System.Net;

namespace e_robot.Application.Features.Commands
{
    public class SolutionCommmand : IRequest<SolutionDto>
    {
        public int? Id { get; set; }
        public string UserGuid { get; set; }
        public int ExamId { get; set; }
    }
    public class SolutionCommmandHander : IRequestHandler<SolutionCommmand, SolutionDto>
    {
        private IAsyncRepository repository;

        public SolutionCommmandHander(IAsyncRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
        }

        public async Task<SolutionDto> Handle(SolutionCommmand request, CancellationToken cancellationToken)
        {            
            if(request.Id != null)
            {
                var user = await repository.GetAsync<User>(a => a.Guid == request.UserGuid);

                var solution = new Solution(user.Id, request.ExamId);

                await repository.InsertAsync<Solution>(solution);

                request.Id = solution.Id;
            }

             var solutionDto = (await repository.GetAllAsync<Solution, SolutionDto>(a=>a.Id == request.Id,includeProperties:"Exam")).FirstOrDefault();

            if (solutionDto == null) throw new EntryPointNotFoundException();

            return solutionDto;

        }
    }
}
