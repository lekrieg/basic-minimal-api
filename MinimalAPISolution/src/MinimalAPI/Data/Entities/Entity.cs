using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Data.Entities;

public class Entity
{
    [Key]
    [Required]
    public int Id { get; private set; }
}
