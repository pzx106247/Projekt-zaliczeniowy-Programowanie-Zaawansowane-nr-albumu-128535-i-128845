using Esport.Data;
using Esport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Esport.Pages;
public class CreateGameModel(
    ILogger<GamesModel> logger,
    ApplicationDbContext context
) : PageModel {
    private readonly ILogger<GamesModel> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public string Genre { get; set; } = string.Empty;

    public bool IsAdmin => User.IsInRole("Admin");

    public IActionResult OnGet() {
        if (!IsAdmin) {
            return RedirectToPage("/Games/Index");
        }

        return Page();
    }

    public IActionResult OnPost() {
        if (!IsAdmin) {
            return RedirectToPage("/Games/Index");
        }

        if (!ModelState.IsValid) {
            return Page();
        }

        _context.Games.Add(new Game {
            Id = Guid.NewGuid(),
            Name = Name,
            Genre = Genre
        });
        _context.SaveChanges();

        return RedirectToPage("/Games/Index");
    }
}
