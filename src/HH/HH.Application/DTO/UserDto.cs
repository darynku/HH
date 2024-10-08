﻿namespace HH.Application.DTO
{
    public record UserDto
    {
        public string UserName { get; init; } = string.Empty;
        public IEnumerable<VacancyDto> Vacancies { get; init; } = [];
    }
}
