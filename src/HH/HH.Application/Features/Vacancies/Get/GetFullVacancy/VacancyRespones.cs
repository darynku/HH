using HH.Application.DTO;
using HH.Domain.Entitties.ValueObjects;

namespace HH.Application.Features.Vacancies.Get.GetFullVacancy 
{
    public class VacancyRespones
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Position { get; init; } = string.Empty;
    public SalaryRange Salary { get; init; } = null!;
    public int WorkExperience { get; init; }
    public string Region { get; init; } = string.Empty;
    public int Views { get; init; }
    public DateTime PostedDate { get; init; }
    public int ApplicationsCount { get; init; }
    public IEnumerable<ItemFileDto> Files { get; init; } = [];

}
}
