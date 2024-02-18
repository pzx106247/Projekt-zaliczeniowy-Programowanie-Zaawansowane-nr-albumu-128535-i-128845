using Esport.Data;
using Esport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Esport.Pages;
public class CreateTournamentModel(
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

    public IActionResult OnGet() {
        if (User.Identity is not { IsAuthenticated: true }) {
            return RedirectToPage("/Tournaments/Index");
        }

        Games = [ .._context.Games ];

        return Page();
    }

    public IActionResult OnPost() {
        if (User.Identity is not { IsAuthenticated: true }) {
            _logger.LogError("User not authenticated");
            return RedirectToPage("/Tournaments/Index");
        }

        if (!ModelState.IsValid) {
            return Page();
        }

        var game = _context.Games.Where(g => g.Id == GameId).FirstOrDefault();
        var user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

        if (game is null || user is null) {
            _logger.LogError("Game or user not found");
            return RedirectToPage("/Tournaments/Index");
        }

        _context.Tournaments.Add(new Tournament {
            Id = Guid.NewGuid(),
            OwnerId = user.Id,
            Owner = user,
            Name = Name,
            GameId = GameId,
            Game = game,
            Date = Date,
            Approved = false
        });
        _context.SaveChanges();

        return RedirectToPage("/Tournaments/Index");
    }
}
