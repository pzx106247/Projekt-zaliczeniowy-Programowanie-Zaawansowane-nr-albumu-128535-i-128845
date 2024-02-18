using Microsoft.AspNetCore.Identity;

namespace Esport.Models;

public class Result {
    public Guid Id { get; set; } = Guid.Empty;
    public Guid TournamentId { get; set; } = Guid.Empty;
    public Tournament Tournament { get; set; } = null!;
    public Guid WinnerId { get; set; } = Guid.Empty;
    public IdentityUser Winner { get; set; } = null!;
    public int WinnerScore { get; set; } = 0;
    public Guid LoserId { get; set; } = Guid.Empty;
    public IdentityUser Loser { get; set; } = null!;
    public int LoserScore { get; set; } = 0;
}
