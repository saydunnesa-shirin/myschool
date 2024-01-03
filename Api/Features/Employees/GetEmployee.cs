using Api.Domain.Employees;
using Api.Infrastructure.Cache;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class GetEmployee
{
    public record Query : IRequest<Result>, ICacheableMediatrQuery
    {
        public int Id { get; set; }
        bool ICacheableMediatrQuery.BypassCache { get; init; }

        string ICacheableMediatrQuery.CacheKey
        {
            get
            {
                string baseKey = "get_employee";
                string emailKey = $"-Id={Id}";

                return $"{baseKey}{emailKey}";
            }
        }
    }

    public record Result
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Query, Result>
    {
        private readonly IEmployeesRepository _repository;
        private readonly IMapper _mapper;

        public Handler(
            IEmployeesRepository repository,
            IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(repository);
            ArgumentNullException.ThrowIfNull(mapper);

            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(
            Query query, 
            CancellationToken cancellationToken)
        {
            var employee =
                await _repository.GetEmployeeyAsync(query.Id, cancellationToken);
            var mappedEmployee = _mapper.Map<Employee, Result>(employee);

            return mappedEmployee;
        }
    }
}
