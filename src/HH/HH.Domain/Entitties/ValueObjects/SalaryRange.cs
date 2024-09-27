using FluentResults;

namespace HH.Domain.Entitties.ValueObjects
{
    public class SalaryRange
    {
        public int MinSalary { get; private set; }
        public int MaxSalary { get; private set; }

        private SalaryRange(int minSalary, int maxSalary)
        {
            if (minSalary < 0)
                Result.Fail("Минимальная зарплата не может быть меньше 0");
            if (maxSalary <= 0 || maxSalary < minSalary)
                Result.Fail("Максимальная зарплата не может быть меньше минимальной");

            MinSalary = minSalary;
            MaxSalary = maxSalary;
        }

        public static Result<SalaryRange> Create(int minSalary, int maxSalary)
        {
            if (minSalary < 0 || maxSalary <= 0 || maxSalary < minSalary)
            {
                return Result.Fail<SalaryRange>("Неверные значения для диапазона зарплат");
            }
            return Result.Ok(new SalaryRange(minSalary, maxSalary));
        }
    }


}
