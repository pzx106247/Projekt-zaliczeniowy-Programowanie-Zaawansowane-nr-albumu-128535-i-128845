using Esport.Data;
using Esport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Esport.Pages;
public class TournamentModel(
    ILogger<GamesModel> logger,
    ApplicationDbContext context
) : PageModel {
    private readonly ILogger<GamesModel> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public List<Tournament> Tournaments { get; set; } = [];

    public bool IsAdmin => User.IsInRole("Admin");

    public IActionResult OnGet() {
        if (User.Identity is not { IsAuthenticated: true }) {
            Tournaments = [.. _context.Tournaments.Include(t => t.Game).Include(t => t.Owner).Where(t => t.Approved).OrderBy(t => t.Approved)];
        } else {
            Tournaments = [.._context.Tournaments.Include(t => t.Game).Include(t => t.Owner).Where(t => t.Approved || IsAdmin || t.Owner.UserName == User.Identity.Name).OrderBy(t => t.Approved)];
        }

        return Page();
    }
}
