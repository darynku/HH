using HH.Application.Specifications;
using HH.Domain.Entitties;

namespace HH.Application.Services.Vacancies.Specifications
{
    public class VacancyByRegionSpecification : Specification<Vacancy>
    {
        public VacancyByRegionSpecification(string region)
            : base(v => v.Region.Equals(region, StringComparison.OrdinalIgnoreCase))
        {
        }
    }

}
