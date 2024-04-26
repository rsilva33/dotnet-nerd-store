namespace NerdStore.Core.DomainObjects;
public abstract class Entity
{
    public Guid Id { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public override int GetHashCode() => 
        (GetType().GetHashCode() * 907) + Id.GetHashCode();
    

    public override string ToString() => 
        GetType().Name + " [Id=" + Id + "]";
    
}
