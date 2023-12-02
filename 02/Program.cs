var gameIdSums = 0;
var gamePower = 0;
using var reader = new StreamReader("input.txt");
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine() ?? "";
    var gameId = line.Split(':')[0][5..];
    var gameSets = line.Split(':')[1].Split(';');
    var gameItems = new Dictionary<string, int>() {
        { "red", 0 },
        { "green", 0 },
        { "blue", 0 }
    };
    foreach (var set in gameSets)
    {
        var items = set.Split(',');
        foreach (var item in items)
        {
            var colour = item.Trim().Split(' ')[1];
            var count = int.Parse(item.Trim().Split(' ')[0]);
            gameItems[colour] = Math.Max(gameItems[colour], count);
        }
    }
    var red = gameItems["red"];
    var green = gameItems["green"];
    var blue = gameItems["blue"];

    gamePower += red * green * blue;

    // only 12 red cubes, 13 green cubes, and 14 blue cubes?
    if (red <= 12 && green <= 13 && blue <= 14)
    {
        gameIdSums += int.Parse(gameId);
    }
}

Console.WriteLine($"Part one: {gameIdSums}");
Console.WriteLine($"Part two: {gamePower}");