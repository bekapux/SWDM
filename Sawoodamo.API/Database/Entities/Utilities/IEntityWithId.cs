namespace Sawoodamo.API.Database.Entities.Utilities;

public interface IEntityWithId<T>
{
    public T Id { get; set; }
}