﻿using HH.Application.DTO;
using HH.Common.Contracts.Common;

namespace HH.Application.Features.Vacancies.Get
{
    public record GetOnlyVacanciesQuery(int Page, int PageSize) : IQuery<IEnumerable<VacancyDto>>;
}
