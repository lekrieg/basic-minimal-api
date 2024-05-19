using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Data.Entities;

public class Todo : Entity
{
    [Required]
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}
