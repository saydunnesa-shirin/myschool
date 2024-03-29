﻿using Api.Domain.Institutions;
using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;

namespace Api.Features.Institutions;

public class UpdateInstitution
{
    public record Command : IRequest<InstitutionResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
    }

    public class Handler : IRequestHandler<Command, InstitutionResult>
    {
        private readonly IMapper _mapper;
        private readonly IInstitutionsRepository _repository;

        public Handler(
          IInstitutionsRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InstitutionResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var institutionToUpdate = await _repository.GetAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");
            institutionToUpdate.Name = command.Name;
            institutionToUpdate.Address = command.Address;
            institutionToUpdate.CountryId = command.CountryId;
            institutionToUpdate.UpdatedBy = 0;
            institutionToUpdate.UpdatedDate = DateTime.UtcNow;
           
            var updated = await _repository.UpdateAsync(institutionToUpdate, cancellationToken);

            var institution = await _repository.GetAsync(updated.Id, cancellationToken);

            var mapped = _mapper.Map<Institution, InstitutionResult>(institution);

            return mapped;
        }
    }
}
