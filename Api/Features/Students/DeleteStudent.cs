﻿using AutoMapper;
using MediatR;

namespace Api.Features.Students;

public class DeleteStudent
{
    public record Command : IRequest<int?>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<Command, int?>
    {
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _repository;

        public Handler(
          IStudentsRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int?> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(command.Id, cancellationToken);
            return deleted;
        }
    }
}
