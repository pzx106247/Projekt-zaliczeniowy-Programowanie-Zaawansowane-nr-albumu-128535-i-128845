using Microsoft.AspNetCore.Identity;

namespace Esport.Models;

public class Participant {
    public Guid TournamentId { get; set; } = Guid.Empty;
    public Tournament Tournament { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    public IdentityUser User { get; set; } = null!;
}