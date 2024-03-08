﻿using Api.Domain.AcademicSessionTemplates;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessionTemplates;

public class UpdateAcademicSessionTemplate
{
    public record Command : IRequest<AcademicSessionTemplateResult>
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public int InstitutionId { get; set; }
    }

    public class Handler : IRequestHandler<Command, AcademicSessionTemplateResult>
    {
        private readonly IMapper _mapper;
        private readonly IAcademicSessionTemplatesRepository _repository;

        public Handler(
          IAcademicSessionTemplatesRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AcademicSessionTemplateResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var @update = new AcademicSessionTemplate
            {
                Id = command.Id,
                TemplateName = command.TemplateName,
                InstitutionId = command.InstitutionId
            };
            var updated = await _repository.UpdateAsync(@update, cancellationToken);

            var institution = await _repository.GetAsync(updated.Id, cancellationToken);

            var mappedAcademicSessionTemplate = _mapper.Map<AcademicSessionTemplate, AcademicSessionTemplateResult>(institution);

            return mappedAcademicSessionTemplate;
        }
    }
}
