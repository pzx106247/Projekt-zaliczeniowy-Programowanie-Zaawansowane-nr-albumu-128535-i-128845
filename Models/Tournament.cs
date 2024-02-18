using Microsoft.AspNetCore.Identity;

namespace Esport.Models;

public class Tournament {
    public Guid Id { get; set; } = Guid.Empty;
    public string OwnerId { get; set; } = string.Empty;
    public IdentityUser Owner { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public Guid GameId { get; set; } = Guid.Empty;
    public Game Game { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.MinValue;
    public bool Approved { get; set; } = false;
    public List<Participant> Participants { get; set; } = [];
}
