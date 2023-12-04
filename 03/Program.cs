using System.Text.RegularExpressions;

int sum = 0;
int sumRatio = 0;
List<string> lines = [];
Regex asteriskRegex = new("\\*");
Regex numRegex = new("\\d+");

using var reader = new StreamReader("input.txt");
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine() ?? "";
    lines.Add(line);
}

lines.Insert(0, new string('.', lines[0].Length));
lines.Add(new string('.', lines[0].Length));

for (int il = 0; il < lines.Count - 2; il++)
{
    var asterisks = asteriskRegex.Matches(lines[il + 1]);
    var numsTop = numRegex.Matches(lines[il]);
    var numsBottom = numRegex.Matches(lines[il + 2]);
    var numsCenter = numRegex.Matches(lines[il + 1]);
    foreach(Match asterisk in asterisks)
    {
        List<int> adjacent = [];
        AddAdjacents(numsTop, asterisk.Index, adjacent);
        AddAdjacents(numsBottom, asterisk.Index, adjacent);
        AddAdjacents(numsCenter, asterisk.Index, adjacent);

        if (adjacent.Count == 2)
        {
            var ratio = adjacent[0] * adjacent[1];
            sumRatio += ratio;
        }
    }

    sum += GetEngineParts(lines[il], lines[il + 1], lines[il + 2]).Sum();
}

Console.WriteLine(sum);
Console.WriteLine(sumRatio);

int[] GetEngineParts(string top, string middle, string bottom)
{
    List<string> parts = [];

    foreach (Match match in numRegex.Matches(middle))
    {
        var left = Math.Max(match.Index - 1, 0);
        var right = Math.Min(match.Index + match.Length + 1, middle.Length - 1);

        if (middle.AsEnumerable().Skip(left).Take(right - left).Any(c => IsSymbol(c)) ||
            top.AsEnumerable().Skip(left).Take(right - left).Any(c => IsSymbol(c)) ||
            bottom.AsEnumerable().Skip(left).Take(right - left).Any(c => IsSymbol(c)))
        {
            parts.Add(match.Value);
        }
    }

    return parts.Select(p => int.Parse(p)).ToArray();
}

static bool IsSymbol(char chr) => !(Char.IsDigit(chr) || chr == '.');

static void AddAdjacents(MatchCollection numbers, int index, List<int> adjacent)
{
    adjacent.AddRange(from Match num in numbers
                      where (num.Index <= index && index <= num.Index + num.Length) || index + 1 == num.Index
                      select int.Parse(num.Value));
}