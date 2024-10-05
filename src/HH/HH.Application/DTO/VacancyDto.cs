using HH.Domain.Entitties.ValueObjects;

namespace HH.Application.DTO
{
    public record VacancyDto
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Position { get; init; } = string.Empty;
        public SalaryRange Salary { get; init; } = null!;
        public int WorkExperience { get; init; } 
        public string Region {  get; init; } = string.Empty;
        public int Views { get; init; } 
        public DateTime PostedDate { get; init; } 
    }
    public class ItemFileDto
    {
        public string PathToStorage { get; set; } = string.Empty;
    }
}
