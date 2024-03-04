using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models;

public enum PersonType
{
    Plaintiff = 0,
    Defendant = 1,
    PlaintiffAttorney = 2,
    DefendantAttourney = 3,
    Witness = 4,
    ExpertWitness = 5,
    CoSummonend = 6,
    CoSummonendAttourney = 7,
    Judge = 8,
    Translator = 9,
}


[Table("Persons")]
[PrimaryKey(nameof(Id))]
public class Person : IBaseModel
{
    // Id of the Entity
    public Guid Id { get; set; }

    // Info about the Person (like name, titles...)
    [Required]
    public string Description { get; set; } = "";

    // What Type of Person (Attourney, Witness...)
    [Required]
    public PersonType Type { get; set; }

    // Link to the Events this Person is participating
    [JsonIgnore]
    public ICollection<Event> Events { get; set; } = [];
    // Link to the EventChanges this Person is participating
    [JsonIgnore]
    public ICollection<EventChange> EventChanges { get; set; } = [];

    public override string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tName:\t\t{Description}\n" +
            $"\tType:\t\t{Type}";
    }
}
