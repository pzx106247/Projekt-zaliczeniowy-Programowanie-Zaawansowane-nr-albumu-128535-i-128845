using Esport.Data;
using Esport.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Esport.Pages;
public class TournamentDetailsModel(
    ILogger<GamesModel> logger,
    ApplicationDbContext context
) : PageModel {
    private readonly ILogger<GamesModel> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public Tournament Tournament { get; set; } = null!;
    public List<IdentityUser> Participants { get; set; } = [];

    public bool IsAdmin => User.IsInRole("Admin");

    public IActionResult OnGet(string id) {
        var tournament =  _context.Tournaments.Include(t => t.Game).Include(t => t.Owner).Where(t => t.Id == Guid.Parse(id)).FirstOrDefault();
        if (tournament is null) {
            return RedirectToPage("/Tournaments/Index");
        }

        Participants = [.. _context.Participants.Include(p => p.User).Where(p => p.TournamentId == tournament.Id).Select(p => p.User)];
        
        Tournament = tournament;

        return Page();
    }
}
