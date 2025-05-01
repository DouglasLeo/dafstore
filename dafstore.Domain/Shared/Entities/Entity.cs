namespace dafstore.Domain.Shared.Entities;

public abstract class Entity : IEquatable<Guid>
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTimeOffset CreatedAt = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; protected set; }

    public bool Equals(Guid id) => id == Id;

    public override int GetHashCode() => Id.GetHashCode();
}