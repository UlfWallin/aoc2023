using System.Text.RegularExpressions;
var sumPoints = 0;
var regex = new Regex(@"\d+");
var cardNumber = 0; 
int[] copies = new int[202];

using var reader = new StreamReader("input.txt");
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine() ?? "";
    var nums = line[line.IndexOf(':')..].Split('|');

    var winningNumbers = regex.Matches(nums[0]).Select(m => int.Parse(m.Value)).ToArray();
    var holdingNumbers = regex.Matches(nums[1]).Select(m => int.Parse(m.Value)).ToArray();

    var wins = holdingNumbers.Intersect(winningNumbers).Count();
    var points = wins > 1 ? (int)Math.Pow(2, wins - 1) : wins;
    copies[cardNumber]++;
    if (wins > 0) {
        for(var i = 0; i < wins; i++) {
            var wonCard = i + cardNumber + 1;
            copies[wonCard] += copies[cardNumber];
        }
    }

    sumPoints += points;
    cardNumber++;
}
Console.WriteLine($"Sum: {sumPoints}");
Console.WriteLine($"Cards: {copies.Sum()}");