using Esport.Data;
using Esport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Esport.Pages;
public class EditTournamentModel(
    ILogger<GamesModel> logger,
    ApplicationDbContext context
) : PageModel {
    private readonly ILogger<GamesModel> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public Guid GameId { get; set; } = Guid.Empty;

    [BindProperty]
    public DateTime Date { get; set; } = DateTime.Now;

    public List<Game> Games { get; set; } = [];

    public IActionResult OnGet(string id) {
        var tournament = _context.Tournaments.Include(t => t.Owner).Where(t => t.Id == Guid.Parse(id)).FirstOrDefault();
        if (tournament is null || User.Identity is not { IsAuthenticated: true } || User.Identity.Name != tournament.Owner.UserName) {
            return RedirectToPage("/Tournaments/Index");
        }

        Games = [ .._context.Games ];

        Name = tournament.Name;
        GameId = tournament.GameId;
        Date = tournament.Date;

        return Page();
    }

    public IActionResult OnPost(string id) {
        var tournament = _context.Tournaments.Include(t => t.Owner).Where(t => t.Id == Guid.Parse(id)).FirstOrDefault();
        if (tournament is null || User.Identity is not { IsAuthenticated: true } || User.Identity.Name != tournament.Owner.UserName) {
            return RedirectToPage("/Tournaments/Index");
        }

        if (!ModelState.IsValid) {
            return Page();
        }

        var game = _context.Games.Where(g => g.Id == GameId).FirstOrDefault();
        if (game is null) {
            return RedirectToPage("/Tournaments/Index");
        }

        tournament.Name = Name;
        tournament.GameId = GameId;
        tournament.Game = game;
        tournament.Date = Date;
        _context.Tournaments.Update(tournament);
        _context.SaveChanges();

        return RedirectToPage("/Tournaments/Index");
    }
}
