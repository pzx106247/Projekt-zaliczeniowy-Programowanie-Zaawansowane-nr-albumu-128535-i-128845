using Esport.Data;
using Esport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Esport.Pages;
public class DeleteTournamentModel(
    ILogger<GamesModel> logger,
    ApplicationDbContext context
) : PageModel {
    private readonly ILogger<GamesModel> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public Tournament Tournament { get; set; } = null!;

    public bool IsAdmin => User.IsInRole("Admin");

    public IActionResult OnGet(string id) {
        var tournament = _context.Tournaments.Include(t => t.Owner).Include(t => t.Game).Where(t => t.Id == Guid.Parse(id)).FirstOrDefault();
        if (tournament is null || User.Identity is not { IsAuthenticated: true } || (User.Identity.Name != tournament.Owner.UserName && !IsAdmin)) {
            return RedirectToPage("/Tournaments/Index");
        }

        Tournament = tournament;
        return Page();
    }

    public IActionResult OnPost(string id) {
        var tournament = _context.Tournaments.Include(t => t.Owner).Include(t => t.Game).Where(t => t.Id == Guid.Parse(id)).FirstOrDefault();
        if (tournament is null || User.Identity is not { IsAuthenticated: true } || (User.Identity.Name != tournament.Owner.UserName && !IsAdmin)) {
            return RedirectToPage("/Tournaments/Index");
        }

        Tournament = tournament;
        _context.Tournaments.Remove(Tournament);
        _context.SaveChanges();

        return RedirectToPage("/Tournaments/Index");
    }
}
