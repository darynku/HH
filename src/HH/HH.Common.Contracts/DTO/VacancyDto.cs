namespace HH.Common.Contracts.DTO
{
    public record VacancyDto
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public int Salary { get; init; }
    }
}
