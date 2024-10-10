using HH.Application.Specifications;
using HH.Domain.Entitties;


namespace HH.Application.Services.Vacancies.Specifications
{
    public class VacancyByPositionSpecification : Specification<Vacancy>
    {
        public VacancyByPositionSpecification(string position)
            : base(v => v.Position.Equals(position, StringComparison.OrdinalIgnoreCase))
        {
        }
    }
}
