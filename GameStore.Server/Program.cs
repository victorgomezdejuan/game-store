using GameStore.Server.Models;

List<Game> games = [
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

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var group = app.MapGroup("/games");

// GET /games
group.MapGet("/", () => games);

// GET /games/{id}
group.MapGet("/{id}", (int id) => {
    var game = games.Find(g => g.Id == id);

    if (game is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(game);
})
.WithName("GetGame");

// POST /games
group.MapPost("/", (Game game) => {
    game.Id = games.Max(g => g.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
});

// PUT /games/{id}
group.MapPut("/{id}", (int id, Game updatedGame) => {
    var existingGame = games.Find(g => g.Id == id);

    if (existingGame is null)
    {
        updatedGame.Id = id;
        games.Add(updatedGame);

        return Results.CreatedAtRoute("GetGame", new { id = updatedGame.Id }, updatedGame);
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
});

// DELETE /games/{id}
group.MapDelete("/{id}", (int id) => {
    var game = games.Find(g => g.Id == id);

    if (game is null)
    {
        return Results.NotFound();
    }

    games.Remove(game);

    return Results.NoContent();
});

app.Run();
