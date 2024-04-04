using GameStore.Client.Models;

namespace GameStore.Client;

public static class GameClient
{
    private static readonly List<Game> games =
    [
        new Game
        {
            Id = 1,
            Name = "Street Figther II",
            Genre = "Fighting",
            Price = 19.99m,
            ReleaseDate = new DateTime(1991, 2, 1)
        },
        new Game
        {
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "Roleplaying",
            Price = 59.99m,
            ReleaseDate = new DateTime(2010, 9, 30)
        },
        new Game
        {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99m,
            ReleaseDate = new DateTime(2022, 9, 27)
        }
    ];

    public static Game[] GetGames() => [.. games];

    public static void AddGame(Game game)
    {
        game.Id = games.Max(g => g.Id) + 1;
        games.Add(game);
    }

    public static Game GetGame(int id)
    {
        return games.Find(g => g.Id == id) ?? throw new InvalidOperationException("Game not found");
    }

    public static void UpdateGame(Game updatedGame)
    {
        var existingGame = GetGame(updatedGame.Id);
        existingGame.Name = updatedGame.Name;
        existingGame.Genre = updatedGame.Genre;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
    }

    public static void DeleteGame(int id)
    {
        var game = GetGame(id);
        games.Remove(game);
    }
}