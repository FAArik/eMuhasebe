namespace eMuhasebeApi.Domain.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public bool isDeleted { get; set; } = false;
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
