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
    public class SolutionUpdateBlockCommmand : IRequest<int>
    {
        public int? Id { get; set; }
        public string BlockXml { get; set; }
    }
    public class SolutionUpdateBlockCommmandHander : IRequestHandler<SolutionUpdateBlockCommmand, int>
    {
        private IAsyncRepository repository;

        public SolutionUpdateBlockCommmandHander(IAsyncRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(SolutionUpdateBlockCommmand request, CancellationToken cancellationToken)
        {
            var solution = await repository.GetAsync<Solution>(a=>a.Id == request.Id && a.Delete == false);

            if(solution == null) throw new EntryPointNotFoundException();

            solution.XmlBlock = request.BlockXml;

            await repository.UpdateSimpleAsync<Solution>(solution);

            return 1;
        }
    }
}
