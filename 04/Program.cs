using System.Text.RegularExpressions;
var sumPoints = 0;
var regex = new Regex(@"\d+");
using var reader = new StreamReader("input.txt");
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine() ?? "";

    var card = line.Split(':')[0][5..].Trim();
    var nums = line.Substring(line.IndexOf(':')).Split('|');

    var winningNumbers = regex.Matches(nums[0]).Select(m => int.Parse(m.Value)).ToArray();
    var holdingNumbers = regex.Matches(nums[1]).Select(m => int.Parse(m.Value)).ToArray();

    var wins = holdingNumbers.Intersect(winningNumbers).Count();
    var points = wins > 1 ? (int)Math.Pow(2, wins - 1) : wins;

    //Console.WriteLine($"Card {card}: {points}");
    sumPoints += points;
}
 Console.WriteLine($"Sum: {sumPoints}");