using HH.Application.Specifications;
using HH.Domain.Entitties;

namespace HH.Application.Services.Vacancies.Specifications
{
    public class VacancyBySalaryRangeSpecification : Specification<Vacancy>
    {
        public VacancyBySalaryRangeSpecification(int minSalary, int maxSalary)
            : base(v => v.Salary.MinSalary >= minSalary && v.Salary.MaxSalary <= maxSalary)
        {
        }
    }

}
