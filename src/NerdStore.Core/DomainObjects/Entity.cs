namespace NerdStore.Core.DomainObjects;
public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        Created_At = DateTime.Now;
    }

    public override int GetHashCode() => 
        (GetType().GetHashCode() * 907) + Id.GetHashCode();
    

    public override string ToString() => 
        GetType().Name + " [Id=" + Id + "]";
    
}
