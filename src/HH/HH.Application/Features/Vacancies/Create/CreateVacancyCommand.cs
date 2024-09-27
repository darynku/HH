using FluentResults;
using HH.Common.Contracts.Common;
using HH.Domain.Entitties.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace HH.Application.Features.Vacancies.Create
{
    public record CreateVacancyCommand(
        string Title,
        string Description,
        int MinSalary,
        int MaxSalary,
        DateTime PostedDate,
        Guid UserId,
        string Region,
        string Posintion,
        int WorkExpirience,
        DateTime ExpirationDate) : ICommand<Result<Guid>>;
}
