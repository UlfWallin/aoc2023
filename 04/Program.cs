using System.Text.RegularExpressions;
var sumPoints = 0;
var regex = new Regex(@"\d+");
var lineNumber = 0; 
int[] copies = new int[220];

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
    copies[lineNumber] += 1;
    if (wins > 0) {
        for(var i = 0; i < wins; i++) {
            var wonCard = i + lineNumber + 1;
            copies[wonCard] += copies[lineNumber];
        }
    }

    //Console.WriteLine($"Card {card}: {points}");
    sumPoints += points;
    lineNumber++;
}
Console.WriteLine($"Sum: {sumPoints}");
Console.WriteLine($"Cards: {copies.Sum()}");