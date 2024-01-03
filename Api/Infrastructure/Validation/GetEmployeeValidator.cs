using Api.Features.Employees;
using FluentValidation;

namespace Api.Infrastructure.Validation;

public class GetEmployeeValidator : AbstractValidator<GetEmployee.Query>
{
    public GetEmployeeValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0);
    }
}
