namespace Esport.Models;

public class Game {
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
}
