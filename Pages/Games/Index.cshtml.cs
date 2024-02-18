using Esport.Data;
using Esport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Esport.Pages;
public class GamesModel(
    ILogger<GamesModel> logger,
    ApplicationDbContext context
) : PageModel {
    private readonly ILogger<GamesModel> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public List<Game> Games { get; set; } = [];

    public bool IsAdmin => User.IsInRole("Admin");

    public IActionResult OnGet() {
        Games = [.._context.Games];
        return Page();
    }
}
