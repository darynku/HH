using HH.Domain.Common;

namespace HH.Domain.Entitties
{
    public class VacancyApplication : BaseEntity
    {
        private VacancyApplication() { }

        public Guid UserId { get; init; }
        public Guid VacancyId { get; init; }

        public User User { get; init; } = null!;
        public Vacancy Vacancy { get; init; } = null!;

        public DateTime ApplyTime { get; init; } 
        public string Recomendation { get; init; } = string.Empty;

        private VacancyApplication(Guid id, Guid userId, Guid vacancyId, DateTime applyTime, string recomendation) : base(id) 
        {
            UserId = userId;
            VacancyId = vacancyId;
            ApplyTime = applyTime;
            Recomendation = recomendation;
        }
        
        public static VacancyApplication Create(Guid id, Guid userId, Guid vacancyId, DateTime applyTime, string recomendation)
        {
            return new VacancyApplication(id, userId, vacancyId, applyTime, recomendation);
        }
    }
}
