using Esport.Data;
using Esport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Esport.Pages;
public class LeaveTournamentModel(
    ILogger<GamesModel> logger,
    ApplicationDbContext context
) : PageModel {
    private readonly ILogger<GamesModel> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public Tournament Tournament { get; set; } = null!;

    public bool IsAdmin => User.IsInRole("Admin");

    public IActionResult OnGet(string id) {
        var tournament = _context.Tournaments.Include(t => t.Owner).Include(t => t.Game).Where(t => t.Id == Guid.Parse(id)).FirstOrDefault();
        if (tournament is null || User.Identity is not { IsAuthenticated: true } || User.Identity.Name == tournament.Owner.UserName) {
            return RedirectToPage("/Tournaments/Index");
        }

        var user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
        if (user is null) {
            return RedirectToPage("/Tournaments/Index");
        }

        var participants = _context.Participants.Where(p => p.TournamentId == tournament.Id && p.UserId == user.Id).ToList();
        if (participants.Count == 0) {
            return RedirectToPage("/Tournaments/Index");
        }

        Tournament = tournament;
        return Page();
    }

    public IActionResult OnPost(string id) {
        var tournament = _context.Tournaments.Include(t => t.Owner).Include(t => t.Game).Where(t => t.Id == Guid.Parse(id)).FirstOrDefault();
        if (tournament is null || User.Identity is not { IsAuthenticated: true } || User.Identity.Name == tournament.Owner.UserName) {
            return RedirectToPage("/Tournaments/Index");
        }

        var user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
        if (user is null) {
            return RedirectToPage("/Tournaments/Index");
        }

        var participants = _context.Participants.Where(p => p.TournamentId == tournament.Id && p.UserId == user.Id).ToList();
        _context.Participants.RemoveRange(participants);
        _context.SaveChanges();

        return RedirectToPage("/Tournaments/Details", new { id = tournament.Id });
    }
}
