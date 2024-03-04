namespace Core.Models;

/// <summary>
/// Base Model for all Entities. This is necessary for Entity Framework
/// to determine common Properties of all the Models (mostly the Id).
/// </summary>
public interface IBaseModel
{
    public Guid Id { get; set; }
}
