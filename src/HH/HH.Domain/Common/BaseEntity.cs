namespace HH.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; init; }

        protected BaseEntity() { }
        protected BaseEntity(Guid id) 
        {
            Id = id;
        }
    }
}
