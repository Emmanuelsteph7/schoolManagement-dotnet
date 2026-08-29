namespace SchoolManagement.Domain.Common
{
    /* It is abstract because this class is intended to
    be a base/template for other classes.*/
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset? UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
