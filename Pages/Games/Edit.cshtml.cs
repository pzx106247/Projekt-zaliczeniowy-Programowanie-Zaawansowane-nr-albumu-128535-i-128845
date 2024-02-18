using Esport.Data;
using Esport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Esport.Pages;
public class EditGameModel(
    ILogger<GamesModel> logger,
    ApplicationDbContext context
) : PageModel {
    private readonly ILogger<GamesModel> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public Game Game { get; set; } = null!;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public string Genre { get; set; } = string.Empty;

    public bool IsAdmin => User.IsInRole("Admin");

    public IActionResult OnGet(string id) {
        if (!IsAdmin) {
            return RedirectToPage("/Games/Index");
        }

        Game = _context.Games.Find(Guid.Parse(id))!;
        Name = Game.Name;
        Genre = Game.Genre;

        return Page();
    }

    public IActionResult OnPost(string id) {
        if (!IsAdmin) {
            return RedirectToPage("/Games/Index");
        }

        if (!ModelState.IsValid) {
            return Page();
        }

        Game = _context.Games.Find(Guid.Parse(id))!;
        Game.Name = Name;
        Game.Genre = Genre;
        _context.Games.Update(Game);
        _context.SaveChanges();

        return RedirectToPage("/Games/Index");
    }
}
