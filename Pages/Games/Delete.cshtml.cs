using Esport.Data;
using Esport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Esport.Pages;
public class DeleteGameModel(
    ILogger<GamesModel> logger,
    ApplicationDbContext context
) : PageModel {
    private readonly ILogger<GamesModel> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public Game Game { get; set; } = null!;

    public bool IsAdmin => User.IsInRole("Admin");

    public IActionResult OnGet(string id) {
        if (!IsAdmin) {
            return RedirectToPage("/Games/Index");
        }

        Game = _context.Games.Find(Guid.Parse(id))!;
        return Page();
    }

    public IActionResult OnPost(string id) {
        if (!IsAdmin) {
            return RedirectToPage("/Games/Index");
        }

        Game = _context.Games.Find(Guid.Parse(id))!;
        _context.Games.Remove(Game);
        _context.SaveChanges();

        return RedirectToPage("/Games/Index");
    }
}
