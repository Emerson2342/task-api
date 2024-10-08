namespace TaskList.Components.Domain.Shared.Entities
{
    public abstract class Entity
    {
        public string Id { get; set; }
        protected Entity() {         
            Id = Guid.NewGuid().ToString("N").ToUpper();        
        }
    }
}
